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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace DurakLibrary
{
    class DurakDeck : Deck
    {
        #region Fields and Properties
        private static int lowestRankValue = 6; // the lowest possible rank in the deck
        #endregion

        #region Constructors
        /// <summary>
        /// Nondefault constructor. A standard constructor for the Durak deck.
        /// </summary>
        /// <param name="trump"></param>
        public DurakDeck() : base(false)
        {
            PlayingCard.IsAceHigh = true;   // set aces to be high
            PlayingCard.UseTrumps = true;   // use trump suits

            // remove cards with the ranks lower than the lowest possible rank
            foreach (PlayingCard card in cards)
            {
                if ((int)card.Rank < lowestRankValue && card.Rank != CardRank.Ace)
                {
                    cards.Remove(card);
                }
            }
        }
        #endregion
    }
}
