using CardGames.Deck;
using CardGames.Players;
using System.Collections.Generic;
using System.Linq;

namespace CardGames.Games
{
    public class PlayerHand
    {
        public Player Player { get; private set; }
        public IEnumerable<PlayingCard> Cards { get; private set; }
        public PlayerHand(Player player)
        {
            Player = player;
            Cards = Enumerable.Empty<PlayingCard>();
        }
        public virtual void AddCard(PlayingCard card)
        {
            var newCards = Cards.Append(card).ToList();
            Cards = newCards;
        }
        public virtual void RemoveCard(PlayingCard card) => Cards = Cards.Where(c => c != card);
    }
}
