using System.Collections;
using System.Collections.Generic;
using Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cube
{
    public class CubeManager : ICubeManager
    {
        public int MAX_ROW = 6;
        public int MAX_COLUMN = 3;
        [SerializeField] private MapHandler _mapHandler;

        [SerializeField] private int currentCube = 1;
        
        public override void Init()
        {
            _mapHandler = new MapHandler(MAX_ROW, MAX_COLUMN);
        }
        
        public override int GetCurrentCube()
        {
            return currentCube;
        }

        public override Map GetCurrentMap()
        {
            return _mapHandler.GameMap;
        }


        [Button("放置方塊")]
        private void AddCube(int column, int number)
        {
            currentCube = number;
            AddCube(column);
        }

        
        public override bool AddCube(int column)
        {
            return _mapHandler.AddCube(column, currentCube);;
        }



        public override bool CanReleaseAt(int column)
        {
            return _mapHandler.CanRelease(column);
        }

        public override bool CanRelease()
        {
            for (int i = 0; i < MAX_COLUMN; i++)
            {
                bool canRelease = CanReleaseAt(i);
                if (canRelease)
                {
                    return true;
                }
            }

            return false;
        }


        public override List<Map> GerMergeMap()
        {
            return _mapHandler.GetMergedMaps();
        }
    }
}