/**
 * MainForm.cs - The MainForm of the DurakGameUI project
 * 
 * This project is used to play the Durak card game.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-04-04
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
        /// The regular size of a CardBox control
        /// </summary>
        static private Size regularSize = new Size(94, 128);

        private Cards myDealer = new Cards(new DurakDeck());

        static private int numberOfCardsInHand = 6;

        private int numberOfCardsInPlay = 0;

        private int playerHandCount = 0;

        private int computerHandCount = 0;

        private bool computerAttacks = false;

        private bool playerTakes = false;

        private Game game;

        /// <summary>
        /// Refers to the card being dragged from one panel to another.
        /// </summary>
        private PictureBox dragCard;

        private const string DRAW = "DRAW!";
        private const string PLAYER_LOST = "YOU LOST:(";
        private const string PLAYER_WON = "YOU WON!";

        #endregion

        #region Form and Static Event Handlers
        /// <summary>
        /// Constructor for frmDurak
        /// </summary>
        public frmDurak()
        {
            InitializeComponent();
            lblResult.Hide();
        }

        /// <summary>
        /// Closes the current form
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDurak_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPickUp_Click(object sender, EventArgs e)
        {
            EndOfGame();
            playerTakes = true;
            ComputerAttacks();
            computerAttacks = true;
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            computerAttacks = true;
            if (game.ComputerPicksUp)
            {
                ComputerPicksUp();
                computerAttacks = false;
            }
            NewRound();
            if (computerAttacks)
            {
                ComputerAttacks();
            }
        }

        /// <summary>
        /// Make the mouse pointer a "move" pointer when a drag enters a Panel.
        /// </summary>
        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;  // Make the mouse pointer a "move" pointer
        }

        private void pnlPlayArea_DragEnter(object sender, DragEventArgs e)
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

        private void pnlPlayArea_DragDrop(object sender, DragEventArgs e)
        {
            // If there is a CardBox to move
            if (dragCard != null)
            {
                CardBox humanCardBox = (CardBox)dragCard.Parent;
                PlayingCard humanCard = new PlayingCard(humanCardBox.Rank, humanCardBox.Suit);
                if ((computerAttacks && humanCard > game.CardsInPlay[game.CardsInPlay.Count - 1] && 
                    (humanCard.Suit == game.CardsInPlay[game.CardsInPlay.Count - 1].Suit || humanCard.Suit == PlayingCard.TrumpSuit)) || 
                    (!computerAttacks && game.CanAttack(humanCard) && numberOfCardsInPlay < computerHandCount * 2) || 
                    (!computerAttacks && numberOfCardsInPlay == 0 && numberOfCardsInPlay < computerHandCount * 2))
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
                            RealignCards(fromPanel);

                            dragCard.MouseDown -= CardBox_MouseDown;
                            dragCard.DragEnter -= CardBox_DragEnter;
                            dragCard.DragDrop -= CardBox_DragDrop;
                            dragCard.MouseEnter -= CardBox_MouseEnter;
                            dragCard.MouseLeave -= CardBox_MouseLeave;

                            numberOfCardsInPlay++;

                            if (game.ComputerPicksUp)
                            {
                                numberOfCardsInPlay++;
                            }

                            game.Human.PlayHand.Remove(humanCard);

                            game.CardsInPlay.Add(humanCard);

                            AddCardToPlayArea(thisPanel.Controls[thisPanel.Controls.Count - 1]);
                        }
                    }

                    if (computerAttacks)
                    {
                        ComputerAttacks();
                    }
                    else
                    {
                        ComputerDefends();
                    }
                    EndOfGame();
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

        private void StartGame()
        {
            game = new Game();

            DealCards();

            // Last card in the deck
            PlayingCard lastCard = myDealer[myDealer.Count - 1];
            pbTrump.FaceUp = true;
            pbTrump.Suit = lastCard.Suit;
            pbTrump.Rank = lastCard.Rank;

            PlayingCard.TrumpSuit = lastCard.Suit;  // Set trump suit

            PlayingCard humanCard = game.GetLowestTrump(game.Human.PlayHand);
            PlayingCard computerCard = game.GetLowestTrump(game.Computer.PlayHand);
            computerAttacks = true;
            if (humanCard.Suit == PlayingCard.TrumpSuit && computerCard.Suit != PlayingCard.TrumpSuit)
            {
                computerAttacks = false;
            }
            else if (humanCard.Suit != PlayingCard.TrumpSuit && computerCard.Suit == PlayingCard.TrumpSuit)
            {
                computerAttacks = true;
            }
            else if (humanCard < computerCard)
            {
                computerAttacks = false;
            }

            if (game.Human.PlayHand.Count >= numberOfCardsInHand)
            {
                playerHandCount = numberOfCardsInHand;
            }
            else
            {
                playerHandCount = game.Human.PlayHand.Count;
            }
            if (game.Computer.PlayHand.Count >= numberOfCardsInHand)
            {
                computerHandCount = numberOfCardsInHand;
            }
            else
            {
                computerHandCount = game.Computer.PlayHand.Count;
            }

            if (computerAttacks)
            {
                btnReady.Enabled = false;
                ComputerAttacks();
            }
            else
            {
                btnTake.Enabled = false;
            }
        }

        private void AddCardToPlayArea(Control control)
        {
            const int STARTING_POINT = 16;
            const int HORIZONTAL_INCREASE = 111;
            const int VERTICAL_INCREASE = 5;

            if (numberOfCardsInPlay % 2 == 0)
            {
                control.Location = new Point(STARTING_POINT + (HORIZONTAL_INCREASE * (numberOfCardsInPlay / 2 - 1)), STARTING_POINT * VERTICAL_INCREASE);
            }
            else
            {
                control.Location = new Point(STARTING_POINT + (HORIZONTAL_INCREASE * (numberOfCardsInPlay / 2)), STARTING_POINT);
            }
            control.BringToFront();
        }

        private void DealCards()
        {
            bool dealMoreCards = false;

            if (game.Human.PlayHand.Count < numberOfCardsInHand && myDealer.Count != 0)
            {
                dealMoreCards = true;
                PlayingCard playerPlayingCard = myDealer.DrawNextCard();
                CardBox playerCard = new CardBox(playerPlayingCard);

                playerCard.FaceUp = true;
                playerCard.BackColor = Color.Transparent;

                playerCard.Controls[0].MouseDown += CardBox_MouseDown;
                playerCard.Controls[0].DragEnter += CardBox_DragEnter;
                playerCard.Controls[0].DragDrop += CardBox_DragDrop;
                playerCard.Controls[0].MouseEnter += CardBox_MouseEnter;
                playerCard.Controls[0].MouseLeave += CardBox_MouseLeave;

                pnlPlayerHand.Controls.Add(playerCard);
                game.Human.PlayHand.Add(playerPlayingCard);
            }

            if (game.Computer.PlayHand.Count < numberOfCardsInHand && myDealer.Count != 0)
            {
                dealMoreCards = true;

                PlayingCard computerPlayingCard = myDealer.DrawNextCard();
                CardBox computerCard = new CardBox(computerPlayingCard);

                computerCard.Card = new PlayingCard();
                computerCard.BackColor = Color.Transparent;

                pnlCPUHand.Controls.Add(computerCard);
                game.Computer.PlayHand.Add(computerPlayingCard);
            }

            if (myDealer.Count == 1)
            {
                pbDeck.Visible = false;
                lblDeck.Visible = false;
            }
            else if (myDealer.Count == 0)
            {
                pbDeck.Visible = false;
                pbTrump.Visible = false;
                lblDeck.Visible = false;
            }

            // Realign cards in the hands
            RealignCards(pnlCPUHand);
            RealignCards(pnlPlayerHand);

            if (dealMoreCards)
            {
                DealCards();
            }
        }

        private void ComputerAttacks()
        {
            if (playerTakes)
            {
                numberOfCardsInPlay++;
            }

            if (numberOfCardsInPlay < playerHandCount * 2)
            {
                PlayingCard attackCard = game.ComputerAttacks();
                CardBox playCard = new CardBox(attackCard);
                playCard.FaceUp = true;
                playCard.BackColor = Color.Transparent;

                if (game.ComputerFoundCard)
                {
                    pnlCPUHand.Controls.RemoveAt(0);
                    pnlPlayArea.Controls.Add(playCard);

                    RealignCards(pnlCPUHand);

                    numberOfCardsInPlay++;

                    AddCardToPlayArea(pnlPlayArea.Controls[pnlPlayArea.Controls.Count - 1]);

                    game.CardsInPlay.Add(attackCard);

                    if (playerTakes)
                    {
                        ComputerAttacks();
                    }
                }
                else
                {
                    computerAttacks = false;
                    NewRound();
                }
            }
            else
            {
                computerAttacks = false;
                NewRound();
            }
        }

        private void NewRound()
        {
            if (playerTakes)
            {
                PlayerPicksUp();
            }

            numberOfCardsInPlay = 0;
            game.ComputerPicksUp = false;
            for (int index = 0; pnlPlayArea.Controls.Count > 3;)
            {
                pnlPlayArea.Controls.RemoveAt(index);
            }
            game.CardsInPlay.Clear();
            DealCards();

            if (game.Human.PlayHand.Count >= numberOfCardsInHand)
            {
                playerHandCount = numberOfCardsInHand;
            }
            else
            {
                playerHandCount = game.Human.PlayHand.Count;
            }

            if (game.Computer.PlayHand.Count >= numberOfCardsInHand)
            {
                computerHandCount = numberOfCardsInHand;
            }
            else
            {
                computerHandCount = game.Computer.PlayHand.Count;
            }

            if (playerTakes)
            {
                playerTakes = false;
                computerAttacks = true;
                ComputerAttacks();
            }
            ReenableButtons();
        }

        private void ComputerDefends()
        {
            if (!game.ComputerPicksUp)
            {
                PlayingCard defendCard = game.ComputerDefends();
                if (!game.ComputerPicksUp)
                {
                    numberOfCardsInPlay++;

                    CardBox newControl = new CardBox(defendCard);
                    newControl.FaceUp = true;
                    newControl.BackColor = Color.Transparent;

                    pnlPlayArea.Controls.Add(newControl);
                    AddCardToPlayArea(pnlPlayArea.Controls[pnlPlayArea.Controls.Count - 1]);

                    pnlCPUHand.Controls.RemoveAt(0);
                    RealignCards(pnlCPUHand);

                    game.CardsInPlay.Add(defendCard);

                    if (numberOfCardsInPlay >= computerHandCount * 2)
                    {
                        NewRound();
                        ComputerAttacks();
                    }
                }
            }
            else
            {
                if (numberOfCardsInPlay >= computerHandCount * 2)
                {
                    computerAttacks = false;
                    ComputerPicksUp();
                }
            }
            EndOfGame();
        }

        private void PlayerPicksUp()
        {
            for (int index = 0; index < game.CardsInPlay.Count;)
            {
                game.Human.PlayHand.Add(game.CardsInPlay[index]);
                CardBox card = new CardBox(game.CardsInPlay[index]);

                card.Card = game.CardsInPlay[index];
                card.FaceUp = true;

                card.Controls[0].MouseDown += CardBox_MouseDown;
                card.Controls[0].DragEnter += CardBox_DragEnter;
                card.Controls[0].DragDrop += CardBox_DragDrop;
                card.Controls[0].MouseEnter += CardBox_MouseEnter;
                card.Controls[0].MouseLeave += CardBox_MouseLeave;

                pnlPlayerHand.Controls.Add(card);
                game.CardsInPlay.RemoveAt(index);
            }
            RealignCards(pnlPlayerHand);
        }

        private void ComputerPicksUp()
        {
            for (int index = 0; index < game.CardsInPlay.Count;)
            {
                game.Computer.PlayHand.Add(game.CardsInPlay[index]);
                CardBox card = new CardBox(game.CardsInPlay[index]);

                card.Card = game.CardsInPlay[index];
                card.FaceUp = false;

                pnlCPUHand.Controls.Add(card);
                game.CardsInPlay.RemoveAt(index);
            }
            RealignCards(pnlPlayerHand);
        }

        private void EndOfGame()
        {
            if (!pbTrump.Visible)
            {
                if (game.Computer.PlayHand.Count == 0 && game.Human.PlayHand.Count == 0)
                {
                    GameOver(DRAW, Color.Green);
                }
                else if (game.Computer.PlayHand.Count == 0)
                {
                    GameOver(PLAYER_LOST, Color.Black);
                }
                else if (game.Human.PlayHand.Count == 0)
                {
                    GameOver(PLAYER_WON, Color.Red);
                }
            } 
        }

        private void GameOver(string result, Color colour)
        {
            btnReady.Enabled = false;
            btnTake.Enabled = false;

            pnlPlayArea.Controls.Add(lblResult);

            lblResult.Show();
            lblResult.Text = result;
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            lblResult.ForeColor = colour;
            Update();
            lblResult.BringToFront();

            for (int index = 0; index < pnlPlayerHand.Controls.Count; index++)
            {
                pnlPlayerHand.Controls[0].MouseDown -= CardBox_MouseDown;
                pnlPlayerHand.Controls[0].DragEnter -= CardBox_DragEnter;
                pnlPlayerHand.Controls[0].DragDrop -= CardBox_DragDrop;
                pnlPlayerHand.Controls[0].MouseEnter -= CardBox_MouseEnter;
                pnlPlayerHand.Controls[0].MouseLeave -= CardBox_MouseLeave;
            }
        }

        private void ReenableButtons()
        {
            if (!computerAttacks)
            {
                btnTake.Enabled = false;
                btnReady.Enabled = true;
            }
            else
            {
                btnTake.Enabled = true;
                btnReady.Enabled = false;
            }
        }
        #endregion
    }
}