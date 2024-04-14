using Input;
using UnityEngine;

namespace GameState
{
    public class SelectingState : GameState
    {
        public override StateType GetStateType() => StateType.Selecting;
        private IInputSystem InputSystem => InputSetting.InputSystem;
        
        public override void Update()
        {
            int row = InputSystem.GetRow();
            // _gameContext.UpdateSelectingRow(row);

            if (InputSystem.IsRelease())
            {
                Debug.Log("IsRelease");
                Release(row);
            }
        }

        private void Release(int row)
        {
            Debug.Log($"{CubeManager.CanRelease(row)}");
            if (CubeManager.CanRelease(row))
            {
                _gameContext.SetGameState(new ProgressingState());
            }
            else
            {
                _gameContext.SetGameState(new IdleState());
            }
        }
    }
}