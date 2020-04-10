/**
 * Game.cs - The Game class
 * 
 * Game class that holds information about a game being played
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
        /// <summary>
        /// Minimum number of players
        /// </summary>
        public static int MinimumNumberOfPlayers = 2;

        /// <summary>
        /// Maximum number of players
        /// </summary>
        public static int MaximumNumberOfPlayers = 7;

        /// <summary>
        /// Number of cards in a player's hand
        /// </summary>
        public static int NumberOfCards = 6;

        /// <summary>
        /// Indicates whether computer found a card to attack with or not
        /// </summary>
        public bool ComputerFoundCard = false;

        /// <summary>
        /// Indicates whether computer decided to take cards
        /// </summary>
        public bool ComputerPicksUp = false;

        /// <summary>
        /// Computer's Player object
        /// </summary>
        public Player Computer { get; set; }

        /// <summary>
        /// Human's Player object
        /// </summary>
        public Player Human { get; set; }

        /// <summary>
        /// Cards that are in play at the moment
        /// </summary>
        public Cards CardsInPlay { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for the Game class
        /// </summary>
        public Game()
        {
            Human = new Player("Human");        // instantiate a new Player object for a human player
            Computer = new Player("Computer");  // instantiate a new Player object for the computer
            CardsInPlay = new Cards();          // instantiate a new list of cards
            ComputerFoundCard = false;          // set ComputerFoundCard property to false
            ComputerPicksUp = false;            // set ComputerPicksUp property to false
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that gets a card with a trump suit and lowest rank
        /// </summary>
        /// <param name="cards">a list of cards</param>
        /// <returns>a card with a trump suit and lowest rank</returns>
        public PlayingCard GetLowestTrump(Cards cards)
        {
            PlayingCard lowestCard = cards[0];  // set the card with the lowest rank to the 1st card in the list

            // Loop through the list of cards
            for (int index = 1; index < cards.Count; index++)
            {
                // If card's suit is the trump suit and the lowest card's suit is not the trump suit OR
                //    card's suit is the trump suit and its rank is lower than the lowest card's rank:
                if (cards[index].Suit == PlayingCard.TrumpSuit && (lowestCard.Suit != PlayingCard.TrumpSuit || cards[index] < lowestCard))
                    lowestCard = cards[index];  // update the lowestCard value
            }

            // If the lowest card's suit is the trump suit, return the lowest card
            if (lowestCard.Suit == PlayingCard.TrumpSuit)
                return lowestCard;
            // Otherwise, find and return the card with the lowest rank
            else
                return GetLowestCard(cards);
        }

        /// <summary>
        /// Method that implements computer attack logic
        /// </summary>
        /// <returns></returns>
        public PlayingCard ComputerAttacks()
        {
            ComputerFoundCard = false;                  // set ComputerFoundCard property to false
            PlayingCard returnCard = new PlayingCard(); // card to return

            // If computer has cards:
            if (Computer.PlayHand.Count != 0)
            {
                // If there are no cards on the table:
                if (CardsInPlay.Count == 0)
                {
                    ComputerFoundCard = true;       // set ComputerFoundCard property to true
                    returnCard = GetCardToAttack(); // get a card to attack with
                }
                // If there are cards on the table
                else
                {
                    // Determine if computer has a card to attack with
                    bool canAttack = false;
                    for (int index = 0; index < Computer.PlayHand.Count && !canAttack; index++)
                        canAttack = CanAttack(Computer.PlayHand[index]);

                    // If computer has a card/cards to attack with, call GetCardToAttack() method
                    // to find the best card to attack with
                    if (canAttack)
                    {
                        ComputerFoundCard = true;
                        returnCard = GetCardToAttack();
                    }
                }
            }
            // If computer found a card to attack with, remove it from its hand
            if (ComputerFoundCard) Computer.PlayHand.Remove(returnCard);
            // Return a card
            return returnCard;
        }

        /// <summary>
        /// Determines whether a player can attack with a passed card
        /// </summary>
        /// <param name="card">a playing card</param>
        /// <returns>true if player can attack, false, otherwise</returns>
        public bool CanAttack(PlayingCard card)
        {
            bool returnValue = false;   // value to return

            // If there are no cards on the table, return true
            if (CardsInPlay.Count == 0) { returnValue = true; }
            // Otherwise:
            else
            {
                // Loop through all cards on the table
                for (int index = 0; index < CardsInPlay.Count && !returnValue; index++)
                {
                    // If a card has the same rank as one of the cards on the table, return true
                    if (card.Rank == CardsInPlay[index].Rank)
                        returnValue = true;
                }
            }
            // Return true if player can attack; false, otherwise
            return returnValue;
        }

        /// <summary>
        /// Determines whether a player can defend with a passed card
        /// </summary>
        /// <param name="computerAttacks">indicates whether it's computer's turn to attack</param>
        /// <param name="humanCard">player's card</param>
        /// <returns>true if player can defend with this card; false, otherwise</returns>
        public bool PlayerCanDefend(bool computerAttacks, PlayingCard humanCard)
        {
            // If it's computer's turn to attack AND player's card has a higher rank AND
            // player's card has the same suit as computer's card OR  player's card has a trump suit,
            // return true; otherwise, return false
            return computerAttacks && humanCard > CardsInPlay[CardsInPlay.Count - 1] &&
                   (humanCard.Suit == CardsInPlay[CardsInPlay.Count - 1].Suit || humanCard.Suit == PlayingCard.TrumpSuit);
        }

        /// <summary>
        /// Method that implements computer defense logic
        /// </summary>
        /// <returns>a card to defend with</returns>
        public PlayingCard ComputerDefends()
        {
            PlayingCard returnCard = new PlayingCard(); // a card to return
            // If computer doesn't already take cards:
            if (!ComputerPicksUp)
            {
                Cards defendCards = new Cards();    // cards that computer can defend with
                // Loop through computer's cards
                for (int index = 0; index < Computer.PlayHand.Count; index++)
                {
                    // If a card has the same suit as the player's card and its rank is higher, add card to the list
                    if (Computer.PlayHand[index].Suit == CardsInPlay[CardsInPlay.Count - 1].Suit && Computer.PlayHand[index] > CardsInPlay[CardsInPlay.Count - 1])
                        defendCards.Add(Computer.PlayHand[index]);
                }

                // If there are cards in the defendCards list, get a card with the lowest rank
                if (defendCards.Count > 0) { returnCard = GetLowestCard(defendCards); }
                // Otherwise:
                else
                {
                    // If player's card doesn't have a trump suit:
                    if (CardsInPlay[CardsInPlay.Count - 1].Suit != PlayingCard.TrumpSuit)
                    {
                        // Loop through computer's cards
                        for (int index = 0; index < Computer.PlayHand.Count; index++)
                        {
                            // If a card has a trump suit, add it to the list
                            if (Computer.PlayHand[index].Suit == PlayingCard.TrumpSuit)
                                defendCards.Add(Computer.PlayHand[index]);
                        }

                        // If there are cards in the list, get a card with the lowest rank from the list
                        if (defendCards.Count > 0)
                            returnCard = GetLowestCard(defendCards);
                        // Otherwise, computer takes cards
                        else
                            ComputerPicksUp = true;
                    }
                    // Otherwise, computer takes cards
                    else { ComputerPicksUp = true; }
                }
                // If computer doesn't take cards, remove a card to return from computer's hand
                if (!ComputerPicksUp) Computer.PlayHand.Remove(returnCard);
            }
            return returnCard;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets a card with the lowest rank
        /// </summary>
        /// <param name="cards">a list of cards</param>
        /// <returns>a card with the lowest rank</returns>
        private PlayingCard GetLowestCard(Cards cards)
        {
            PlayingCard lowestCard = cards[0];  // set the lowest card to the 1st card in the list

            // Loop through the list of cards
            for (int index = 1; index < cards.Count; index++)
            {
                // If a card is lower than the lowest card, update the lowest card
                if (cards[index] < lowestCard)
                    lowestCard = cards[index];
            }
            // Return a card with the lowest rank
            return lowestCard;
        }

        /// <summary>
        /// Gets a list of cards with the lowest rank
        /// </summary>
        /// <param name="cards">a list of cards</param>
        /// <returns>a list of cards with the lowest rank</returns>
        private Cards GetLowestCards(Cards cards)
        {
            Cards returnCards = new Cards();    // a list of cards to return

            PlayingCard lowestCard = GetLowestCard(cards); // get a card with the lowest rank

            // Loop through the list of cards
            for (int index = 0; index < cards.Count; index++)
            {
                // If a card has the same rank as the lowest card, and its suit is not a trump suit,
                // add it to the list of cards to return
                if (cards[index].Rank == lowestCard.Rank && cards[index].Suit != PlayingCard.TrumpSuit)
                    returnCards.Add(cards[index]);
            }
            // Return a list of cards with the lowest rank
            return returnCards;
        }

        /// <summary>
        /// Get a card with the most popular suit in computer's hand
        /// </summary>
        /// <param name="cards">a list of cards</param>
        /// <returns>a card with the most popular suit in computer's hand</returns>
        private PlayingCard GetMostPopularCard(Cards cards)
        {
            int[] suitsInHand = new int[cards.Count];   // an array that holds counts for suits in the hand
            int returnIndex = 0;                        // index of the suit to return
            // Loop through all cards in the list
            for (int suitIndex = 0; suitIndex < cards.Count; suitIndex++)
            {
                // Loop through all cards in computer's hand
                for (int index = 0; index < Computer.PlayHand.Count; index++)
                {
                    // If cards have the same suit, increase the counter of that suit
                    if (Computer.PlayHand[index].Suit == cards[suitIndex].Suit)
                        suitsInHand[suitIndex]++;
                }
            }
            // Loop through the list of cards
            for (int index = 1; index < cards.Count; index++)
            {
                // If a card's suit is more popular, update the returnIndex value
                if (suitsInHand[index] > suitsInHand[returnIndex])
                    returnIndex = index;
            }
            // Return a card with the most popular suit in computer's hand
            return cards[returnIndex];
        }

        /// <summary>
        /// Gets a card to attack with
        /// </summary>
        /// <returns>a card to attack with</returns>
        private PlayingCard GetCardToAttack()
        {
            Cards cardsList = new Cards();  // a list of cards to choose from 
            PlayingCard returnCard = new PlayingCard(); // a card to return
            bool onlyTrumpSuit = true;      // indicates whether there are only cards with the trump suit in the list

            // If there are no cards on the table, add all cards in computer's hand to the list
            if (CardsInPlay.Count == 0) { cardsList = Computer.PlayHand; }
            // Otherwise:
            else
            {
                // Loop through the list of cards in computer's hand
                for (int index = 0; index < Computer.PlayHand.Count; index++)
                {
                    // If computer can attack with this cards, add it to the list
                    if (CanAttack(Computer.PlayHand[index]))
                        cardsList.Add(Computer.PlayHand[index]);
                }
            }

            // Loop through the list of cards to determine whether there are only cards with the trump suit in the list
            for (int index = 0; index < cardsList.Count && onlyTrumpSuit; index++)
            {
                if (cardsList[index].Suit != PlayingCard.TrumpSuit)
                    onlyTrumpSuit = false;
            }

            // If there are only cards with the trump suit, get a card with the lowest rank
            if (onlyTrumpSuit) { returnCard = GetLowestCard(cardsList); }
            // Otherwise:
            else
            {
                // Get cards with the lowest rank
                Cards lowestCards = GetLowestCards(cardsList);
                // If there's only 1 card with the lowest rank, return this card
                if (lowestCards.Count == 1)
                    returnCard = lowestCards[0];
                // Otherwise, get a card with the most popular suit
                else
                    returnCard = GetMostPopularCard(lowestCards);
            }
            // Return a card to attack with
            return returnCard;
        }
        #endregion
    }
}
