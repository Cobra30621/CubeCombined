using UnityEngine;

namespace GameState
{
    public class GameProgressHandler : GameContext
    {
        private GameState _gameState;

        public GameState CurrentState => _gameState;

        [ContextMenu("開始遊戲")]
        public void StartGame()
        {
            SetGameState(new IdleState());
        }


        public void SetGameState(GameState newState)
        {
            _gameState?.Exit();
            _gameState = newState;
            
            newState.Init(this);
            newState.Enter();
        }

        public void Update()
        {
            _gameState.Update();
        }
    }
}