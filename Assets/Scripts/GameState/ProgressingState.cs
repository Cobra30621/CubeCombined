using System.Collections;
using Cube;
using Input;
using UnityEngine;

namespace GameState
{
    public class ProgressingState : GameState
    {
        public override StateType GetStateType() => StateType.Progressing;

        private ICubeUI _cubeUI => _gameContext.CubeUI;
        
        public override void EnterState()
        {
            var mergeMap = CubeManager.GerMergeMap();
            _cubeUI.PlayAddCubeAnimation(mergeMap);
            
        }


        public override void Update()
        {
            if (_cubeUI.IsAnimationComplete())
            {
                OnAnimationComplete();
            }
        }

        private void OnAnimationComplete()
        {
            CheckEvent();
            _gameContext.SetGameState(new IdleState());
        }

        private void CheckEvent()
        {
            Debug.Log("Check Event");
        }
    }
}