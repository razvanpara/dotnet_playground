using CardGames.Deck;
using System.Linq;

namespace CardGames.Games.Blackjack
{
    public class BlackjackScoreCalculator : IScoreCalculator
    {
        public int GetScore(PlayerHand playerHand)
        {
            var score = playerHand.Cards.Select(card => GetCardPoints(card)).Sum();
            if (score > 21 && playerHand.Cards.Any(pc => pc.Value == CardValue.Ace))
                score -= 10; //making the ace score as 1 if score exceeds 21
            return score;
        }

        private int GetCardPoints(PlayingCard card)
        {
            var value = (int)card.Value + 2;
            return value > 11 ? 10 : value;
        }
    }
}
