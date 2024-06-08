using System.Collections.Generic;
using Event;
using UnityEngine;

namespace Cube
{
    /// <summary>
    /// CubeManager class manages the game logic for the cube game.
    /// </summary>
    public class CubeManager : ICubeManager
    {
        [SerializeField] private int maxRow = 6;
        [SerializeField] private int maxColumn = 3;
        
        /// <summary>
        /// The MapHandler instance used to handle the game map.
        /// </summary>
        [SerializeField] private MapHandler _mapHandler;

        /// <summary>
        /// The ICubeRecorder instance used to record cube events.
        /// </summary>
        [SerializeField] private ICubeRecorder _cubeRecorder;

        /// <summary>
        /// The ICubeController instance used to update the current cube UI.
        /// </summary>
        private ICubeController _cubeController;

        /// <summary>
        /// Gets the current cube number.
        /// </summary>
        public int CurrentCube => currentCube;

        /// <summary>
        /// The initial cube number.
        /// </summary>
        [SerializeField] private int currentCube = 1;

        public int MaxRow => maxRow;
        public int MaxColumn => maxColumn;
        
        /// <summary>
        /// Gets the merged maps from the MapHandler instance.
        /// </summary>
        public List<Map> MergeMap => _mapHandler.GetMergedMaps();

        /// <summary>
        /// Sets the ICubeController instance to update the current cube UI.
        /// </summary>
        /// <param name="cubeController">The ICubeController instance to set.</param>
        public void SetCubeUI(ICubeController cubeController)
        {
            _cubeController = cubeController;
        }

        /// <summary>
        /// Starts the game by initializing the MapHandler and CubeRecorder instances.
        /// </summary>
        public virtual void StartGame()
        {
            _mapHandler = new MapHandler(maxRow, maxColumn);
            _cubeRecorder = new CubeRecorder();
            _mapHandler.SetCubeRecorder(_cubeRecorder);

            UpdateCurrentCube();
        }

        /// <summary>
        /// Gets the list of CubeEvents recorded by the CubeRecorder instance.
        /// </summary>
        /// <returns>The list of CubeEvents.</returns>
        public List<CubeEvent> GetCubeEvents()
        {
            return _cubeRecorder.GetThisTurnEvent();
        }

        /// <summary>
        /// Adds a cube to the game map at the specified column.
        /// </summary>
        /// <param name="column">The column where the cube will be added.</param>
        /// <returns>True if the cube was added successfully, false otherwise.</returns>
        public virtual bool AddCube(int column)
        {
            var addCube = _mapHandler.AddCube(column, currentCube);
            UpdateCurrentCube();
            return addCube;
        }

        /// <summary>
        /// Checks if a cube can be released at the specified column.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>True if a cube can be released at the specified column, false otherwise.</returns>
        public virtual bool CanReleaseAt(int column)
        {
            return _mapHandler.CanRelease(column);
        }

        /// <summary>
        /// Checks if a cube can be released anywhere on the game map.
        /// </summary>
        /// <returns>True if a cube can be released anywhere on the game map, false otherwise.</returns>
        public virtual bool CanRelease()
        {
            for (int i = 0; i < maxColumn; i++)
            {
                bool canRelease = CanReleaseAt(i);
                if (canRelease)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the first row index where a zero cube can be placed at the specified column.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>The first row index where a zero cube can be placed, or -1 if no such row exists.</returns>
        public int GetFirstZeroRowAt(int column)
        {
            return _mapHandler.GetFirstZeroRowAt(column);
        }

        /// <summary>
        /// Updates the current cube number based on the CubeRecorder instance.
        /// </summary>
        private void UpdateCurrentCube()
        {
            currentCube = _cubeRecorder.GetNumberInRange();
            _cubeController.UpdateCurrentCube(currentCube);
        }
    }
}