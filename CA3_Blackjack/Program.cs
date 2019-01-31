//OOSD1 CA3 Blackjack program written by Vincent Flaherty S00190498
//    Program contains a blackjack game where the user plays against a dealer
//    The deck is shuffled before each game
//    Player is dealt cards until they decide to stick or have bust
//    I have added a gambling feature to the application where the user places a bet on each hand.
//    The results of the bet are then displayed at the end of the game

 /* Steps
 *    1. Ask user how much they want to wager on the game
 *    2. Deal 2 cards to the user
 *    3. Continue to deal cards until user decides to stick, has hit blackjack or has bust
 *    4. Deal cards to the dealer until they score 17 or more
 *    5. Continue to deal until the dealer has won, hit blackjack or bust
 *    6. Display the result of the game
 *    7. Display the winnings to the user if they have won the bet (odds paid on a win are 3.2 or 2/1 if win with blackjack)
 *    8. Repeat steps 1 to 8 until the user ends the game
 *    9. Close the program
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            #region SETUP
            //Create Variables for cardIndex, playerTotal dealerTotal and cardValue
            int cardIndex =0, playerTotal=0, dealerTotal=0, cardValue;
            //Create a double for the player wager
            double wager;
            //Create strings for the card face and suit. For the user input and for the playAgain item. This is set to y by default.
            //This is so the game loop will run the 1st time before the user has been asked
            string face, suit, userInput,playAgain="y";
            //Add bools for the checking of user inputs and also the results of the game
            bool validEntry,playerWins,draw;
                       
            //Add encoding for euro symbol
            Console.OutputEncoding = Encoding.UTF8;

            //Display instructions to the user
            DisplayInstructions();

            //Create a new deck of cards
            Deck newDeck = new Deck();
            
            //Print out the deck to see if it has been created correctly
            //Console.WriteLine(newDeck);
            //Pause the program
            //Console.ReadLine();
            //Clear the console
            //Console.Clear();
            #endregion SETUP

            #region PLAYGAME
            while (playAgain == "y") //Loop the game while user wants to play
            {
                //Reset the dealer and player totals each time the loop iterates for a new game
                //Also reset the card index and the results indicators so the previous games results dont influence the outcome of this game
                playerTotal = 0;
                dealerTotal = 0;
                cardIndex = 0;
                playerWins = false;
                draw = false;
                
                //print the current deck to screen
                //Console.WriteLine(newDeck);
                //Pause the program
                //Console.ReadLine();

                //Shuffle the deck for the new game
                newDeck.Shuffle();
                //Print out the deck to see if the shuffle was successful
                //Console.WriteLine(newDeck);

                //Ask the user how much they want to wager and save the value
                wager = GetWager();
                //CLear the console
                Console.Clear();

                //Pause the program
                //Console.ReadLine();

                #region PlayersTurn
                //Show user it is the players turn
                Console.WriteLine("\t\t*********PLAYERS TURN*********\t\t");
                //Start a new game by drawing 2 cards for the player
                for (int i = 0; i < 2; i++)
                {
                    cardValue = newDeck.GetCard(i, out face, out suit); //Get the next card and set cardvalue 
                    //If the user draws an ace ask if they want to use it as 1 or 11
                    if (cardValue == 1)
                    {
                        //Reset font color
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("You have drawn an Ace.\nPlease enter if you wish the ace to be worth 11 or 1....");
                        validEntry = Int32.TryParse(Console.ReadLine(), out cardValue);

                        //Check to see an int value or either 1 or 11 was entered by the user
                        while ((validEntry != true) || ((cardValue != 1) && (cardValue != 11)))
                        {
                            //Make the user re-enter the value until they enter it correctly
                            Console.WriteLine("Invalid response please enter 11 or 1");
                            validEntry = Int32.TryParse(Console.ReadLine(), out cardValue);
                        }

                    }
                    //Add new card value to the total and display which card was dealt
                    playerTotal += cardValue;
                    //Reset font color
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    //Display new total for player
                    Console.WriteLine("Player total is {0}", playerTotal);
                    //Pause the program
                    Console.ReadLine();
                    //Index the number of cards taken from the deck
                    cardIndex++;
                }

                //Ask the user if they want to stick or twist
                userInput = StickOrTwist();
                
                //While loop Play until player goes bust hits 21 or sticks
                while ((playerTotal < 21) && (userInput == "t"))
                {
                    //Get the next card and set cardvalue 
                    cardValue = newDeck.GetCard(cardIndex, out face, out suit); 
                    if (cardValue == 1)
                    {
                        //Reset font color
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("You have drawn an Ace.\nPlease enter if you wish the ace to be worth 11 or 1....");
                        validEntry = Int32.TryParse(Console.ReadLine(), out cardValue);

                        //Check to see an int value of 1 or 11was entered by the user
                        while ((validEntry != true) || ((cardValue != 1) && (cardValue != 11)))
                        {
                            //Make the user re-enter the value until they enter it correctly
                            Console.WriteLine("Invalid response please enter 11 or 1");
                            validEntry = Int32.TryParse(Console.ReadLine(), out cardValue);
                        }
                    }
                    //Add new card value to the total and display which card was dealt
                    playerTotal += cardValue;
                    //Reset font color
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    //Print out the player total
                    Console.WriteLine("Player total is {0}", playerTotal);
                    //Pause the program
                    Console.ReadLine();
                    //Increment the card index
                    cardIndex++;

                    //If player has scored less than 21 ask if they want to stick or twist
                    if (playerTotal < 21) 
                    {
                        //Use method to ask user to stick or twist
                        userInput = StickOrTwist();                       
                    }//end of if
                    //Otherwise set user selection to stick         
                    else userInput = "s";                      
                } //End of while loop

                //If player score is less than 21 and player wants to stick
                if ((userInput == "s") && (playerTotal < 21)) 
                {
                    Console.WriteLine("Player has decided to stick on {0}", playerTotal);
                    Console.ReadLine();
                }//End of If player score is less than 21 and player wants to stick

                //Else if player has 21 this is blackjack
                else if (playerTotal == 21) 
                {
                    Console.WriteLine("Player has hit BLACKJACK:{0}", playerTotal);
                    Console.ReadLine();
                }//End of else if player has 21

                //else if player has above 21 (bust)
                else if (playerTotal > 21)  
                {
                    Console.WriteLine("Player has bust!!", playerTotal);
                    Console.ReadLine();
                } //End of else if player has above 21 (bust)

                //All other cases
                else
                {
                    Console.WriteLine("Unrecognised value error!!!!!");
                    Console.ReadLine();
                }//End of else

                //Clear the console
                Console.Clear();
                #endregion PlayersTurn

                #region Dealers Turn
                //Play for the dealer
                Console.WriteLine("\t\t*********DEALERS TURN*********\t\t");
                //Dealer must draw minimum 2 cards by default and must play until they score more than 17
                while (dealerTotal < 17)
                {
                    //Get the next card and set cardvalue 
                    cardValue = newDeck.GetCard(cardIndex, out face, out suit); 
                    //If the dealer gets an Ace. Use as 11 as long as 11 will not bust the dealer
                    if ((cardValue == 1) && ((dealerTotal + 11) <= 21))
                    {
                        cardValue = 11;
                    }
                    //Add new card value to the total and display which card was dealt
                    dealerTotal += cardValue;
                    //Reset font color
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    //Display new total for dealer
                    Console.WriteLine("Dealer total is {0}", dealerTotal);
                    //Pause the script
                    Console.ReadLine();
                    //Index the number of cards taken from the pack
                    cardIndex++; 
                }//End of while loop

                //If the player has scored between 17 and 21 the dealer must try to draw/win
                while ((playerTotal >= 17 && playerTotal <= 21) && (dealerTotal <= playerTotal) &&(dealerTotal!=21))
                {
                    //Get the next card and set cardvalue 
                    cardValue = newDeck.GetCard(cardIndex, out face, out suit);
                    //Add new card value to the total and display which card was dealt
                    dealerTotal += cardValue;
                    //Display new total for dealer
                    Console.WriteLine("Dealer total is {0}", dealerTotal);
                    //Pause the script
                    Console.ReadLine();
                    //Index the number of cards taken from the pack
                    cardIndex++; 
                } //End of while

                //Clear the console
                Console.Clear();
                #endregion Dealers Turn 

                #region Decide Results
                //Reset font color
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                //If player has <= 21 but more than dealer they win
                if ((dealerTotal < playerTotal) && (playerTotal <= 21)) 
                    {
                    Console.WriteLine("Player wins");
                    Console.ReadLine();
                    //Set result bool indicator
                    playerWins = true;
                    }//end of if

                //Else if dealer has <= 21 but more than player they win
                else if ((playerTotal < dealerTotal) && (dealerTotal <= 21))
                    {
                    Console.WriteLine("Dealer wins");
                    Console.ReadLine();
                    //Set result bool indicator
                    playerWins = false;
                    }//End if

                //Else if both players >21
                else if ((playerTotal > 21) && (dealerTotal > 21))
                    {
                    Console.WriteLine("Both players bust!!\nDRAW GAME");
                    Console.ReadLine();
                    //Set result bool indicator
                    draw = true;
                    }//End if

                //Else if player busts and dealer<21, dealer wins
                else if ((playerTotal > 21) && (dealerTotal <= 21)) 
                    {
                    Console.WriteLine("Dealer wins");
                    Console.ReadLine();
                    //Set result bool indicator
                    playerWins = false;
                    } //End if

                //else if dealer and player hit 21 draw
                else if ((dealerTotal == 21) && (playerTotal == 21))  
                    {
                    Console.WriteLine("Both players have Blackjack!! \nDRAW GAME");
                    Console.ReadLine();
                    //Set result bool indicator
                    draw = true;
                    }

                else //Remaining cases player wins
                    {
                    Console.WriteLine("Player wins");
                    Console.ReadLine();
                    //Set result bool indicator
                    playerWins = true;
                    }
                #endregion Decide Results

                #region DisplayResults
                //Reset Output text colour
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                //If the player has won calculate player winnings and display
                if (playerWins == true)
                    {
                    GetWinnings(wager, playerTotal);
                    }

                //If the game is a draw refund the user
                else if (draw == true)
                    {

                    Console.WriteLine("Result is a draw refund {0:C} to the user", wager);
                    Console.ReadLine();
                    }

                //else the dealer wins display to screen
                else
                    {
                    Console.WriteLine("Dealer has won. Player loses {0:C}",wager);
                    }
                #endregion DisplayResults

                //Ask the user if they want to play another game
                Console.WriteLine("\nWOULD YOU LIKE TO PLAY AGAIN?\ny for yes n for no......");
                //Convert all response to lowercase so case is ignored
                playAgain =  Console.ReadLine().ToLower(); 
                //If the user enters an incorrect value ask them again until correct value entered
                while ((playAgain != "y") && (playAgain != "n"))
                    {
                        Console.WriteLine("Incorrect Value entered!!!\nWOULD YOU LIKE TO PLAY AGAIN?\n y for yes n for no......");
                        playAgain = Console.ReadLine();
                    }
                //CLear the console after each game
                Console.Clear();
            } //End of play again loop
            #endregion PLAYGAME

            //Display if the user has decided not to play again
            Console.WriteLine("Player has chosen to end the game, press enter to exit.....");
            Console.ReadLine(); //Pause for input from user
            

        } //End of main

        #region Methods
        //Methods
        //Display Instructions to the user
        private static void DisplayInstructions()
        {
            Console.WindowHeight = 25;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("\t\t\t*****Welcome to my BlackJack application******\n\n");
            Console.WriteLine("Before the game starts you will be asked how much you want to bet on the hand\n" +
                              "When a new game begins you will be dealt 2 cards by default.\n" +
                              "You will then be asked if you would like to stick or twist\n" +
                              "Once you decide to stick the dealer will then be dealt cards until \n" +
                              "he scores above 17 or goes bust.\n" +
                              "The application will then display the results of the game and ask \n" +
                              "the user if they wish to play again or exit!!\n\n" +
                              "Press enter when you are ready to play a game.....");

            //Pause the program until the user presses enter
            Console.ReadLine();
            //Clear the console before beginning the 1st game
            Console.Clear();
        } //End of method to display instructions to the user

        //Method to ask player if they want to stick or twist and store the result
        private static string StickOrTwist()
        {
            //Create string variable for user response
            string userInput;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Would you like to stick or twist?\nEnter s to stick or t to twist....\n");
            //Convert all response to lowercase so case is ignored
            userInput = Console.ReadLine().ToLower();

            //Check the user has entered the correct response if not ask again
            while ((userInput != "s") && (userInput != "t"))
                {
                    Console.WriteLine("Incorrect Value entered!!!\nWould you like to stick or twist?\nEnter s to stick or t to twist....\n");
                    userInput = Console.ReadLine();
                } //End of while                   
            //return the user response to the main method
            return userInput;
        } //End of stick or twist method

        //Method to get wager from the user
        private static double GetWager()
        {
            //create variables for wager and a bool to check the response
            double wager;
            bool validAmount;
            //Ask the user how much to bet
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("How much would you like to bet on the game?");
            //Check if the entry from user is valid
            validAmount = double.TryParse(Console.ReadLine(), out wager);
            //If amount is invalid loop until valid amount is entered
            while (validAmount != true)
            {
                Console.WriteLine("Incorrect value enterred.\nHow much would you like to bet on the game?");
                validAmount = double.TryParse(Console.ReadLine(), out wager);
            }
            //return the wager from the method
            return wager;
        } //End of GetWager() Method

        //Method to calculate winnings for the user
        private static void GetWinnings(double wager,int playerScore)
        {
            //Set the payout odds and create a double for the winnings            
            double BLKJACK = 2;
            double WIN = 1.5;
            double winnings;
            //If the user has scored blackjack and won calculate winnings
            if (playerScore == 21)
            {
                winnings = wager * BLKJACK ;
            }
            //Otherwise if the player wins without blackjack calculate winnings
            else
            {
                winnings = wager * WIN;
            }
            //Print winnings on screen
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("User has won {0:C}",winnings);
            Console.ReadLine();
        }
        #endregion Methods
    }
}
