namespace GameState
{
    public class IdleState : GameState
    {
        public override StateType GetStateType() => StateType.Idle;

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}