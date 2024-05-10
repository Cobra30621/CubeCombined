using System.Collections.Generic;

namespace Cube
{
    public interface ICubeUI
    {

        
        public void PlayAddCubeAnimation(List<Map> mergeMap);

        public bool IsAnimationComplete();
    }
}