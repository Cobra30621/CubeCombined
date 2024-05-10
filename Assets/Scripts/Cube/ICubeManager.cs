using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public abstract class ICubeManager
    {
        public abstract void Init();
        
        public abstract  int GetCurrentCube();
        
        public abstract void AddCube(int column);

        public abstract bool CanRelease(int column);
        
        public abstract List<Map> GerMergeMap();
    }
}