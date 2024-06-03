using System.Collections.Generic;
using Event;
using UnityEngine;

namespace Cube
{
    public interface ICubeManager
    {

        void SetCubeUI(ICubeUI cubeUI);
        
        void Init();
        int GetCurrentCube();
        Map GetCurrentMap();

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

        List<Map> GerMergeMap();
        MapHandler MapHandler { get; }
        void ShowPreview(int column);
        
        void ClosePreview();
        void SetEventUI(IEventController eventController);
        
        void ShowCubeEvents();
        
    }
}