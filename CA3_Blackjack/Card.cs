using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_Blackjack
{
    public class Card
    {
        //properties
        public string Suit { get; set; }
        public string Face { get; set; }
        public int CardValue { get; set; }


        //constructors for the class
        //default constructor
        public Card()
        {

        }

        //Full constructor
        public Card(string suit, string face, int cardValue)
        {
            Suit = suit;
            Face = face;
            CardValue = cardValue;
        }

        //Partial Constructor
        public Card(string suit, string face)
        {
            Suit = suit;
            Face = face;
            CardValue = 0;
        }

        //methods for the Card class

    }
}
