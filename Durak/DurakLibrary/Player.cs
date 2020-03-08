/**
 * Player.cs - The Player class
 * 
 * Player class that holds information about a single player.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using CardLibrary;

namespace DurakLibrary
{
    public class Player
    {
        #region Fields and Properties
        /// <summary>
        /// Player's name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Player's cards
        /// </summary>
        public Cards PlayHand { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Parameterized constructor that sets the player's name and their cards
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            Name = name;
            PlayHand = new Cards();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Indicates whether the player has won the game
        /// </summary>
        /// <returns>true if player has no cards left</returns>
        public bool HasWon()
        {
            bool won = false;   // a return value
            // Player wins if they have no cards left
            if (PlayHand.Count == 0) won = true;
            return won;
        }
        #endregion
    }
}
