using CardGames.Deck;
using CardGames.Games;
using CardGames.Games.Blackjack;
using CardGames.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerNames = new List<string>();
            Console.WriteLine("Register players:");
            do
            {
                var playerName = HelperClass.GetInputWithMessage("Enter player name:");
                while (playerNames.Contains(playerName))
                {
                    playerName = HelperClass.GetInputWithMessage("Player already exists, pick a new name:");
                }
                playerNames.Add(playerName);
            }
            while (HelperClass.GetInputWithMessage("Register more?") is string input && !(input.ToLower() == "done" || input.ToLower() == "no"));
            var blackjack = new BlackJackGame(playerNames.Select(name => new Player(name)).ToArray());
            do
            {
                blackjack.NewRound();
            }
            while (blackjack.Continue());
            blackjack.PrintPlayerScores();
        }
    }
}
