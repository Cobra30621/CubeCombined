using Cube;

namespace GameState
{
    public interface GameContext
    {
        void SetGameState(GameState newState);

        void GameOver();
        
        StateType CurrentStateType { get; }
        ICubeManager CubeManager { get; set; }
        ICubeUI CubeUI { get; }
        
        GameOverUI GameOverUI { get; set; }

    }
}