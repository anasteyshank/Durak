/**
 * CardOutOfRangeException.cs - The Deck class
 * 
 * CardOutOfRangeException class that defines a custom exception for the Card class.
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
    class CardOutOfRangeException : Exception
    {
        #region Fields and Properties
        private Cards deckContents;  // holds the deck of cards (the Cards object)
        /// <summary>
        /// Gets the Cards object stored in the class
        /// </summary>
        public Cards DeckContents
        {
            get { return deckContents; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Parameterized constructor that sets the Cards object and supplies a suitable
        /// error message to the base Exception constructor
        /// </summary>
        /// <param name="sourceDeckContents"></param>
        public CardOutOfRangeException(Cards sourceDeckContents) : base("There are only 52 cards in the deck.")
        {
            deckContents = sourceDeckContents;
        }
        #endregion
    }
}