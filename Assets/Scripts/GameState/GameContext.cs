using Cube;

namespace GameState
{
    public interface GameContext
    {
        void SetGameState(GameState newState);
        
        StateType CurrentState { get; }
        ICubeManager CubeManager { get; set; }
        CubeUI CubeUI { get; }

        void UpdateSelectingRow(int row);
    }
}