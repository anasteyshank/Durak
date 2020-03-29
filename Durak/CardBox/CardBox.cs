/**
 * CardBox.cs - The CardBox class
 * 
 * CardBox class represents a custom user control.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     https://www.youtube.com/watch?v=gK6bJ9IudW8&list=PLfNfAX7mRzNqDFJr-9UJZ6praJY10fXvY&index=3
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using CardLibrary;

namespace CardBox
{
    /// <summary>
    /// A control to use in a Windows Forms Application that displays a playing card.
    /// </summary>
    public partial class CardBox: UserControl
    {
        #region Fields and Properties
        /// <summary>
        /// Card Property: sets/gets the underlying Card object.
        /// </summary>
        private PlayingCard myCard;
        public PlayingCard Card
        {
            set
            {
                myCard = value;
                pbMyPictureBox.Image = myCard.GetCardImage();   // update the card image
                UpdateCardImage();  // update the card image
            }
            get { return myCard; }
        }

        /// <summary>
        /// Suit Property: sets/gets the underlying Card object's Suit.
        /// </summary>
        public CardSuit Suit
        {
            set
            {
                Card.Suit = value;
                UpdateCardImage();  // update the card image
            }
            get { return Card.Suit; }
        }

        /// <summary>
        /// Rank Property: sets/gets the underlying Card object's Rank.
        /// </summary>
        public CardRank Rank
        {
            set
            {
                Card.Rank = value;
                UpdateCardImage();  // update the card image
            }
            get { return Card.Rank; }
        }

        /// <summary>
        /// FaceUp Property: sets/gets the the underlying Card object's FaceUp property.
        /// </summary>
        public bool FaceUp
        {
            set
            {
                // if value is different than the underlying card's FaceUp property
                if (myCard.FaceUp != value) // then the card is flipped over
                {
                    myCard.FaceUp = value;  // change the card's FaceUp preperty

                    UpdateCardImage();      // update the card image (back or front)

                    // if there is an event handler for CardFlipped in the client program
                    if (CardFlipped != null)
                        CardFlipped(this, new EventArgs()); // call it
                }
            }
            get { return Card.FaceUp; }
        }

        /// <summary>
        /// CardOrientation Property: sets/gets the Orientation of the card.
        /// If setting this property changes the state of control, swaps
        /// the height and width of the control and updates the image.
        /// </summary>
        private Orientation myOrientation;
        public Orientation CardOrientation
        {
            set
            {
                // if value is different than myOrientation
                if (myOrientation != value)
                {
                    myOrientation = value;  // change the orientation
                    // swap the height and width of the control
                    Size = new Size(Size.Height, Size.Width);
                    UpdateCardImage();  // update the card image
                }
            }
            get { return myOrientation; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor (Default): Constructs the control with a new card, oriented vertically.
        /// </summary>
        public CardBox()
        {
            InitializeComponent();                  // required method for Designer support
            myOrientation = Orientation.Vertical;   // set the orientation to vertical
            myCard = new PlayingCard();             // create a new undetlying card
        }

        /// <summary>
        /// Constructor (PlayingCard, Orientation): Constructs the control using parameters.
        /// </summary>
        /// <param name="card">Underlying PlayingCard object</param>
        /// <param name="orientation">Orientation enumeration.Vertical by default.</param>
        public CardBox(PlayingCard card, Orientation orientation = Orientation.Vertical)
        {
            InitializeComponent();          // required method for Designer support
            myOrientation = orientation;    // set the orientation
            myCard = card;                  // set the underlying card
        }
        #endregion

        #region Events and Event Handlers
        /// <summary>
        /// An event handler for the load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardBox_Load(object sender, EventArgs e)
        {
            UpdateCardImage();  // update the card image
        }

        /// <summary>
        /// An event the client program can handle when the card flips face up/down
        /// </summary>
        public event EventHandler CardFlipped;

        /// <summary>
        /// An event the client program can handle when the user clicks the control
        /// </summary>
        new public event EventHandler Click;

        /// <summary>
        /// An event handler for the user clicking the picturebox control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMyPictureBox_Click(object sender, EventArgs e)
        {
            if (Click != null)      // if there is a handler for clicking the control in the client program
                Click(this, e);     // call it
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// UpdateCardImage Helper Method: sets the PictureBox image using
        /// the underlying card and the orientation
        /// </summary>
        private void UpdateCardImage()
        {
            // set the image using the underlying card
            pbMyPictureBox.Image = myCard.GetCardImage();

            // if the orientation is horizontal 
            if (myOrientation == Orientation.Horizontal)
            {
                // rotate the image
                pbMyPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }

        /// <summary>
        /// ToString: Overrides System.Object.ToString()
        /// </summary>
        /// <returns>the name of the card as a string</returns>
        public override string ToString()
        {
            return myCard.ToString();
        }
        #endregion
    }
}