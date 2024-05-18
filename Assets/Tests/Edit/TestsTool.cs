using Cube;
using GameState;
using Input;
using NSubstitute;

namespace Tests.Edit
{
    public class TestsTool
    {
        public static GameProgressHandler InitGameProgressHandler()
        {
            var handler = new GameProgressHandler();
            var initCubeManager = InitCubeManager();
            var cubeUI = InitCubeUI();
            handler.Init(initCubeManager, cubeUI);

            return handler;
        }

        private static ICubeUI InitCubeUI()
        {
            var cubeUI = Substitute.For<ICubeUI>();
            return cubeUI;
        }

        private static ICubeManager InitCubeManager()
        {
            var cubeManager = Substitute.For<ICubeManager>();
            return cubeManager;
        }

        public static IInputSystem InitInputSystem()
        {
            IInputSystem inputSystem = Substitute.For<IInputSystem>();
            InputSetting.SetInputSystem(inputSystem);
            return inputSystem;
        }
        
        
    }
}