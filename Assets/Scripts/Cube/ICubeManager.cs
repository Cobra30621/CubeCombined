using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public abstract class ICubeManager
    {
        public abstract void Init();
        
        public abstract int GetCurrentCube();

        public abstract Map GetCurrentMap();
        
        /// <summary>
        /// 放置方塊
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public abstract bool AddCube(int column);
        
        /// <summary>
        /// 檢查是否能在某處放置方塊
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public abstract bool CanReleaseAt(int column);
        
        /// <summary>
        /// 檢查現在的情況，是否還能放置方塊
        /// </summary>
        /// <returns></returns>
        public abstract bool CanRelease();
        
        public abstract List<Map> GerMergeMap();
    }
}