using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Cube
{
    public class CubeManager : ICubeManager
    {
        public Dictionary<Vector2, int> numbers;
        private bool isPlayingAnimation;


        public override void Init()
        {
            numbers = new Dictionary<Vector2, int>();
        }

        public override int GetCurrentCube()
        {
            throw new System.NotImplementedException();
        }

        public override void AddCube(int row, int index)
        {
            
        }

        private IEnumerator CombinedCoroutine()
        {


            yield return null;
        }
        

        public override bool CanRelease(int row)
        {
            return false;
        }

        public override bool IsPlayingAnimation()
        {
            return isPlayingAnimation;
        }
    }
}