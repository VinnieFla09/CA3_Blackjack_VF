using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    public class Deck
    {
        //properties
        public Card[] playingDeck { get; set; }
        public int[] CardValue { get; set; }
        public string[] Suit { get; set; }
        public string[] Face { get; set; }

        //constructors
        //Default constructor
        public Deck()
        {
            //Array of the values of each card
            int[] CardValue = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
            //Array for the suits in the deck 
            string[] Suit = { "Hearts", "Diamonds", "Clubs", "Spades" };
            //Array for the card faces in the deck
            string[] Face = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            //Create a new array of cards called playingDeck
            playingDeck = new Card[52];
            //Set the deck index to 0
            int deckIndex = 0;

            // Setting outer control loop to populate based on Suit index in "string[] suit"
            for (int i = 0; i < 4; i++)
            {
                // Setting inner control loop to populate based on index in "int[] CardValue"
                for (int j = 0; j < 13; j++)
                {
                    //Create a new card object for each card 
                    Card c1 = new Card(Suit[i], Face[j], CardValue[j]);

                    //Add the cards to the deck with each loop
                    playingDeck[deckIndex] = c1;
                    //Increment the deck index
                    deckIndex++;

                }// end for

            }//end for
        }//end of constructor

        //alternative constructor
        public Deck(int[] cardValue, string[] suit, string[] face)
        {
            //Setup properties
            CardValue = cardValue;
            Suit = suit;
            Face = face;

            //Create a new array of cards called playingDeck
            playingDeck = new Card[52];
            //Set the deck index to 0
            int deckIndex = 0;

            // Setting outer control loop to populate based on Suit index in "string[] suit"
            for (int i = 0; i < 4; i++)
            {
                // Setting inner control loop to populate based on index in "int[] CardValue"
                for (int j = 0; j < 13; j++)
                {
                    //Create a new card object for each card 
                    Card c1 = new Card(Suit[i], Face[j], CardValue[j]);

                    //Add the cards to the deck with each loop
                    playingDeck[deckIndex] = c1;
                    //Increment the deck index
                    deckIndex++;

                }// end for

            }//end for 

        }

        //3rd constructor
        //alternative constructor
        public Deck(int[] cardValue, string[] suit)
        {
            //Setup properties
            CardValue = cardValue;
            Suit = suit;
            
            //Array for the card faces in the deck
            string[] Face = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

            //Create a new array of cards called playingDeck
            playingDeck = new Card[52];
            //Set the deck index to 0
            int deckIndex = 0;

            // Setting outer control loop to populate based on Suit index in "string[] suit"
            for (int i = 0; i < 4; i++)
            {
                // Setting inner control loop to populate based on index in "int[] CardValue"
                for (int j = 0; j < 13; j++)
                {
                    //Create a new card object for each card 
                    Card c1 = new Card(Suit[i], Face[j], CardValue[j]);

                    //Add the cards to the deck with each loop
                    playingDeck[deckIndex] = c1;
                    //Increment the deck index
                    deckIndex++;

                }// end for

            }//end for 

        }

        #region METHODS                
        //Methods for Deck class
        //Method to shuffle the deck
        public void Shuffle()
        {
            //Create a new random
            Random rng = new Random();
            //Initialise the shuffle index
            int shuffleIndex = 0;
            //create a card object for the temp card to be used while shuffling
            Card tmpCard = new Card();
            //Create a card object for the end card of the deck
            Card endCard = new Card();

            for (int i = 51; i >= 0; i--)
            {
                // generate a number between 0 and i
                int swapCard = rng.Next(0, i);
                // preserve the end card, it will be replaced by the random card selected
                endCard = playingDeck[i];
                // used as temp for swapping shffleIndex to random card location
                tmpCard = playingDeck[swapCard];
                // perform the card swapping and increment to next card location to operate on
                playingDeck[swapCard] = playingDeck[shuffleIndex];
                playingDeck[i] = tmpCard;
                // Add what was previously the end card to be available for random selection/shuffle
                playingDeck[shuffleIndex] = endCard; 
                //Increment the shuffle index
                shuffleIndex++;
            }
        }// end shuffle method

        //Method to print out the contents of the Deck Class when called
        public override string ToString()
        {
            string output = string.Format("{0,-22}{1,10}\n\n", "Card", "Value");
            Card currentCard = new Card();

            for (int i = 0; i <= 51; i++)
            {
                currentCard = playingDeck[i];
                output += string.Format("Card {0}: {1} of {2}. Value: {3}\n", i, currentCard.Face, currentCard.Suit, currentCard.CardValue);
                
            }
            return output;
        }// end method

        //Method to take next card from the deck and return its integer value
        public int GetCard(int cardIndex, out string cardFace, out string suit )
        {
            //Create a new card object
            Card newCard = new Card();
            //Draw the next card from the deck and use the card object to store it
            newCard = playingDeck[cardIndex];
            //Store the suit of the new card
            suit = newCard.Suit;
            //Store the face of the new card
            cardFace = newCard.Face;
            //Change color of output text based on card face
            if (suit == "Hearts" || suit == "Diamonds")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else
                Console.ForegroundColor = ConsoleColor.Black;
            //Print the card details to screen
            Console.WriteLine("Card is {0} of {1}.",cardFace,suit);
            //Return the card value
            return newCard.CardValue;
        }
        #endregion METHODS
    }
}
