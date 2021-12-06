using System;

namespace CardGames.Deck
{
    public class DuplicatePlayingCardException : Exception
    {
        public DuplicatePlayingCardException(PlayingCard card) : base($"{card.Value} of {card.Suit} already exists")
        {
        }
    }

}
