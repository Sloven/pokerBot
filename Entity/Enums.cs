using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Entity
{
    public enum Rank
    {
        NOT_RECOGNIZED = 0,
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public enum Suit
    {
        NOT_RECOGNIZED = 0,
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }
    public enum CardOwner
    {
        NOT_RECOGNIZED = 0,
        Player,
        Table,
        Opponent
    }


    
}
