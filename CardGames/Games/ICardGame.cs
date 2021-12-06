using CardGames.Players;

namespace CardGames.Games
{
    public interface ICardGame
    {
        void AddPlayer(Player player);
        void RemovePlayer(Player player);
        void NewRound();
        bool Continue();
        void PrintPlayerScores();
    }
}
