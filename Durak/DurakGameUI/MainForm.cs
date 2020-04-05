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

        /// <summary>
        /// Refers to the card being dragged from one panel to another.
        /// </summary>
        private PictureBox dragCard;
        #endregion

        #region Form and Static Event Handlers
        /// <summary>
        /// Constructor for frmDurak
        /// </summary>
        public frmDurak()
        {
            InitializeComponent();
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

                        AddCardToPlayArea(thisPanel.Controls[thisPanel.Controls.Count - 1]);
                    }
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
        private void StartGame()
        {
            for (int index = 0; index < numberOfCardsInHand; index++)
            {
                CardBox computerCard = new CardBox(myDealer.DrawNextCard());
                CardBox playerCard = new CardBox(myDealer.DrawNextCard());

                computerCard.Card = new PlayingCard();
                playerCard.FaceUp = true;

                playerCard.BackColor = Color.Transparent;
                computerCard.BackColor = Color.Transparent;

                playerCard.Controls[0].MouseDown += CardBox_MouseDown;
                playerCard.Controls[0].DragEnter += CardBox_DragEnter;
                playerCard.Controls[0].DragDrop += CardBox_DragDrop;
                playerCard.Controls[0].MouseEnter += CardBox_MouseEnter;
                playerCard.Controls[0].MouseLeave += CardBox_MouseLeave;

                pnlCPUHand.Controls.Add(computerCard);
                pnlPlayerHand.Controls.Add(playerCard);
            }

            PlayingCard lastCard = myDealer[myDealer.Count - 1];
            pbTrump.FaceUp = true;
            pbTrump.Suit = lastCard.Suit;
            pbTrump.Rank = lastCard.Rank;

            PlayingCard.TrumpSuit = lastCard.Suit;

            RealignCards(pnlCPUHand);
            RealignCards(pnlPlayerHand);
        }

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

        private void AddCardToPlayArea(Control control)
        {
            if (numberOfCardsInPlay == 1)
            {
                control.Location = new Point(16, 16);
            }
            else if (numberOfCardsInPlay == 2)
            {
                control.Location = new Point(16, 16 * 5);
            }
            else if (numberOfCardsInPlay == 3)
            {
                control.Location = new Point(16 * 7, 16);
            }
            else if (numberOfCardsInPlay == 4)
            {
                control.Location = new Point(16 * 7, 16 * 5);
            }
            else if (numberOfCardsInPlay == 5)
            {
                control.Location = new Point(16 * 13, 16);
            }
            else if (numberOfCardsInPlay == 6)
            {
                control.Location = new Point(16 * 13, 16 * 5);
            }
            control.BringToFront();
        }
        #endregion
    }
}