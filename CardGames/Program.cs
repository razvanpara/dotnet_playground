using CardGames.Deck;
using CardGames.Games;
using CardGames.Games.Blackjack;
using CardGames.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("Razvan");
            var player2 = new Player("John");
            var blackjack = new BlackJackGame(player1, player2);
            do
            {
                blackjack.NewRound();
            }
            while (blackjack.Continue());
            blackjack.PrintPlayerScores();
        }
    }
}
