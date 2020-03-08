/**
 * PlayingCard.cs - The PlayingCard class
 * 
 * Card class that holds information about a single playing card.
 * 
 * @author  Thom MacDonald
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 * @see     https://www.youtube.com/watch?v=xXohVJYKqjw&list=PLfNfAX7mRzNqDFJr-9UJZ6praJY10fXvY&index=2
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

namespace CardLibrary
{
    public class PlayingCard : ICloneable, IComparable
    {
        #region Fields and Properties
        /// <summary>
        /// Suit Property
        /// Used to set or get the Card Suit.
        /// </summary>
        protected CardSuit mySuit;
        public CardSuit Suit
        {
            get { return mySuit; }  // return the suit
            set { mySuit = value; } // set the suit
        }

        /// <summary>
        /// Rank Property
        /// Used to set or get the Card Rank.
        /// </summary>
        protected CardRank myRank;
        public CardRank Rank
        {
            get { return myRank; }  // return the rank
            set { myRank = value; } // set the rank
        }

        /// <summary>
        /// CardValue Property
        /// Used to set or get the Card Value.
        /// </summary>
        protected int myValue;
        public int CardValue
        {
            get { return myValue; }  // return the card value
            set { myValue = value; } // set the card value
        }

        /// <summary>
        /// Alternate Value Property
        /// Used to set or get the alternate value for certain games. Set to null by default
        /// </summary>
        protected int? altValue = null; // nullable type
        public int? AlternateValue
        {
            get { return altValue; }  // return the alternate card value
            set { altValue = value; } // set the alternate card value
        }

        /// <summary>
        /// FaceUp Property
        /// Used to set or get whether the card is face up
        /// </summary>
        protected bool faceUp = false;
        public bool FaceUp
        {
            get { return faceUp; }
            set { faceUp = value; }
        }

        /// <summary>
        /// UseTrumps Property
        /// Flag for trump usage. If true, trumps are valued higher than cards of other suits.
        /// </summary>
        protected static bool useTrumps = false;
        public static bool UseTrumps
        {
            get { return useTrumps; }
            set { useTrumps = value; }
        }

        /// <summary>
        /// TrumpSuit Property
        /// Trump suit to use if useTrumps is true.
        /// </summary>
        protected static CardSuit trumpSuit = CardSuit.Clubs;
        public static CardSuit TrumpSuit
        {
            get { return trumpSuit; }
            set { trumpSuit = value; }
        }

        /// <summary>
        /// IsAceHigh Property
        /// Flag that determines whether aces are higher than kings or lower than deuces.
        /// </summary>
        protected static bool isAceHigh = true;
        public static bool IsAceHigh
        {
            get { return isAceHigh; }
            set { isAceHigh = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Card Constructor
        /// Initializes the playing card object. By default, card is face down, useTrumps is false, isAceHigh is true, with no alternate value.
        /// </summary>
        /// <param name="rank">The playing card rank.</param>
        /// <param name="suit">The playing card suit.</param>
        public PlayingCard(CardRank rank = CardRank.Ace, CardSuit suit = CardSuit.Hearts)
        {
            // Set the rank and the suit
            myRank = rank;
            mySuit = suit;
            // Set the default card value
            myValue = (int)rank;
        }
        #endregion

        #region Relational Operators
        /// <summary>
        /// Determines if two cards are the same.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator ==(PlayingCard card1, PlayingCard card2)
        {
            return (card1.Suit == card2.Suit) && (card1.Rank == card2.Rank);
        }

        /// <summary>
        /// Determines if two cards are not the same.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator !=(PlayingCard card1, PlayingCard card2)
        {
            return !(card1 == card2);
        }

        /// <summary>
        /// Determines if the rank of one card is higher than the rank of another card.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator >(PlayingCard card1, PlayingCard card2)
        {
            // If 2 cards are of the same suit:
            if (card1.Suit == card2.Suit)
            {
                // If aces are higher than kings:
                if (isAceHigh)
                {
                    if (card1.Rank == CardRank.Ace)
                    {
                        // If both cards' ranks are aces, return false
                        if (card2.Rank == CardRank.Ace)
                            return false;
                        else  // Otherwise the rank of the 1st card is higher, return true
                            return true;
                    }
                    else // If the 1st card's rank isn't Ace
                    {
                        // If the rank of the 2nd card is Ace, return false
                        if (card2.Rank == CardRank.Ace)
                            return false;
                        // If none of the cards' ranks is Ace, return true if the 1st card's rank is higher, false otherwise
                        else
                            return (card1.Rank > card2.Rank);
                    }
                }
                // If aces are lower than deuces, return true if the 1st card's rank is higher, false otherwise
                else
                {
                    return (card1.Rank > card2.Rank);
                }
            }
            // If cards have different suits:
            else
            {
                // If the 2nd card's suit is a trump suit return false, true otherwise
                if (useTrumps && (card2.Suit == trumpSuit))
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Determines if the rank of one card is lower than the rank of another card.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator <(PlayingCard card1, PlayingCard card2)
        {
            return !(card1 >= card2);
        }

        /// <summary>
        /// Determines if the rank of one card is higher or equals to the rank of another card.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator >=(PlayingCard card1, PlayingCard card2)
        {
            // If 2 cards are of the same suit:
            if (card1.Suit == card2.Suit)
            {
                // If aces are higher than kings:
                if (isAceHigh)
                {
                    // If the rank of the 1st card is Ace, return true
                    if (card1.Rank == CardRank.Ace)
                    {
                        return true;
                    }
                    else  // If the rank of the 1st card is not Ace
                    {
                        // If the rank of the 2nd card is Ace, return false
                        if (card2.Rank == CardRank.Ace)
                            return false;
                        else // Otherwise, return the value based on the rank
                            return (card1.Rank >= card2.Rank);
                    }
                }
                else  // Return the value based on the rank
                {
                    return (card1.Rank >= card2.Rank);
                }
            }
            // If cards have different suits:
            else
            {
                // If the 2nd card's suit is a trump suit return false, true otherwise
                if (useTrumps && (card2.Suit == trumpSuit))
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Determines if the rank of one card is lower or equals to the rank of another card.
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator <=(PlayingCard card1, PlayingCard card2)
        {
            return !(card1 > card2);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// CompareTo Method
        /// Card-specific comparison method used to sort Card instances.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int CompareTo(object obj)
        {
            // is the argument null?
            if (obj == null)
            {
                // throw an argument null exception
                throw new ArgumentNullException("Unable to compare a Card to a null object");
            }
            // Convert the argument to a Card
            PlayingCard compareCard = obj as PlayingCard;
            // if conversion worked
            if (compareCard != null)
            {
                // compare based on Value first, then Suit
                int thisSort = myValue * 10 + (int)mySuit;
                int compareCardSort = compareCard.myValue * 10 + (int)compareCard.mySuit;
                return (thisSort.CompareTo(compareCardSort));
            }
            else // otherwise, the conversion failed
            {
                // throw an argument exception
                throw new ArgumentException("Object being compared cannot be converted to a Card.");
            }
        }

        /// <summary>
        /// Makes a shallow copy of the object.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// The overriden ToString method 
        /// </summary>
        /// <returns>the name of the card as a string</returns>
        public override string ToString()
        {
            string cardString;  // holds the playing card name
            // if the card is face up
            if (faceUp)
            {
                // if the card is a Joker
                if (myRank == CardRank.Joker)
                {
                    // set card name string to {Red|Black} Joker
                    // if the suit is black
                    if (mySuit == CardSuit.Clubs || mySuit == CardSuit.Spades)
                    {
                        // set the name string to black joker
                        cardString = "Black Joker";
                    }
                    else
                    {
                        // set the name string to red joker
                        cardString = "Red Joker";
                    }
                }
                // otherwise, the card is a face up but not a joker
                else
                {
                    // set the card name string to {Rank} of {Suit}
                    cardString = myRank.ToString() + " of " + mySuit.ToString();
                }
            }
            // otherwise, the card is face down
            else
            {
                // set the card name to face down
                cardString = "Face Down";
            }
            // return the appropriate card name string
            return cardString;
        }

        /// <summary>
        /// Equals: Overrides System.Object.Equals()
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if the card values are equal</returns>
        public override bool Equals(object obj)
        {
            return this == (PlayingCard)obj;
        }

        /// <summary>
        /// GetHashCode: Overrides System.Object.GetHashCode()
        /// </summary>
        /// <returns>Card value *10 + Suit number</returns>
        public override int GetHashCode()
        {
            return myValue * 100 + (int)mySuit * 10 + (faceUp ? 1 : 0);
        }

        /// <summary>
        /// GetCardImage
        /// Gets the image associated with the card from the resource file
        /// </summary>
        /// <returns>an Image corresponding to the playing card</returns>
        public Image GetCardImage()
        {
            string imageName;   // name of the image in the resources file
            Image cardImage;    // holds the image
            // if the card is not face up
            if (!faceUp)
            {
                // set the image name to "Back"
                imageName = "Back"; // sets it to the image name for the back of a card
            }
            else if (myRank == CardRank.Joker)  // if the card is a joker
            {
                // if the suit is black
                if (mySuit == CardSuit.Clubs || mySuit == CardSuit.Spades)
                {
                    // set the image to black joker
                    imageName = "Black_Joker";
                }
                else  // the suit must be red
                {
                    // set the image to red joker
                    imageName = "Red_Joker";
                }
            }
            else  // otherwise, the card is face up and not a joker
            {
                // set the image name to {Suit}_{Rank}
                imageName = mySuit.ToString() + "_" + myRank.ToString();
            }
            // Set the image to the appropriate object from the resources file
            cardImage = Properties.Resources.ResourceManager.GetObject(imageName) as Image;
            // return the image
            return cardImage;
        }

        /// <summary>
        /// DebugString
        /// Generates a string showing the state of the card object; useful for debug purposes.
        /// </summary>
        /// <returns>a string showing the state of this card object</returns>
        public string DebugString()
        {
            string cardState = (myRank.ToString() + " of " + mySuit.ToString()).PadLeft(20);
            cardState += ((FaceUp) ? "(Face Up)" : "(Face Down)").PadLeft(12);
            cardState += " Value: " + myValue.ToString().PadLeft(2);
            cardState += ((altValue != null) ? "/" + altValue.ToString() : "");
            return cardState;
        }
        #endregion
    }
}