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
        [SerializeField] private CubeRecorder _cubeRecorder;

        private ICubeUI _cubeUI;
        private IEventController _eventController;
        

        [SerializeField] private int currentCube = 1;

        
        public virtual void Init()
        {
            _mapHandler = new MapHandler(MAX_ROW, MAX_COLUMN);
            _cubeRecorder = new CubeRecorder();
            _mapHandler.SetCubeRecorder(_cubeRecorder);

            UpdateCurrentCube();
        }

        
        
        public void SetCubeUI(ICubeUI cubeUI)
        {
            _cubeUI = cubeUI;
        }
        
        public void SetEventUI(IEventController eventController)
        {
            _eventController = eventController;
        }
        
        private void UpdateCurrentCube()
        {
            currentCube = _cubeRecorder.GetNumberInRange();
            _cubeUI.UpdateCurrentCube(currentCube);
        }

        public void ShowCubeEvents()
        {
            var thisTurnEvent = _cubeRecorder.GetThisTurnEvent();
            _eventController.ShowEvent(thisTurnEvent);
        }


        public virtual int GetCurrentCube()
        {
            return currentCube;
        }
        
        public virtual void ShowPreview(int column)
        {
            var firstZeroRowAt = _mapHandler.GetFirstZeroRowAt(column);
            if (firstZeroRowAt != -1)
            {
                _cubeUI.ShowCubePreviewAt(column, firstZeroRowAt, currentCube);
            }
        }

        public void ClosePreview()
        {
            _cubeUI.ClosePreview();
        }

        

        public virtual Map GetCurrentMap()
        {
            return _mapHandler.GameMap;
        }


        [Button("放置方塊")]
        private void AddCube(int column, int number)
        {
            currentCube = number;
            AddCube(column);
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


        public virtual List<Map> GerMergeMap()
        {
            return _mapHandler.GetMergedMaps();
        }

        
        public MapHandler MapHandler { get; }
        

    }
}