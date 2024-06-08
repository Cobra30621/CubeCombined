using System.Collections;
using System.Collections.Generic;
using Core;
using Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cube
{
    public class CubeManager : ICubeManager
    {
        public int MAX_ROW = 6;
        public int MAX_COLUMN = 3;
        
        [SerializeField] private MapHandler _mapHandler;
        [SerializeField] private ICubeRecorder _cubeRecorder;
        private ICubeController _cubeController;

        public int CurrentCube => currentCube;
        [SerializeField] private int currentCube = 1;
        public List<Map> MergeMap => _mapHandler.GetMergedMaps();

        public void SetCubeUI(ICubeController cubeController)
        {
            _cubeController = cubeController;
        }
        
        
        public virtual void StartGame()
        {
            _mapHandler = new MapHandler(MAX_ROW, MAX_COLUMN);
            _cubeRecorder = new CubeRecorder();
            _mapHandler.SetCubeRecorder(_cubeRecorder);

            UpdateCurrentCube();
        }

        
        
        public List<CubeEvent> GetCubeEvents()
        {
            return _cubeRecorder.GetThisTurnEvent();
        }
        
        
        public virtual bool AddCube(int column)
        {
            var addCube = _mapHandler.AddCube(column, currentCube);
            UpdateCurrentCube();
            return addCube;
        }



        public virtual bool CanReleaseAt(int column)
        {
            return _mapHandler.CanRelease(column);
        }

        public virtual bool CanRelease()
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
        
        public int GetFirstZeroRowAt(int column)
        {
            return _mapHandler.GetFirstZeroRowAt(column);
        }

        
        private void UpdateCurrentCube()
        {
            currentCube = _cubeRecorder.GetNumberInRange();
            _cubeController.UpdateCurrentCube(currentCube);
        }



    }
}