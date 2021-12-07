namespace CardGames.Deck
{
    public class PlayingCard
    {
        public CardValue Value { get; private set; }
        public CardSuit Suit { get; private set; }
        public PlayingCard(CardValue cardValue, CardSuit cardSuit)
        {
            Value = cardValue;
            Suit = cardSuit;
        }

        public override bool Equals(object obj)
        {
            if (obj is PlayingCard card)
                return card.Value == this.Value && card.Suit == this.Suit;
            return false;
        }
        public static bool operator ==(PlayingCard a, PlayingCard b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(PlayingCard a, PlayingCard b)
        {
            return !(a == b);
        }
        public override string ToString() => $"{Value.AsString()}{Suit.AsString()}";
    }

}
