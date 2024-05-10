using System.Collections.Generic;

namespace Cube
{
    public interface CubeUI
    {
        public void ShowSelecting(int row, int num);

        public void CloseSelecting();

        public void PlayAddCubeAnimation(List<Map> mergeMap);

        public bool IsAnimationComplete();
    }
}