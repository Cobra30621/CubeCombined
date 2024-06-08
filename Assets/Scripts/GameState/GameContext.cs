using Cube;
using Event;

namespace GameState
{
    public interface GameContext
    {
        void SetGameState(GameState newState);

        void GameOver();
        
        StateType CurrentStateType { get; }
        ICubeManager CubeManager { get; set; }
        ICubeController CubeController { get; set; }
        IEventController EventController { get; set; }
        
        IGameOverController GameOverController { get; set; }

    }
}