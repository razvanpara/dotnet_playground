using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Deck
{
    public sealed class CardsDeck
    {
        private LinkedList<PlayingCard> _deckCards;
        public int CardsInDeck { get => _deckCards.Count; }
        public CardsDeck() : this(false)
        {

        }
        public CardsDeck(bool shuffled)
        {
            _deckCards = GetDeck();
            if (shuffled) _deckCards = Shuffle(_deckCards);
        }
        private LinkedList<PlayingCard> GetDeck()
        {
            var cards = HelperClass.GetEnumValues<CardValue>();
            var suits = HelperClass.GetEnumValues<CardSuit>();
            return new LinkedList<PlayingCard>(cards.SelectMany(card => suits.Select(suit => new PlayingCard(card, suit))));
        }
        private LinkedList<PlayingCard> Shuffle(LinkedList<PlayingCard> deck)
        {
            var shuffledCards = deck.OrderBy(_ => Guid.NewGuid());
            return new LinkedList<PlayingCard>(shuffledCards);
        }
        public PlayingCard GetTop()
        {
            var card = _deckCards.First.Value;
            _deckCards.RemoveFirst();
            return card;
        }
        public PlayingCard GetBottom()
        {
            var card = _deckCards.Last.Value;
            _deckCards.RemoveLast();
            return card;
        }
        public void AddTop(PlayingCard card)
        {
            if (_deckCards.Contains(card)) throw new DuplicatePlayingCardException(card);
            _deckCards.AddFirst(card);
        }
        public void AddBottom(PlayingCard card)
        {
            if (_deckCards.Contains(card)) throw new DuplicatePlayingCardException(card);
            _deckCards.AddLast(card);
        }
        public void Shuffle()
        {
            _deckCards = Shuffle(_deckCards);
        }
        public override string ToString() => $"[{string.Join(", ", _deckCards)}]";
    }

}
