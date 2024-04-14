using Input;
using UnityEngine;

namespace GameState
{
    public class IdleState : GameState
    {
        public override StateType GetStateType() => StateType.Idle;



        public override void Update()
        {
            if (InputSetting.InputSystem.IsClick())
            {
                Debug.Log("Is Click");
                _gameContext.SetGameState(new SelectingState());
            }   
        }
    }
}