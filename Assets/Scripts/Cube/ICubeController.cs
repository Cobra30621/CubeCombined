using System.Collections.Generic;

namespace Cube
{
    public interface ICubeController
    {
        void Init(ICubeManager cubeManager);

        
        void UpdateCurrentCube(int number);
        
        public void ShowPreviewAt(int column);
        
        public void ClosePreview();
        
        public void PlayAddCubeAnimation(List<Map> mergeMap);

        public bool IsAnimationComplete();
    }
}