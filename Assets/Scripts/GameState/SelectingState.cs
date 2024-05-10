using Input;
using UnityEngine;

namespace GameState
{
    public class SelectingState : GameState
    {
        public override StateType GetStateType() => StateType.Selecting;

        private IInputSystem InputSystem => InputSetting.InputSystem;

        public override void EnterState()
        {
            
        }

        public override void Update()
        {
            

            if (InputSystem.IsRelease())
            {
                Debug.Log("IsRelease");
                int row = InputSystem.GetRow();
                Release(row);
            }
        }

        private void Release(int column)
        {
            Debug.Log($"{CubeManager.CanRelease(column)}");
            if (CubeManager.CanRelease(column))
            {
                CubeManager.AddCube(column);
                _gameContext.SetGameState(new ProgressingState());
            }
            else
            {
                _gameContext.SetGameState(new IdleState());
            }
        }
    }
}