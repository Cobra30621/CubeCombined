using Cube;
using Event;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameState
{
    public class GameProgressHandler :  GameContext
    {
        [SerializeField] private GameState _gameState;

        public StateType CurrentStateType => _gameState.GetStateType();
        public ICubeManager CubeManager { get; set; }
        public ICubeController CubeController { get; set; }
        public IEventController EventController { get; set; }
        public IGameOverController GameOverController { get; set; }
        

        public void Init(ICubeManager cubeManager, ICubeController cubeController, 
            IEventController eventController, IGameOverController gameOverController)
        {
            CubeManager = cubeManager;
            CubeController = cubeController;
            EventController = eventController;
            GameOverController = gameOverController;
        }
        

        public void StartGame()
        {    
            CubeManager.StartGame();
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
        
        public void GameOver()
        {
            GameOverController.GameOver();
        }
    }
}