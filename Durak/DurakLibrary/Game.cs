/**
 * Game.cs - The Game class
 * 
 * Game class that holds information about a game being played.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-04-07
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using CardLibrary;

namespace DurakLibrary
{
    public class Game
    {
        #region Fields and Properties 
        public static int MinimumNumberOfPlayers = 2;   // Minimum number of players
        public static int MaximumNumberOfPlayers = 7;   // Maximum number of players
        public static int NumberOfCards = 6;            // Number of cards in the game

        public bool ComputerFoundCard = false;
        public bool ComputerPicksUp = false;

        public Player Computer { get; set; }
        public Player Human { get; set; }
        public Cards CardsInPlay { get; set; }
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
            ComputerFoundCard = false;
            ComputerPicksUp = false;
        }
        #endregion

        #region Public Methods
        public PlayingCard GetLowestTrump(Cards cards)
        {
            PlayingCard lowestCard = cards[0];

            for (int index = 1; index < cards.Count; index++)
            {
                if (cards[index] < lowestCard && cards[index].Suit == PlayingCard.TrumpSuit)
                {
                    lowestCard = cards[index];
                }
            }

            if (lowestCard.Suit == PlayingCard.TrumpSuit)
            {
                return lowestCard;
            }
            else
            {
                return GetLowestCard(cards);
            }
        }

        public PlayingCard ComputerAttacks()
        {
            ComputerFoundCard = false;
            PlayingCard returnCard = new PlayingCard();

            if (Computer.PlayHand.Count != 0)
            {
                if (CardsInPlay.Count == 0)
                {
                    ComputerFoundCard = true;
                    returnCard = GetCardToAttack();
                }
                else
                {
                    bool canAttack = false;
                    for (int index = 0; index < Computer.PlayHand.Count && !canAttack; index++)
                    {
                        canAttack = CanAttack(Computer.PlayHand[index]);
                    }

                    if (canAttack)
                    {
                        ComputerFoundCard = true;
                        returnCard = GetCardToAttack();
                    }
                }
            }

            if (ComputerFoundCard)
            {
                Computer.PlayHand.Remove(returnCard);
            }
            return returnCard;
        }

        public bool CanAttack(PlayingCard card)
        {
            bool returnValue = false;

            if (CardsInPlay.Count == 0)
            {
                returnValue = true;
            }
            else
            {
                for (int index = 0; index < CardsInPlay.Count && !returnValue; index++)
                {
                    if (card.Rank == CardsInPlay[index].Rank)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        public PlayingCard ComputerDefends()
        {
            PlayingCard returnCard = new PlayingCard();
            Cards defendCards = new Cards();
            for (int index = 0; index < Computer.PlayHand.Count; index++)
            {
                if (Computer.PlayHand[index].Suit == CardsInPlay[CardsInPlay.Count - 1].Suit && Computer.PlayHand[index] > CardsInPlay[CardsInPlay.Count - 1])
                {
                    defendCards.Add(Computer.PlayHand[index]);
                }
            }

            if (defendCards.Count > 0)
            {
                returnCard = GetLowestCard(defendCards);
            }
            else
            {
                if (CardsInPlay[CardsInPlay.Count - 1].Suit != PlayingCard.TrumpSuit)
                {
                    for (int index = 0; index < Computer.PlayHand.Count; index++)
                    {
                        if (Computer.PlayHand[index].Suit == PlayingCard.TrumpSuit)
                        {
                            defendCards.Add(Computer.PlayHand[index]);
                        }
                    }

                    if (defendCards.Count > 0)
                    {
                        returnCard = GetLowestCard(defendCards);
                    }
                    else
                    {
                        ComputerPicksUp = true;
                    }
                }
                else
                {
                    ComputerPicksUp = true;
                }
            }

            if (!ComputerPicksUp)
            {
                Computer.PlayHand.Remove(returnCard);
            }

            return returnCard;
        }
        #endregion

        #region Helper Methods
        private PlayingCard GetLowestCard(Cards cards)
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

        private PlayingCard GetCardToAttack()
        {
            Cards cardsList = new Cards();

            if (CardsInPlay.Count == 0)
            {
                cardsList = Computer.PlayHand;
            }
            else
            {
                for (int index = 0; index < Computer.PlayHand.Count; index++)
                {
                    if (CanAttack(Computer.PlayHand[index]))
                    {
                        cardsList.Add(Computer.PlayHand[index]);
                    }
                }
            }

            PlayingCard returnCard = new PlayingCard();
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
                returnCard = GetLowestCard(cardsList);
            }
            else
            {
                Cards lowestCards = GetLowestCards(cardsList);
                if (lowestCards.Count == 1)
                {
                    returnCard = lowestCards[0];
                }
                else
                {
                    returnCard = GetMostPopularCard(lowestCards);
                }
            }
            return returnCard;
        }
        #endregion
    }
}
