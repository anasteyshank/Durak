/**
 * MainForm.cs - The MainForm of the DurakGameUI project
 * 
 * This project is used to play the Durak card game.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-04-09
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

/** ATTRIBUTION
 *  ===========
 *  The card images used in this class were downloaded from 
 *  http://acbl.mybigcommerce.com/52-playing-cards/ on 3 March 2020.
 *  The Joker cards images were retrieved on 3 March 2020 from
 *  http://clipart-library.com/clipart/8cxrabjji.htm (License: for personal 
 *  use only).
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using MyCardBox;
using CardLibrary;
using DurakLibrary;
using System.IO;

namespace DurakGameUI
{
    public partial class frmDurak : Form
    {
        #region Fields and Properties
        /// <summary>
        /// The amount, in points, that CardBox controls are enlarged when hovered over. 
        /// </summary>
        private const int POP = 25;

        /// <summary>
        /// Result that indicates a draw
        /// </summary>
        private const string DRAW = "DRAW!";

        /// <summary>
        /// Result that indicates that the player lost the game
        /// </summary>
        private const string PLAYER_LOST = "YOU LOST:(";

        /// <summary>
        /// Result that indicates that the player won the game
        /// </summary>
        private const string PLAYER_WON = "YOU WON!";

        /// <summary>
        /// Default player's name
        /// </summary>
        private const string DEFAULT_PLAYER_NAME = "Your Hand";

        /// <summary>
        /// Default value for the counts
        /// </summary>
        private const string DEFAULT_STATISTICS_VALUE = "0";

        /// <summary>
        /// The regular size of a CardBox control
        /// </summary>
        static private Size regularSize = new Size(94, 128);

        /// <summary>
        /// The number od cards in a player's hand
        /// </summary>
        static private int numberOfCardsInHand = 6;

        /// <summary>
        /// Tha playing deck of cards
        /// </summary>
        private Cards myDealer = new Cards(new DurakDeck());

        /// <summary>
        /// Number of cards in the playing area
        /// </summary>
        private int numberOfCardsInPlay = 0;

        /// <summary>
        /// Number of cards in the player's hand
        /// </summary>
        private int playerHandCount = 0;

        /// <summary>
        /// Number of cards in the computer's hand
        /// </summary>
        private int computerHandCount = 0;

        /// <summary>
        /// Indicates whether it's computer's turn to attack
        /// </summary>
        private bool computerAttacks = false;

        /// <summary>
        /// Indicates whether the player takes the cards
        /// </summary>
        private bool playerTakes = false;

        /// <summary>
        /// Indicates whether the game is over
        /// </summary>
        private bool gameOver = false;

        /// <summary>
        /// Game object that holds the main logic of the game
        /// </summary>
        private Game game;

        /// <summary>
        /// Refers to the card being dragged from one panel to another.
        /// </summary>
        private PictureBox dragCard;

        // STATISTICS

        /// <summary>
        /// Number of games played
        /// </summary>
        private int numberOfGames = 0;

        /// <summary>
        /// Number of games the player won
        /// </summary>
        private int numberOfWins = 0;

        /// <summary>
        /// Number of games the player lost
        /// </summary>
        private int numberOfLosses = 0;

        /// <summary>
        /// Number of draws
        /// </summary>
        private int numberOfDraws = 0;

        /// <summary>
        /// File that holds game's statistics
        /// </summary>
        private string statisticsFile = "../../LogAndStatistics/statistics.txt";

        /// <summary>
        /// File that holds game logs
        /// </summary>
        private string logFile = "../../LogAndStatistics/logs.txt";
        #endregion

        #region Form and Static Event Handlers
        /// <summary>
        /// Constructor for frmDurak
        /// </summary>
        public frmDurak()
        {
            InitializeComponent();
            lblResult.Hide();   // Hide the Result label
        }

        /// <summary>
        /// Fires when the game loads for the 1st time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDurak_Load(object sender, EventArgs e)
        {
            StartGame();    // start the game

            // If file with statistics exists
            if (File.Exists(statisticsFile))
            {
                // Attemp to open the file
                try
                {
                    // Open the file to get data from it
                    StreamReader sr = new StreamReader(File.OpenRead(statisticsFile));

                    // Update the form's labels based on the retrieved stats 
                    lblPlayerHand.Text = GetStatistics(sr.ReadLine(), DEFAULT_PLAYER_NAME);
                    lblGamesPlayedCount.Text = GetStatistics(sr.ReadLine(), DEFAULT_STATISTICS_VALUE);
                    lblWinsCount.Text = GetStatistics(sr.ReadLine(), DEFAULT_STATISTICS_VALUE);
                    lblLossCount.Text = GetStatistics(sr.ReadLine(), DEFAULT_STATISTICS_VALUE);
                    lblDrawCount.Text = GetStatistics(sr.ReadLine(), DEFAULT_STATISTICS_VALUE);

                    // Convert counters to integers
                    numberOfGames = int.Parse(lblGamesPlayedCount.Text);
                    numberOfWins = int.Parse(lblWinsCount.Text);
                    numberOfLosses = int.Parse(lblLossCount.Text);
                    numberOfDraws = int.Parse(lblDrawCount.Text);

                    // Close the file
                    sr.Close();
                }
                // Catch any exceptions occured
                catch (Exception ex) { System.Diagnostics.Debug.Print(ex.ToString()); }
            }
            // If file doesn't exists, open the NamePromptForm
            else { OpenNamePromptForm(); }
        }

        /// <summary>
        /// Closes the current form
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Fires on form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDurak_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Update game's statistics
            UpdateStatistics();
            // Update the logs file to indicate when the game ended
            UpdateLogs("The game ended at " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:tt"));
            UpdateLogs("==================================================================================================================");
            UpdateLogs("");
            UpdateLogs("");
        }

        /// <summary>
        /// Fires when a user clicks on the ChangeName button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeName_Click(object sender, EventArgs e)
        {
            OpenNamePromptForm();   // Open the NamePromptForm
        }

        /// <summary>
        /// Fires when a user clicks on the ResetStatistics button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetStatistics_Click(object sender, EventArgs e)
        {
            // Set statistics values to defaults
            lblGamesPlayedCount.Text = DEFAULT_STATISTICS_VALUE;
            lblWinsCount.Text = DEFAULT_STATISTICS_VALUE;
            lblLossCount.Text = DEFAULT_STATISTICS_VALUE;
            lblDrawCount.Text = DEFAULT_STATISTICS_VALUE;

            // Convert counters to integers
            numberOfGames = int.Parse(lblGamesPlayedCount.Text);
            numberOfWins = int.Parse(lblWinsCount.Text);
            numberOfLosses = int.Parse(lblLossCount.Text);
            numberOfDraws = int.Parse(lblDrawCount.Text);
        }

        /// <summary>
        /// Fires when the player clicks on the Reset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Update the logs file
            UpdateLogs("Player clicks on the Reset button.");
            UpdateLogs("==================================================================================================================");
            UpdateLogs("");
            UpdateLogs("");

            // Hide the game's result
            lblResult.Hide();

            // Enable the buttons
            btnTake.Enabled = true;
            btnReady.Enabled = true;

            // Instantiate a new deck
            myDealer = new Cards(new DurakDeck());

            // Reset counts
            numberOfCardsInPlay = 0;    // reset number of cards on the table
            playerHandCount = 0;        // reset number of player's cards
            computerHandCount = 0;      // reset number of computers's cards

            // Reset boolean values
            computerAttacks = false;  
            playerTakes = false;
            gameOver = false;

            // Clear the panels
            pnlCPUHand.Controls.Clear();
            pnlPlayerHand.Controls.Clear();
            pnlPlayArea.Controls.Clear();

            // Add controls to the main playing area
            pnlPlayArea.Controls.Add(lblDeck);
            pnlPlayArea.Controls.Add(pbDeck);
            pnlPlayArea.Controls.Add(pbTrump);

            // Show controls in the main playing area
            pbDeck.Visible = true;
            pbTrump.Visible = true;
            lblDeck.Visible = true;
            
            // Start a new game
            StartGame();
        }

        /// <summary>
        /// Fires when the player clicks the Take button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickUp_Click(object sender, EventArgs e)
        {
            UpdateLogs("Player clicks on the Take button.");    // Update the logs file
            EndOfGame();    // check whether the game is over
            // If the game isn't over
            if (!gameOver)
            {
                playerTakes = true;        // player takes the cards
                ComputerAttacks();         // computer attacks
                computerAttacks = true;
            }
        }

        /// <summary>
        /// Fires when the player clicks the Ready button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReady_Click(object sender, EventArgs e)
        {
            // if there are cards on the table:
            if (game.CardsInPlay.Count > 0)
            {
                UpdateLogs("Player clicks on the Ready button.");   // Update the logs file
                computerAttacks = true;     
                // check whether the computer takes the cards
                if (game.ComputerPicksUp)
                {
                    // computer takes the cards on the table
                    ComputerPicksUp();          
                    computerAttacks = false;
                }

                NewRound(); // start a new round
                
                // computer attacks if it's computer's turn
                if (computerAttacks) ComputerAttacks();
            }
        }

        /// <summary>
        /// Make the mouse pointer a "move" pointer when a drag enters a Panel
        /// </summary>
        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;  // Make the mouse pointer a "move" pointer
        }

        /// <summary>
        /// Move a card/control when it is dropped from one Panel to another.
        /// </summary>
        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            // If there is a CardBox to move
            if (dragCard != null)
            {
                // Determine which Panel is which
                Panel thisPanel = sender as Panel;
                Panel fromPanel = dragCard.Parent.Parent as Panel;

                // If neither panel is null (no conversion issue)
                if (thisPanel != null && fromPanel != null)
                {
                    // if the Panels are not the same 
                    if (thisPanel != fromPanel)
                    {
                        // (this would happen if a card is dragged from one spot in the Panel to another)
                        // Remove the card from the Panel it started in
                        fromPanel.Controls.Remove(dragCard.Parent);
                        // Add the card to the Panel it was dropped in 
                        thisPanel.Controls.Add(dragCard.Parent);
                        // Realign cards in both Panels
                        RealignCards(thisPanel);
                        RealignCards(fromPanel);
                    }
                }
            }
        }

        /// <summary>
        /// Move a card/control when it is dropped from one Panel to pnlPlayArea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPlayArea_DragDrop(object sender, DragEventArgs e)
        {
            // If there is a CardBox to move
            if (dragCard != null)
            {
                // Get a PlayingCard object from the dragCard
                CardBox humanCardBox = (CardBox)dragCard.Parent;
                PlayingCard humanCard = new PlayingCard(humanCardBox.Rank, humanCardBox.Suit);

                // Move the card if:
                //  player can defend OR
                //  player can attack and number of cards on the table is less than a limit OR
                //  player attacks. and there are no cards on the table
                if ((game.PlayerCanDefend(computerAttacks, humanCard)) || 
                   (!computerAttacks && game.CanAttack(humanCard) && numberOfCardsInPlay < computerHandCount * 2 - 1) || 
                   (!computerAttacks && numberOfCardsInPlay == 0))
                {
                    // Update the logs file based on whose turn it is to attack
                    if (computerAttacks)
                        UpdateLogs("Player defends with " + humanCard.DebugString());
                    else
                        UpdateLogs("Player attacks with " + humanCard.DebugString());

                    // Determine which Panel is which
                    Panel thisPanel = sender as Panel;
                    Panel fromPanel = dragCard.Parent.Parent as Panel;

                    // If neither panel is null (no conversion issue)
                    if (thisPanel != null && fromPanel != null)
                    {
                        // if the Panels are not the same 
                        if (thisPanel != fromPanel)
                        {
                            // (this would happen if a card is dragged from one spot in the Panel to another)
                            // Remove the card from the Panel it started in
                            fromPanel.Controls.Remove(dragCard.Parent);
                            // Add the card to the Panel it was dropped in 
                            thisPanel.Controls.Add(dragCard.Parent);
                            // Realign cards in the 1st Panel
                            RealignCards(fromPanel);

                            AddRemoveEventHandlers(dragCard, false); // remove event handlers from the card

                            dragCard.Parent.Size = regularSize;     // set the card's size to the regular size
                            dragCard.BackColor = Color.Transparent; // set the back colour to transparent

                            // Increase number of cards on the table
                            numberOfCardsInPlay++;
                            if (game.ComputerPicksUp) numberOfCardsInPlay++;
                            // Remove a card from the player's hand
                            game.Human.PlayHand.Remove(humanCard);
                            // Add a card to the playing area
                            game.CardsInPlay.Add(humanCard);
                            // Set card's location on the table
                            AddCardToPlayArea(thisPanel.Controls[thisPanel.Controls.Count - 1]);    
                        }
                    }
                    // Determine whether it's computer's turn to attack or defend
                    if (computerAttacks)
                        ComputerAttacks();
                    else
                        ComputerDefends();
                }
            }
        }
        #endregion

        #region CardBox Event Handlers
        /// <summary>
        /// CardBox controls grow in size when the mouse is over it.
        /// </summary>
        void CardBox_MouseEnter(object sender, EventArgs e)
        {
            // Convert sender to a CardBox
            PictureBox aCardBox = sender as PictureBox;

            // If the conversion worked
            if (aCardBox != null)
            {
                // Enlarge the card for visual effect
                aCardBox.Parent.Size = new Size(regularSize.Width + POP, regularSize.Height + POP);
                // move the card to the top edge of the panel.
                aCardBox.Parent.Top = 0;
            }
        }

        /// <summary>
        /// CardBox control shrinks to regular size when the mouse leaves.
        /// </summary>
        void CardBox_MouseLeave(object sender, EventArgs e)
        {
            // Convert sender to a CardBox
            PictureBox aCardBox = sender as PictureBox;
            // If the conversion worked
            if (aCardBox != null)
            {
                aCardBox.Parent.Size = regularSize; // resize the card back to regular size
                aCardBox.Parent.Top = POP;          // move the card down to accommodate for the smaller size.
            }
        }

        /// <summary>
        /// Initiate a card move on the start of a drag.
        /// </summary>
        private void CardBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Set dragCard 
            dragCard = sender as PictureBox;
            // If the conversion worked
            if (dragCard != null)
            {
                // Set the data to be dragged and the allowed effect dragging will have.
                DoDragDrop(dragCard, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// When a drag is enters a card, enter the parent panel instead.
        /// </summary>
        private void CardBox_DragEnter(object sender, DragEventArgs e)
        {
            // Convert sender to a CardBox
            PictureBox aCardBox = sender as PictureBox;

            // If the conversion worked
            if (aCardBox != null)
            {
                // Do the operation on the parent panel instead
                Panel_DragEnter(aCardBox.Parent.Parent, e);
            }
        }

        /// <summary>
        /// When a drag is dropped on a card, drop on the parent panel instead.
        /// </summary>
        private void CardBox_DragDrop(object sender, DragEventArgs e)
        {
            // Convert sender to a CardBox
            PictureBox aCardBox = sender as PictureBox;

            // If the conversion worked
            if (aCardBox != null)
            {
                // Do the operation on the parent panel instead
                Panel_DragDrop(aCardBox.Parent.Parent, e);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Repositions the cards in a panel so that they are evenly distributed in the area available.
        /// </summary>
        /// <param name="panelHand"></param>
        private void RealignCards(Panel panelHand)
        {
            // Determine the number of cards/controls in the panel.
            int myCount = panelHand.Controls.Count;

            // If there are any cards in the panel
            if (myCount > 0)
            {
                // Determine how wide one card/control is.
                int cardWidth = panelHand.Controls[0].Width;

                // Determine where the left-hand edge of a card/control placed 
                // in the middle of the panel should be  
                int startPoint = (panelHand.Width - cardWidth) / 2;

                // An offset for the remaining cards
                int offset = 0;

                // If there are more than one cards/controls in the panel
                if (myCount > 1)
                {
                    // Determine what the offset should be for each card based on the 
                    // space available and the number of card/controls
                    offset = (panelHand.Width - cardWidth - 2 * POP) / (myCount - 1);

                    // If the offset is bigger than the card/control width, i.e. there is lots of room, 
                    // set the offset to the card width. The cards/controls will not overlap at all.
                    if (offset > cardWidth)
                        offset = cardWidth;

                    // Determine width of all the cards/controls 
                    int allCardsWidth = (myCount - 1) * offset + cardWidth;

                    // Set the start point to where the left-hand edge of the "first" card should be.
                    startPoint = (panelHand.Width - allCardsWidth) / 2;
                }

                // Align the "first" card (which is the last control in the collection)
                panelHand.Controls[myCount - 1].Top = POP;
                panelHand.Controls[myCount - 1].Left = startPoint;

                // for each of the remaining controls, in reverse order.
                for (int index = myCount - 2; index >= 0; index--)
                {
                    // Align the current card
                    panelHand.Controls[index].Top = POP;
                    panelHand.Controls[index].Left = panelHand.Controls[index + 1].Left + offset;
                }
            }
        }

        /// <summary>
        /// Method that sets the initial game
        /// </summary>
        private void StartGame()
        {
            // Update the logs file
            UpdateLogs("==================================================================================================================");
            UpdateLogs("The game was started at " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:tt"));
            UpdateLogs("");

            game = new Game();  // instantiate a new Game object

            DealCards();        // deal cards to players
            UpdateLogs(DisplayCardsInHand("Computer's hand: ", game.Computer));
            UpdateLogs(DisplayCardsInHand("Player's hand: ", game.Human));
            UpdateLogs("");

            // Get the last card in the deck
            PlayingCard lastCard = myDealer[myDealer.Count - 1];

            // Show a card with a trump suit on the table
            pbTrump.FaceUp = true;
            pbTrump.Suit = lastCard.Suit;
            pbTrump.Rank = lastCard.Rank;

            PlayingCard.TrumpSuit = lastCard.Suit;  // set trump suit

            // Get players' cards with a trump suit and the lowest rank
            PlayingCard humanCard = game.GetLowestTrump(game.Human.PlayHand);      
            PlayingCard computerCard = game.GetLowestTrump(game.Computer.PlayHand);

            // Determine whether the computer attacks first
            computerAttacks = true;
            if (humanCard.Suit == PlayingCard.TrumpSuit && computerCard.Suit != PlayingCard.TrumpSuit)
                computerAttacks = false;
            else if (humanCard.Suit != PlayingCard.TrumpSuit && computerCard.Suit == PlayingCard.TrumpSuit)
                computerAttacks = true;
            else if (humanCard < computerCard)
                computerAttacks = false;

            // Set number of cards in players' hands
            playerHandCount = numberOfCardsInHand;      
            computerHandCount = numberOfCardsInHand;

            // Update the logs file
            UpdateLogs("Trump card: " + lastCard.DebugString());
            UpdateLogs("");
            UpdateLogs("------------------------------------------------------------------------------------------------------------------");


            // If computer attacks, disable the Ready button and call the ComputerAttacks() method
            if (computerAttacks)
            {
                btnReady.Enabled = false;
                ComputerAttacks();
            }
            // If player attacks, disable the Take button
            else { btnTake.Enabled = false; }
        }

        /// <summary>
        /// Sets card's location in the main playing area
        /// </summary>
        /// <param name="control"></param>
        private void AddCardToPlayArea(Control control)
        {
            const int STARTING_POINT = 16;          // 1st card's location
            const int HORIZONTAL_INCREASE = 111;    // horizontal location increase
            const int VERTICAL_INCREASE = 5;        // vertical location increase

            // Set the card's location, depending on the number of cards on the table
            if (numberOfCardsInPlay % 2 == 0)
                control.Location = new Point(STARTING_POINT + (HORIZONTAL_INCREASE * (numberOfCardsInPlay / 2 - 1)), STARTING_POINT * VERTICAL_INCREASE);
            else
                control.Location = new Point(STARTING_POINT + (HORIZONTAL_INCREASE * (numberOfCardsInPlay / 2)), STARTING_POINT);
            // Bring card to the front of the panel
            control.BringToFront();
        }

        /// <summary>
        /// Deal cards to players
        /// </summary>
        private void DealCards()
        {
            bool dealMoreCards = false; // indicates whether more cards should be distributed to players

            // If player was attacking, deal cards to the player first, then the computer
            if (btnReady.Enabled)
            {
                dealMoreCards = DealCardsToPlayer(dealMoreCards);
                dealMoreCards = DealCardsToComputer(dealMoreCards);
            }
            // Otherwise, deal cards to the computer first, then then player
            else
            {
                dealMoreCards = DealCardsToComputer(dealMoreCards);
                dealMoreCards = DealCardsToPlayer(dealMoreCards);
            }

            // If there's 1 card left in the deck, hide deck from the table
            if (myDealer.Count == 1)
            {
                pbDeck.Visible = false;
                lblDeck.Visible = false;
            }
            // If there's no cards left in the deck, hide a deck and a card with trump suit from the table
            else if (myDealer.Count == 0)
            {
                pbDeck.Visible = false;
                pbTrump.Visible = false;
                lblDeck.Visible = false;
            }

            // Realign cards in hands
            RealignCards(pnlCPUHand);
            RealignCards(pnlPlayerHand);

            // If players need more cards, deal more cards
            if (dealMoreCards) DealCards();
        }

        /// <summary>
        /// Method that adds cards to the player's hand
        /// </summary>
        /// <param name="dealMoreCards"></param>
        /// <returns></returns>
        private bool DealCardsToPlayer(bool dealMoreCards)
        {
            // If player needs more cards:
            if (game.Human.PlayHand.Count < numberOfCardsInHand && myDealer.Count != 0)
            {
                dealMoreCards = true;   // set dealMoreCards value to true
                PlayingCard playerPlayingCard = myDealer.DrawNextCard();    // get the next card in the deck

                // Instantiate a new CardBox object
                CardBox playerCard = new CardBox(playerPlayingCard);
                playerCard.FaceUp = true;
                playerCard.BackColor = Color.Transparent;

                AddRemoveEventHandlers(playerCard.Controls[0], true);   // add event handlers to the CardBox

                // Add a card to the player's hand
                pnlPlayerHand.Controls.Add(playerCard);
                game.Human.PlayHand.Add(playerPlayingCard);
            }
            return dealMoreCards;
        }

        /// <summary>
        /// Method that adds cards to the computer's hand
        /// </summary>
        /// <param name="dealMoreCards"></param>
        /// <returns></returns>
        private bool DealCardsToComputer(bool dealMoreCards)
        {
            // If computer needs more cards:
            if (game.Computer.PlayHand.Count < numberOfCardsInHand && myDealer.Count != 0)
            {
                dealMoreCards = true;   // set dealMoreCards value to true

                PlayingCard computerPlayingCard = myDealer.DrawNextCard();  // get the next card in the deck

                // Instantiate a new CardBox object
                CardBox computerCard = new CardBox(computerPlayingCard);
                computerCard.Card = new PlayingCard();
                computerCard.BackColor = Color.Transparent;

                // Add a card to the computer's hand
                pnlCPUHand.Controls.Add(computerCard);
                game.Computer.PlayHand.Add(computerPlayingCard);
            }
            return dealMoreCards;
        }

        /// <summary>
        /// Method that implements computer attack logic
        /// </summary>
        private void ComputerAttacks()
        {
            // If player takes the cards on the table, increase the number of cards on the table
            if (playerTakes) numberOfCardsInPlay++;

            // If number of cards on the table is less than a limit:
            if (numberOfCardsInPlay < playerHandCount * 2)
            {
                // Attempt to get a card to attack with
                PlayingCard attackCard = game.ComputerAttacks();

                // Instantiate a new CardBox object
                CardBox playCard = new CardBox(attackCard);
                playCard.FaceUp = true;
                playCard.BackColor = Color.Transparent;

                // If computer found a card to attack with:
                if (game.ComputerFoundCard)
                {
                    UpdateLogs("Computer attacks with " + attackCard.DebugString());
                    // Remove a card from computer's hand, add it to the table
                    pnlCPUHand.Controls.RemoveAt(0);
                    pnlPlayArea.Controls.Add(playCard);
                    // Realign cards in computer's hand
                    RealignCards(pnlCPUHand);
                    // Increase the number of cards on the table
                    numberOfCardsInPlay++;
                    // Set card's location on the table
                    AddCardToPlayArea(pnlPlayArea.Controls[pnlPlayArea.Controls.Count - 1]);
                    // Add card to the list of cards in play
                    game.CardsInPlay.Add(attackCard);
                    // If player takes cards on the table, attack again
                    if (playerTakes) ComputerAttacks();
                }
                // If computer didn't find a card to attack with, start a new round
                else
                {
                    if (!gameOver)
                    {
                        computerAttacks = false;
                        NewRound();
                    }
                }
            }
            // If number of cards on the table is greater than a limit, start a new round
            else
            {
                if (!gameOver)
                {
                    computerAttacks = false;
                    NewRound();
                }
            }
        }

        /// <summary>
        /// Method that starts a new round
        /// </summary>
        private void NewRound()
        {
            // If player takes cards on the table, call PlayerPicksUp() method
            if (playerTakes) PlayerPicksUp();

            numberOfCardsInPlay = 0;        // reset the number of cards on the table
            game.ComputerPicksUp = false;   // set ComputerPicksUp property to false
            game.CardsInPlay.Clear();       // remove all cards from the list of cards in play

            // Remove all cards from the main playing area
            for (int index = 0; pnlPlayArea.Controls.Count > 3;)
                pnlPlayArea.Controls.RemoveAt(index);

            // Update the logs file
            UpdateLogs("");
            UpdateLogs("NEW ROUND");
            UpdateLogs("------------------------------------------------------------------------------------------------------------------");

            DealCards();                    // deal cards to players
            // Update the logs file
            UpdateLogs(DisplayCardsInHand("Computer's hand: ", game.Computer));
            UpdateLogs(DisplayCardsInHand("Player's hand: ", game.Human));
            UpdateLogs("");

            // If player has more than 6 cards, set the number of cards in hand to 6
            if (game.Human.PlayHand.Count >= numberOfCardsInHand)
                playerHandCount = numberOfCardsInHand;
            // Otherwise, set the number of cards in hand to the actual number of player's cards
            else
                playerHandCount = game.Human.PlayHand.Count;

            // If computer has more than 6 cards, set the number of cards in hand to 6
            if (game.Computer.PlayHand.Count >= numberOfCardsInHand)
                computerHandCount = numberOfCardsInHand;
            // Otherwise, set the number of cards in hand to the actual number of computer's cards
            else
                computerHandCount = game.Computer.PlayHand.Count;

            // If player took cards, computer attacks
            if (playerTakes)
            {
                playerTakes = false;
                computerAttacks = true;
                ComputerAttacks();
            }
            // Re-enable Take and Ready buttons
            ReenableButtons();
            // Determine whether the game is over
            EndOfGame();
        }

        /// <summary>
        /// Method that implements computer defense logic
        /// </summary>
        private void ComputerDefends()
        {
            // If computer doesn't take cards:
            if (!game.ComputerPicksUp)
            {
                // If computer ran out of cards:
                if (game.Computer.PlayHand.Count == 0)
                {
                    NewRound();         // start a new round
                    if (!gameOver) ComputerAttacks();  // computer attacks if the game isn't over
                }
                // If computer has cards:
                else
                {
                    // Attempt to get a card to defend with
                    PlayingCard defendCard = game.ComputerDefends();
                    // If computer has a card to defend with:
                    if (!game.ComputerPicksUp)
                    {
                        UpdateLogs("Computer defends with " + defendCard.DebugString());    // Update the logs file

                        numberOfCardsInPlay++;  // increase number of cards in play

                        // Instantiate a new CardBox object
                        CardBox newControl = new CardBox(defendCard);
                        newControl.FaceUp = true;
                        newControl.BackColor = Color.Transparent;

                        // Add a card to the main playing area and set its location
                        pnlPlayArea.Controls.Add(newControl);
                        AddCardToPlayArea(pnlPlayArea.Controls[pnlPlayArea.Controls.Count - 1]);

                        // Remove a card from the computer's hand and realign cards
                        pnlCPUHand.Controls.RemoveAt(0);
                        RealignCards(pnlCPUHand);

                        game.CardsInPlay.Add(defendCard);   // add a card to the list of cards in play

                        // If number of cards in play is greater than a limit:
                        if (numberOfCardsInPlay >= computerHandCount * 2)
                        {
                            NewRound();         // start a new round
                            ComputerAttacks();  // computer attacks
                        }
                    }
                }
            }
            // If number of cards in play is greater than a limit:
            else if (numberOfCardsInPlay >= computerHandCount * 2 - 1)
            {
                computerAttacks = false;
                ComputerPicksUp();  // computer takes the cards on the table
                NewRound();         // start a new round
            }
            // Determine whether the game is over
            EndOfGame();
        }

        /// <summary>
        /// Method that is called when player decides to take cards
        /// </summary>
        private void PlayerPicksUp()
        {
            UpdateLogs("Player picks up the cards from the table.");    // Update the logs file
            // Loop through the cards on the table
            for (int index = 0; index < game.CardsInPlay.Count;)
            {
                // Add a card to the player's hand
                game.Human.PlayHand.Add(game.CardsInPlay[index]);
                // Instantiate a new CardBox object 
                CardBox card = new CardBox(game.CardsInPlay[index]);
                card.Card = game.CardsInPlay[index];
                card.FaceUp = true;
                // Add event handlers to the CardBox
                AddRemoveEventHandlers(card.Controls[0], true);
                // Add a card to the player's hand, remove it from the list of cards in play
                pnlPlayerHand.Controls.Add(card);
                game.CardsInPlay.RemoveAt(index);
            }
            // Realign cards in the player's hand
            RealignCards(pnlPlayerHand);
        }

        /// <summary>
        /// Method that is called when computer decides to take cards
        /// </summary>
        private void ComputerPicksUp()
        {
            UpdateLogs("Computer picks up the cards from the table.");  // Update the logs file
            // Loop through the cards on the table
            for (int index = 0; index < game.CardsInPlay.Count;)
            {
                // Add a card to the computer's hand
                game.Computer.PlayHand.Add(game.CardsInPlay[index]);
                // Instantiate a new CardBox object 
                CardBox card = new CardBox(game.CardsInPlay[index]);
                card.Card = game.CardsInPlay[index];
                card.FaceUp = false;
                // Add a card to the computer's hand, remove it from the list of cards in play
                pnlCPUHand.Controls.Add(card);
                game.CardsInPlay.RemoveAt(index);
            }
            // Realign cards in the computer's hand
            RealignCards(pnlCPUHand);
        }

        /// <summary>
        /// Determines whether the game is over and the result of the game
        /// </summary>
        private void EndOfGame()
        {
            // If a card with a trump suit was hidden:
            if (!gameOver && !pbTrump.Visible)
            {
                // If both players have no cards, it's a draw
                if (game.Computer.PlayHand.Count == 0 && game.Human.PlayHand.Count == 0)
                {
                    DisplayResult(DRAW, Color.Green);        // display the result
                    lblDrawCount.Text = (++numberOfDraws).ToString();
                    UpdateLogs("Game ends in a draw.");      // Update the logs file
                }
                // If computer has no cards, player has lost
                else if (game.Computer.PlayHand.Count == 0)
                {
                    DisplayResult(PLAYER_LOST, Color.Black); // display the result
                    lblLossCount.Text = (++numberOfLosses).ToString();
                    UpdateLogs("Player loses the game.");    // Update the logs file
                }
                // If player has no cards, player has won
                else if (game.Human.PlayHand.Count == 0)
                {
                    DisplayResult(PLAYER_WON, Color.Red);    // display the result
                    lblWinsCount.Text = (++numberOfWins).ToString();
                    UpdateLogs("Player wins the game.");     // Update the logs file
                }
            }
        }

        /// <summary>
        /// Displays the game's result for the user
        /// </summary>
        /// <param name="result"></param>
        /// <param name="colour"></param>
        private void DisplayResult(string result, Color colour)
        {
            gameOver = true;    // set the gameOver value to true
            // Remove event handlers from player's cards
            for (int index = 0; index < pnlPlayerHand.Controls.Count; index++)
                AddRemoveEventHandlers(pnlPlayerHand.Controls[index].Controls[0], false);
            // Disable Ready and Take buttons
            btnReady.Enabled = false;
            btnTake.Enabled = false;
            // Display the games' count
            lblGamesPlayedCount.Text = (++numberOfGames).ToString();
            // Add the Result label to the main playing area
            pnlPlayArea.Controls.Add(lblResult);
            // Display the Result label
            lblResult.Show();
            lblResult.Text = result;
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            lblResult.ForeColor = colour;
            lblResult.BringToFront();
        }

        /// <summary>
        /// Re-enables Take and Ready buttons
        /// </summary>
        private void ReenableButtons()
        {
            // If computer defends
            if (!computerAttacks)
            {
                btnTake.Enabled = false;    // disable the Take button
                btnReady.Enabled = true;    // enable the Ready button
            }
            // If computer attacks
            else
            {
                btnTake.Enabled = true;     // enable the Take button
                btnReady.Enabled = false;   // disable the Ready button
            }
        }

        /// <summary>
        /// Adds or removes CardBox event handlers from a control
        /// </summary>
        /// <param name="card"></param>
        /// <param name="add"></param>
        private void AddRemoveEventHandlers(Control card, bool add)
        {
            // Add event handlers
            if (add)
            {
                card.MouseDown += CardBox_MouseDown;
                card.DragEnter += CardBox_DragEnter;
                card.DragDrop += CardBox_DragDrop;
                card.MouseEnter += CardBox_MouseEnter;
                card.MouseLeave += CardBox_MouseLeave;
            }
            // Remove event handlers
            else
            {
                card.MouseDown -= CardBox_MouseDown;
                card.DragEnter -= CardBox_DragEnter;
                card.DragDrop -= CardBox_DragDrop;
                card.MouseEnter -= CardBox_MouseEnter;
                card.MouseLeave -= CardBox_MouseLeave;
            }
        }

        /// <summary>
        /// Check if the line read from the file is null
        /// </summary>
        /// <param name="line"></param>
        /// <param name="failedString"></param>
        /// <returns></returns>
        private String GetStatistics(string line, string failedString)
        {
            // Return the line if it's not null
            if (line != null)
                return line;
            // Otherwise, return an alternative value
            else
                return failedString;
        }

        /// <summary>
        /// Methos that writes to the statistics.txt file
        /// </summary>
        private void UpdateStatistics()
        {
            // Attempt to write to the file
            try
            {
                StreamWriter sw = new StreamWriter(statisticsFile, false);  // open the file
                // Write player's name and statistics values
                sw.WriteLine(lblPlayerHand.Text);
                sw.WriteLine(lblGamesPlayedCount.Text);
                sw.WriteLine(lblWinsCount.Text);
                sw.WriteLine(lblLossCount.Text);
                sw.WriteLine(lblDrawCount.Text);
                // Close the file
                sw.Close();
            }
            // Catch any exceptions occured
            catch (Exception ex) { System.Diagnostics.Debug.Print(ex.ToString()); }
        }

        /// <summary>
        /// Methos that writes to the logs.txt file
        /// </summary>
        /// <param name="line"></param>
        private void UpdateLogs(String line)
        {
            // Attempt to write to the file
            try
            {
                StreamWriter sw = new StreamWriter(logFile, true);  // open the file
                sw.WriteLine(line); // write a line to the file
                sw.Close();         // close the file
            }
            // Catch any exceptions occured
            catch (Exception ex) { System.Diagnostics.Debug.Print(ex.ToString()); }
        }

        /// <summary>
        /// Mehtod that opens the NamePromptForm
        /// </summary>
        private void OpenNamePromptForm()
        {
            // Open the form
            frmNamePrompt nameForm = new frmNamePrompt();
            nameForm.ShowDialog();
            // If a user didn't enter any name, get the player's default name
            if (nameForm.PlayerName == String.Empty)
                lblPlayerHand.Text = DEFAULT_PLAYER_NAME;
            // Otherwise, get their name from the form
            else
                lblPlayerHand.Text = nameForm.PlayerName.Trim() + "'s Hand";
        }

        /// <summary>
        /// Method that gets players' cards and converts them to string
        /// </summary>
        /// <param name="line"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private string DisplayCardsInHand(string line, Player player)
        {
            // Loop through the player's cards, and add them to the line to return
            for (int index = 0; index < player.PlayHand.Count - 1; index++)
                line += player.PlayHand[index].DebugString() + ", ";
            if (player.PlayHand.Count > 0)
                line += player.PlayHand[player.PlayHand.Count - 1].DebugString() + ".";
            // return the line
            return line;
        }
        #endregion
    }
}