/**
 * Game.cs - The Game class
 * 
 * Game class that holds information about a game being played.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using System;
using CardLibrary;

namespace DurakLibrary
{
    public class Game
    {
        #region Fields and Properties
        private int currentCard;        // Points to the next card in the deck   
        private DurakDeck playDeck;     // A deck of cards used for the game
        private Player[] players;       // An array of Player objects
        private Cards discardedCards;   // A collection of cards that have been discarded by players

        public static int MinimumNumberOfPlayers = 2;   // Minimum number of players
        public static int MaximumNumberOfPlayers = 7;   // Maximum number of players
        public static int NumberOfCards = 6;            // Number of cards in the game
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the Game class
        /// </summary>
        public Game()
        {
            currentCard = 0;                     // Points to the 1st card in the deck   
            playDeck = new DurakDeck();          // Set a default deck
            playDeck.Shuffle();                  // Shuffle the deck
            discardedCards = new Cards();        // Empty cards collection
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set an array of players 
        /// </summary>
        /// <param name="newPlayers"></param>
        public void SetPlayers(Player[] newPlayers)
        {
            // If more than a defined maximum number of players want to the game, throw an exception
            if (newPlayers.Length > MaximumNumberOfPlayers)
                throw new ArgumentException("A maximum of " + MaximumNumberOfPlayers + " players may play this game.");

            // If less than a defined minimum number of players want to the game, throw an exception
            if (newPlayers.Length < MinimumNumberOfPlayers)
                throw new ArgumentException("A minimum of " + MinimumNumberOfPlayers + " players may play this game.");

            players = newPlayers;   // Set an array of players
        }

        /// <summary>
        /// A method to deal cards
        /// </summary>
        private void DealHands()
        {
            // Loop through the players of the game:
            for (int player = 0; player < players.Length; player++)
            {
                // Deal cards from the remaining deck
                for (int card = players[player].PlayHand.Count; card < NumberOfCards; card++)
                    players[player].PlayHand.Add(playDeck.GetCard(currentCard++));
            }
        }

        /// <summary>
        /// The main logic of the card game
        /// </summary>
        /// <returns></returns>
        public void PlayGame()
        {

        }
        #endregion
    }
}
