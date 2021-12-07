using CardGames.Deck;
using CardGames.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Games.Blackjack
{
    public class BlackJackGame : ICardGame
    {
        private IEnumerable<Player> _players = Enumerable.Empty<Player>();
        private IEnumerable<IGameRound> _gameRounds = Enumerable.Empty<IGameRound>();
        private BlackjackScoreCalculator _scoreCalculator = new BlackjackScoreCalculator();

        public BlackJackGame(params Player[] players)
        {
            players.ToList().ForEach(p => _players = _players.Append(p));
        }

        public void AddPlayer(Player player)
        {
            if (!_players.Any(p => p.Name == player.Name))
                _players = _players.Append(player);
        }

        public void RemovePlayer(Player player)
        {
            _players = _players.Where(p => p.Name != player.Name);
        }

        public bool Continue() => HelperClass.GetInputWithMessage("Play again?") is string input && !(input.ToLower().Contains("exit") || input.ToLower().Contains("no"));

        public void NewRound()
        {
            Console.Clear();
            CardsDeck deck = new CardsDeck(true);
            var round = new BlackJackRound(deck, _scoreCalculator, _players.Select(p => new PlayerHand(p)).Append(new PlayerHand(new Dealer())));
            _gameRounds = _gameRounds.Append(round);
            round.Play();
            round.PrintOutcome();
        }

        public void PrintPlayerScores()
        {
            Console.WriteLine("======== ENDGAME SCOREBOARD ========");
            Console.WriteLine(string.Join("\n", _players.Select(p => $"{p.Name} - {p.RoundsWon}")));
        }
    }
}
