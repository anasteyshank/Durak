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

        public Player Computer { get; set; }
        public Player Human { get; set; }
        public Cards CardsInPlay { get; set; }
        public bool ComputerFoundCard = false;

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
            Human = new Player("Human");
            Computer = new Player("Computer");
            CardsInPlay = new Cards();
        }
        #endregion

        #region Public Methods
        public PlayingCard GetHighestCard(Cards cards)
        {
            PlayingCard highestCard = cards[0];

            for (int index = 1; index < cards.Count; index++)
            {
                if (cards[index] > highestCard)
                {
                    highestCard = cards[index];
                }
            }
            return highestCard;
        }

        public PlayingCard GetLowestCard(Cards cards)
        {
            PlayingCard lowestCard = cards[0];

            for (int index = 1; index < cards.Count; index++)
            {
                if (cards[index] < lowestCard)
                {
                    lowestCard = cards[index];
                }
            }
            return lowestCard;
        }

        private Cards GetLowestCards(Cards cards)
        {
            Cards returnCards = new Cards();
            PlayingCard lowestCard = cards[0];

            for (int index = 1; index < cards.Count; index++)
            {
                if (cards[index] < lowestCard)
                {
                    lowestCard = cards[index];
                }
            }

            returnCards.Add(lowestCard);

            for (int index = 1; index < cards.Count; index++)
            {
                if (cards[index].Rank == lowestCard.Rank && cards[index] != lowestCard && cards[index].Suit != PlayingCard.TrumpSuit)
                {
                    returnCards.Add(cards[index]);
                }
            }
            return returnCards;
        }

        private PlayingCard GetMostPopularCard(Cards cards)
        {
            int[] ranksInHand = new int[cards.Count];
            int returnIndex = 0;

            for (int rankIndex = 0; rankIndex < cards.Count; rankIndex++)
            {
                for (int index = 0; index < Computer.PlayHand.Count; index++)
                {
                    if (Computer.PlayHand[index].Rank == cards[rankIndex].Rank)
                    {
                        ranksInHand[rankIndex]++;
                    }
                }
            }

            for (int index = 1; index < cards.Count; index++)
            {
                if (ranksInHand[index] > ranksInHand[returnIndex])
                {
                    returnIndex = index;
                }
            }

            return cards[returnIndex];
        }

        public PlayingCard ComputerAttacks()
        {
            ComputerFoundCard = false;
            PlayingCard returnCard = new PlayingCard();

            if (CardsInPlay.Count == 0)
            {
                ComputerFoundCard = true;

                bool onlyTrumpSuit = true;
                for (int index = 0; index < Computer.PlayHand.Count && onlyTrumpSuit; index++)
                {
                    if (Computer.PlayHand[index].Suit != PlayingCard.TrumpSuit)
                    {
                        onlyTrumpSuit = false;
                    }
                }

                if (onlyTrumpSuit)
                {
                    returnCard = GetLowestCard(Computer.PlayHand);
                }
                else
                {
                    Cards lowestCards = GetLowestCards(Computer.PlayHand);
                    if (lowestCards.Count == 1)
                    {
                        returnCard = lowestCards[0];
                    }
                    else
                    {
                        returnCard = GetMostPopularCard(lowestCards);
                    }
                }
            }
            else
            {

            }

            if (ComputerFoundCard)
            {
                Computer.PlayHand.Remove(returnCard);
            }
            return returnCard;
        }
        #endregion
    }
}
