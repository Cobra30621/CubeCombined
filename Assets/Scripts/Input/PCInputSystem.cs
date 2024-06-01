using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Input
{
    public class PCInputSystem : IInputSystem
    {
        private KeyCode[] _keyCodes = new []
        {
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G
        };
        
        
        public bool IsClick()
        {
            foreach (var keyCode in _keyCodes)
            {
                if (UnityEngine.Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsRelease()
        {
            foreach (var keyCode in _keyCodes)
            {
                if (UnityEngine.Input.GetKeyUp(keyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public Vector2 GetInputPosition()
        {
            throw new System.NotImplementedException();
        }

        public int Column()
        {
            for (var index = 0; index < _keyCodes.Length; index++)
            {
                var keyCode = _keyCodes[index];
                if (UnityEngine.Input.GetKeyUp(keyCode) || UnityEngine.Input.GetKeyDown(keyCode))
                {
                    return index;
                }
            }

            return -1;
        }
    }
}