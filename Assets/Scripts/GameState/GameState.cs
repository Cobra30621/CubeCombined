using Cube;

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
        
        public void Init(GameContext context)
        {
            _gameContext = context;
        }

        public abstract StateType GetStateType();
        
        public abstract void Update();
    }
}