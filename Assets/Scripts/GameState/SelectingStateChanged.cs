using Input;
using UnityEngine;

namespace GameState
{
    public class SelectingStateChanged : GameState
    {
        public override StateType GetStateType() => StateType.Selecting;

        private IInputSystem InputSystem => InputSetting.InputSystem;

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            int row = InputSystem.GetRow();
            _gameContext.UpdateSelectingRow(row);

            if (InputSystem.IsRelease())
            {
                Debug.Log("IsRelease");
                Release(row);
            }
        }

        private void Release(int column)
        {
            Debug.Log($"{CubeManager.CanRelease(column)}");
            if (CubeManager.CanRelease(column))
            {
                CubeManager.AddCube(column);
                _gameContext.SetGameState(new ProgressingStateChanged());
            }
            else
            {
                _gameContext.SetGameState(new IdleStateChanged());
            }
        }
    }
}