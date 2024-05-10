using Cube;
using JetBrains.Annotations;
using UnityEngine;

namespace GameState
{
    public class GameProgressHandler : GameContext
    {
        private GameState _gameStateChanged;

        public int CurrentRow;

        public StateType CurrentState => _gameStateChanged.GetStateType();
        public ICubeManager CubeManager { get; set; }
        
        public CubeUI CubeUI { get; set; }

        public void Init(ICubeManager cubeManager, CubeUI cubeUI)
        {
            CubeManager = cubeManager;
            CubeUI = cubeUI;
        }
        

        public void UpdateSelectingRow(int row)
        {
            int currentCube = CubeManager.GetCurrentCube();
            CubeUI.ShowSelecting(row, currentCube);
            
            Debug.Log("更新 UI 的 Row");
        }

        [ContextMenu("開始遊戲")]
        public void StartGame()
        {    
            SetGameState(new IdleStateChanged());
        }


        public void SetGameState(GameState newState)
        {
            Debug.Log($"SetGameState: {newState.GetStateType()}");
            _gameStateChanged = newState;
            newState.Init(this);
        }

        public void Update()
        {
            _gameStateChanged.Update();
        }
    }
}