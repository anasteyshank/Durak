/**
 * DurakDeck.cs - The DurakDeck class
 * 
 * DurakDeck class that is used to play a Durak game.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-25
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using CardLibrary;

namespace DurakLibrary
{
    public class DurakDeck : Deck
    {
        #region Fields and Properties
        /// <summary>
        /// The lowest possible rank in the deck
        /// </summary>
        private static int lowestRankValue = 6;
        #endregion

        #region Constructors
        /// <summary>
        /// Nondefault constructor. A standard constructor for the Durak deck.
        /// </summary>
        /// <param name="trump"></param>
        public DurakDeck()
        {
            cards = new Cards();            // get a new cards' collection

            PlayingCard.IsAceHigh = true;   // set aces to be high
            PlayingCard.UseTrumps = true;   // use trump suits

            int numberOfRanks = 14; // number of ranks
            int rankVal = 1;        // 1st rank to add

            // Add aces to the Cards collection
            for (int suitVal = 0; suitVal < 4; suitVal++)
                cards.Add(new PlayingCard((CardRank)rankVal, (CardSuit)suitVal));

            // Add the rest of cards to the Cards collection
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (rankVal = lowestRankValue; rankVal < numberOfRanks; rankVal++)
                    cards.Add(new PlayingCard((CardRank)rankVal, (CardSuit)suitVal));
            }
        }
        #endregion
    }
}
