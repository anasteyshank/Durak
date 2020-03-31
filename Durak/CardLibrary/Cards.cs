/**
 * Cards.cs - The Cards class
 * 
 * Cards class that holds a generic list of Card objects.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using System;
using System.Collections.Generic;

namespace CardLibrary
{
    public class Cards : List<PlayingCard>, ICloneable
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Cards()
        {

        }

        public Cards(Deck deck)
        {
            deck.Shuffle();

            int index = 0;
            bool isValid = true;

            while (isValid)
            {
                try
                {
                    Add(deck.GetCard(index++));
                }
                catch (Exception ex)
                {
                    isValid = false;
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method for copying card instances into another Cards instance.
        /// The source and target lists must be the same size.
        /// </summary>
        /// <param name="targetCards"></param>
        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

        /// <summary>
        /// Makes a clone of the Cards list
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Cards newCards = new Cards();
            foreach (PlayingCard sourceCard in this)
            {
                newCards.Add((PlayingCard)sourceCard.Clone());
            }
            return newCards;
        }

        public PlayingCard DrawNextCard()
        {
            PlayingCard returnCard = new PlayingCard();
            if (Count != 0)
            {
                returnCard = this[0];
                Remove(returnCard);
            }
            else
            {
                throw new ArgumentNullException("The Cards list is empty.");
            }
            return returnCard;
        }
        #endregion
    }
}