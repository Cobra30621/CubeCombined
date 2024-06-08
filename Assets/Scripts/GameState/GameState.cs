using Cube;
using Event;

namespace GameState
{
    public enum StateType
    {
        Idle,
        Selecting,
        Progressing
    }
    
    public abstract class GameState
    {
        protected GameContext _gameContext;
        protected ICubeManager CubeManager => _gameContext.CubeManager;
        protected ICubeController CubeController => _gameContext.CubeController;
        protected IEventController EventController => _gameContext.EventController;
        
        public void Init(GameContext context)
        {
            _gameContext = context;
        }

        public abstract StateType GetStateType();

        public abstract void EnterState();
        
        public abstract void Update();
    }
}