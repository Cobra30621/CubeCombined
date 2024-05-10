using Cube;

namespace GameState
{
    public interface GameContext
    {
        void SetGameState(GameState newState);
        
        StateType CurrentStateType { get; }
        ICubeManager CubeManager { get; set; }
        ICubeUI CubeUI { get; }

    }
}