namespace CardGames.Games
{
    public interface IScoreCalculator
    {
        int GetScore(PlayerHand playerHand);
    }
}
