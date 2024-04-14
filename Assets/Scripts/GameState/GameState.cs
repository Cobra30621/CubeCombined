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
        protected GameContext _context;
        public void Init(GameContext context)
        {
            _context = context;
        }

        public abstract StateType GetStateType();
        
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}