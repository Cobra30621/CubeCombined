using System.Collections.Generic;
using Event;
using UnityEngine;

namespace Cube
{
    public interface ICubeManager
    {
        /// <summary>
        /// The maximum number of rows in the game.
        /// </summary>
        public int MaxRow { get; }
        /// <summary>
        /// The maximum number of columns in the game.
        /// </summary>
        public int MaxColumn { get; }
        
        List<Map> MergeMap { get; }

        int CurrentCube{get;}

        
        void SetCubeUI(ICubeController cubeController);
        
        void StartGame();

        /// <summary>
        /// 放置方塊
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        bool AddCube(int column);

        /// <summary>
        /// 檢查是否能在某處放置方塊
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        bool CanReleaseAt(int column);

        /// <summary>
        /// 檢查現在的情況，是否還能放置方塊
        /// </summary>
        /// <returns></returns>
        bool CanRelease();
        
        List<CubeEvent> GetCubeEvents();

        int GetFirstZeroRowAt(int column);
    }
}