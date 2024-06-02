using Input;
using UnityEngine;

namespace GameState
{
    public class IdleState : GameState
    {
        public override StateType GetStateType() => StateType.Idle;
        public override void EnterState()
        {
            if (!CubeManager.CanRelease())
            {
                _gameContext.GameOver();
            }
        }


        public override void Update()
        {
            if (InputSetting.InputSystem.IsClick())
            {
                Debug.Log("Is Click");
                int column = InputSetting.InputSystem.Column();
                CubeManager.ShowPreview(column);
                
                _gameContext.SetGameState(new SelectingState());
            }   
        }
    }
}