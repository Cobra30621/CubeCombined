namespace Input
{
    public class InputSetting
    {
        public static IInputSystem InputSystem => _inputSystem ??= new PCInputSystem();

        private static IInputSystem _inputSystem;

        
        public static void SetInputSystem(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }
    }
}