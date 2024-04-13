namespace GameState
{
    public abstract class GameState
    {
        protected GameContext _context;
        public void Init(GameContext context)
        {
            _context = context;
        }
        
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }
}