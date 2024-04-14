using Input;

namespace GameState
{
    public class ProgressingState : GameState
    {
        public override StateType GetStateType() => StateType.Progressing;

        public override void Update()
        {
            if (!CubeManager.IsPlayingAnimation())
            {
                _gameContext.SetGameState(new IdleState());
            }
        }
    }
}