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
    }
}
