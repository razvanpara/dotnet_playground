using CardGames.Deck;
using CardGames.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames.Games.Blackjack
{
    public class BlackJackRound : IGameRound
    {
        private CardsDeck _deck;
        private IScoreCalculator _scoreCalculator;
        private IEnumerable<PlayerHand> _players;

        public BlackJackRound(CardsDeck deck, IScoreCalculator scoreCalculator, IEnumerable<PlayerHand> players)
        {
            _deck = deck;
            _scoreCalculator = scoreCalculator;
            _players = players.ToList();
        }

        public void PrintOutcome()
        {
            Console.WriteLine("========== ROUND OUTCOME ==========");
            foreach (var player in _players)
                PrintPlayerHand(player);

        }

        public void Play()
        {
            Console.WriteLine("========== NEW ROUND ==========");
            DealInitialCards();
            PrintTable();
            foreach (var player in _players)
                PlayTurn(player);
            PrintTable();
            SetWinners();
        }

        private void SetWinners()
        {
            var dealer = _players.Where(p => p.Player is Dealer).First();
            var players = _players.Where(p => p.Player is not Dealer);
            var dealerScore = GetFinalScore(dealer);
            foreach (var player in players)
            {
                var playerName = player.Player.Name;
                var playerScore = GetFinalScore(player);

                Console.WriteLine($"{playerName} vs Dealer:");

                if (playerScore == dealerScore)
                    Console.WriteLine("It's a Tie!");
                else if (playerScore > dealerScore)
                {
                    Console.WriteLine($"{playerName} has won !");
                    player.Player.RoundsWon++;
                }
                else
                {
                    dealer.Player.RoundsWon++;
                    Console.WriteLine($"Dealer has won !");
                }
                Console.WriteLine();
            }
        }

        private int GetFinalScore(PlayerHand playerHand)
        {
            var score = _scoreCalculator.GetScore(playerHand);
            return score > 21 ? -1
                              : score;
        }

        private void DealInitialCards()
        {
            foreach (var player in _players)
                for (int i = 0; i < 2; i++)
                    DealPlayerCard(player);
        }

        private void PrintPlayerHand(PlayerHand playerHand)
        {
            var hand = playerHand.Cards.Select(card => card.ToString());
            var score = $"=> {_scoreCalculator.GetScore(playerHand)}";
            if (playerHand.Player is Dealer d && !d.ShowCards)
            {
                hand = hand.Take(1).Append("Hidden");
                score = "";
            }
            Console.WriteLine($"{playerHand.Player.Name}'s cards: [ {string.Join(", ", hand)} ] {score}");
        }
        private void PrintTable()
        {
            Console.WriteLine("============ TABLE ============");
            foreach (var playerHand in _players)
                PrintPlayerHand(playerHand);
            Console.WriteLine();
        }

        private PlayingCard DrawCard()
        {
            return _deck.GetTop();
        }
        private PlayingCard DealPlayerCard(PlayerHand player)
        {
            var card = DrawCard();
            player.AddCard(card);
            return card;
        }

        private int GetPlayerChoice()
        {
            Console.WriteLine($"Options: \n - 1 hit\n - 0 stay");
            var input = Console.ReadKey();
            Console.WriteLine($"\bHas chosen {input.KeyChar}");
            int.TryParse(input.KeyChar.ToString(), out int choice);
            return choice;
        }

        private bool PlayerIsBust(PlayerHand playerHand) => _scoreCalculator.GetScore(playerHand) > 21;

        private void PlayTurn(PlayerHand playerHand)
        {
            Console.WriteLine($"It's {playerHand.Player.Name}'s turn:");
            if (playerHand.Player is Dealer)
                DealerTurn(playerHand);
            else
                PlayerTurn(playerHand);
            Console.WriteLine();
        }
        private void DealerTurn(PlayerHand playerHand)
        {
            (playerHand.Player as Dealer).ShowCards = true;
            PrintPlayerHand(playerHand);
            Console.WriteLine();
            while (_scoreCalculator.GetScore(playerHand) <= 16 && !PlayerIsBust(playerHand))
            {
                System.Threading.Thread.Sleep(1000);
                var card = DealPlayerCard(playerHand);
                Console.WriteLine($"Dealer was dealt a '{card}'");
                PrintPlayerHand(playerHand);
                Console.WriteLine();
            }
        }
        private void PlayerTurn(PlayerHand playerHand)
        {
            PrintPlayerHand(playerHand);
            Console.WriteLine();
            while (!PlayerIsBust(playerHand) && _scoreCalculator.GetScore(playerHand) < 21)
            {
                if (GetPlayerChoice() != 1)
                    break;
                var card = DealPlayerCard(playerHand);
                Console.WriteLine($"{playerHand.Player.Name} was dealt a '{card}'");
                PrintPlayerHand(playerHand);
                Console.WriteLine();
            }
        }
    }
}
