using System.Collections.Generic;

namespace Cube
{
    public interface ICubeUI
    {
        public void UpdateCubeDisplay(Map map);
        public void InitCubes(int column, int row);
        public void PlayAddCubeAnimation(List<Map> mergeMap);

        public bool IsAnimationComplete();
    }
}