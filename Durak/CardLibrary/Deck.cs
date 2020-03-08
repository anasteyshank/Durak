/**
 * Deck.cs - The Deck class
 * 
 * Deck class that holds 52 cards and is able to shuffle them.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using System;

namespace CardLibrary
{
    public class Deck : ICloneable
    {
        #region Fields and Properties
        private Cards cards = new Cards();      // a Cards collection
        private static bool useJokers = false;  // flag for jokers usage
        #endregion

        #region Constructors
        /// <summary>
        /// Nondefault constructor. Allows to include jokers.
        /// </summary>
        public Deck(bool includeJokers = false)
        {
            useJokers = includeJokers;  // set the useJokers field

            int numberOfRanks = 14;

            if (useJokers) numberOfRanks++; // increase the number of ranks if jokers are included

            // Add every card in the deck to the Cards collection
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < numberOfRanks; rankVal++)
                {
                    cards.Add(new PlayingCard((CardRank)rankVal, (CardSuit)suitVal));
                }
            }
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high.
        /// </summary>
        /// <param name="isAceHigh"></param>
        public Deck(bool isAceHigh, bool includeJokers = false) : this(includeJokers)
        {
            PlayingCard.IsAceHigh = isAceHigh;
        }

        /// <summary>
        /// Nondefault constructor. Allows a trump suit to be used.
        /// </summary>
        /// <param name="useTrumps"></param>
        /// <param name="trump"></param>
        public Deck(bool useTrumps, CardSuit trump, bool includeJokers = false) : this(includeJokers)
        {
            PlayingCard.UseTrumps = useTrumps;
            PlayingCard.TrumpSuit = trump;
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high and a trump suit to be used.
        /// </summary>
        /// <param name="isAceHigh"></param>
        /// <param name="useTrumps"></param>
        /// <param name="trump"></param>
        public Deck(bool isAceHigh, bool useTrumps, CardSuit trump, bool includeJokers = false) : this(includeJokers)
        {
            PlayingCard.IsAceHigh = isAceHigh;
            PlayingCard.UseTrumps = useTrumps;
            PlayingCard.TrumpSuit = trump;
        }

        /// <summary>
        /// Private Copy constructor
        /// </summary>
        /// <param name="newCards"></param>
        private Deck(Cards newCards)
        {
            cards = newCards;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the Card object with the requested index if the index is valid;
        /// otherwise, throws an exception
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public PlayingCard GetCard(int cardNum)
        {
            // Return the requested card if the index is within the range
            if (cardNum >= 0 && cardNum <= 51)
                return cards[cardNum];
            // Otherwise, throw an exception
            else
                throw new CardOutOfRangeException(cards.Clone() as Cards);
        }

        /// <summary>
        /// The method that shuffles the cards in the deck
        /// </summary>
        public void Shuffle()
        {
            Cards newDeck = new Cards();        // temporary collection of cards
            bool[] assigned = new bool[52];     // indicates if the spot is taken by the card
            Random sourceGen = new Random();    // generates a random number 
            // Loop through the deck of cards
            for (int i = 0; i < 52; i++)
            {
                int sourceCard = 0;       // index of the destination card
                bool foundCard = false;   // indicates if the spot was found
                // while the card isn't found
                while (!foundCard)
                {
                    sourceCard = sourceGen.Next(52);    // generate a random number from 1 to 52
                    // if the spot is found, quit the loop
                    if (assigned[sourceCard] == false)
                        foundCard = true;
                }
                // add the card to the temporary collection
                assigned[sourceCard] = true;
                newDeck.Add(cards[sourceCard]);
            }
            // set the original array to the new one
            newDeck.CopyTo(cards);
        }

        /// <summary>
        /// Clones the deck
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }
        #endregion
    }
}