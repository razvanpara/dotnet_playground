using CardGames.Deck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Players
{
    public class Player
    {
        public string Name { get; private set; }
        public int RoundsWon { get; set; }
        public Player(string name)
        {
            Name = name;
        }
    }
}
