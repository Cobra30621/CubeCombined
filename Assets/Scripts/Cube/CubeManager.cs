using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Cube
{
    public class CubeManager : ICubeManager
    {
        public int MAX_ROW = 6;
        public int MAX_COLUMN = 3;
        private MapHandler _mapHandler;

        private int currentCube;
        
        public override void Init()
        {
            _mapHandler = new MapHandler(MAX_ROW, MAX_COLUMN);
        }
        
        public override int GetCurrentCube()
        {
            throw new System.NotImplementedException();
        }

        public override void AddCube(int column)
        {
            _mapHandler.AddCube(column, currentCube);
        }

        private IEnumerator CombinedCoroutine()
        {


            yield return null;
        }
        

        public override bool CanRelease(int column)
        {
            return false;
        }


        public override List<Map> GerMergeMap()
        {
            return _mapHandler.GetMergedMaps();
        }
    }
}