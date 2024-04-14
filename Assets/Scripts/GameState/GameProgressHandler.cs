using Cube;
using JetBrains.Annotations;
using UnityEngine;

namespace GameState
{
    public class GameProgressHandler : GameContext
    {
        private GameState _gameState;

        public int CurrentRow;

        public StateType CurrentState => _gameState.GetStateType();
        public ICubeManager CubeManager { get; set; }
        
        public CubeUI CubeUI { get; set; }

        public void Init(ICubeManager cubeManager)
        {
            CubeManager = cubeManager;
        }
        

        public void UpdateSelectingRow(int row)
        {
            CubeUI.ShowSelecting(row, CubeManager.GetCurrentCube());
            
            Debug.Log("更新 UI 的 Row");
        }

        [ContextMenu("開始遊戲")]
        public void StartGame()
        {    
            SetGameState(new IdleState());
        }


        public void SetGameState(GameState newState)
        {
            Debug.Log($"SetGameState: {newState.GetStateType()}");
            _gameState = newState;
            newState.Init(this);
        }

        public void Update()
        {
            _gameState.Update();
        }
    }
}