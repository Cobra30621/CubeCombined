namespace GameState
{
    public interface GameContext
    {
        void SetGameState(GameState newState);
        
        GameState CurrentState { get; } 
    }
}