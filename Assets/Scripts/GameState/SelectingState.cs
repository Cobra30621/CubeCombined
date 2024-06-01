using Input;
using Sirenix.OdinInspector;
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
            int column = InputSystem.Column();
            Debug.Log($"Column {column}");
         
            
            if (InputSystem.IsRelease())
            {
                Debug.Log("IsRelease");
                CubeManager.ClosePreview();
                Release(column);
            }
        }

        [Button("放置")]
        private void Release(int column)
        {
            Debug.Log($"{CubeManager.CanReleaseAt(column)}");
            var canRelease = CubeManager.AddCube(column);
            if (canRelease)
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