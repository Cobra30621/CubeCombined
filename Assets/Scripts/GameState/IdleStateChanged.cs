using Input;
using UnityEngine;

namespace GameState
{
    public class IdleStateChanged : GameState
    {
        public override StateType GetStateType() => StateType.Idle;
        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }


        public override void Update()
        {
            if (InputSetting.InputSystem.IsClick())
            {
                Debug.Log("Is Click");
                _gameContext.SetGameState(new SelectingStateChanged());
            }   
        }
    }
}