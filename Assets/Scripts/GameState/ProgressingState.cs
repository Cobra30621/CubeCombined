using System.Collections;
using Cube;
using Input;
using UnityEngine;

namespace GameState
{
    public class ProgressingState : GameState
    {
        public override StateType GetStateType() => StateType.Progressing;

        
        public override void EnterState()
        {
            var mergeMap = CubeManager.MergeMap;
            CubeController.PlayAddCubeAnimation(mergeMap);
            
        }


        public override void Update()
        {
            if (CubeController.IsAnimationComplete())
            {
                OnAnimationComplete();
            }
        }

        private void OnAnimationComplete()
        {
            var cubeEvents = CubeManager.GetCubeEvents();
            EventController.ShowEvent(cubeEvents);
            
            _gameContext.SetGameState(new IdleState());
        }

        
    }
}