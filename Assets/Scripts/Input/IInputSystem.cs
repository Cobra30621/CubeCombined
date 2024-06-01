using System.Numerics;

namespace Input
{
    public interface IInputSystem
    {
        bool IsClick();
        
        bool IsRelease();

        Vector2 GetInputPosition();

        int Column();
    }
}