/**
 * MainForm.cs - The MainForm of the DurakGameUI project
 * 
 * This project is used to play the Durak card game.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-25
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
using System.Diagnostics;

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
        static private Size regularSize = new Size(75, 107);

        private Cards myDealer = new Cards(new DurakDeck());

        static private int numberOfCardsInHand = 6;

        /// <summary>
        /// Refers to the card being dragged from one panel to another.
        /// </summary>
        private CardBox dragCard;
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
        #endregion
        
        #region CardBox Event Handlers

        #endregion

        #region Helper Methods
        private void StartGame()
        {
            for (int index = 0; index < numberOfCardsInHand; index++)
            {
                CardBox computerCard = new CardBox(myDealer.DrawNextCard());
                CardBox playerCard = new CardBox(myDealer.DrawNextCard());
                
                playerCard.FaceUp = true;
                computerCard.Card = new PlayingCard();   

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
                // Aligning the cards: Note that I align them in reserve order from how they
                // are stored in the controls collection. This is so that cards on the left 
                // appear underneath cards to the right. This allows the user to see the rank
                // and suit more easily.

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
        #endregion
    }
}