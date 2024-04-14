using UnityEngine;

namespace Cube
{
    public abstract class ICubeManager
    {
        public abstract void Init();
        
        public abstract  int GetCurrentCube();
        
        public abstract void AddCube(int row, int index);

        public abstract bool CanRelease(int row);

        public abstract bool IsPlayingAnimation();
    }
}