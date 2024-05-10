using Cube;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class GameProgressHandler :  GameContext
    {
        [SerializeField] private GameState _gameState;

        public int CurrentRow;

        public StateType CurrentStateType => _gameState.GetStateType();
        public ICubeManager CubeManager { get; set; }
        
        public ICubeUI CubeUI { get; set; }

        public void Init(ICubeManager cubeManager, ICubeUI cubeUI)
        {
            CubeManager = cubeManager;
            CubeUI = cubeUI;
        }
        

        public void StartGame()
        {    
            CubeManager.Init();
            SetGameState(new IdleState());
        }

        [Button("設置遊戲狀態")]

        public void SetGameState(GameState newState)
        {
            Debug.Log($"SetGameState: {newState.GetStateType()}");
            _gameState = newState;
            newState.Init(this);
            newState.EnterState();
        }

        public void Update()
        {
            _gameState.Update();
        }
    }
}