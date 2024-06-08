using Cube;
using Event;
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
            var cubeController = InitCubeController();
            var eventController = InitEventController();
            var gameOverController = InitGameOverController();
            handler.Init(initCubeManager, cubeController, eventController, gameOverController);

            return handler;
        }

        private static ICubeController InitCubeController()
        {
            var cubeUI = Substitute.For<ICubeController>();
            return cubeUI;
        }

        private static ICubeManager InitCubeManager()
        {
            var cubeManager = Substitute.For<ICubeManager>();
            return cubeManager;
        }

        private static IEventController InitEventController()
        {
            var eventController = Substitute.For<IEventController>();
            return eventController;
        }

        private static IGameOverController InitGameOverController()
        {
            var gameOverController = Substitute.For<IGameOverController>();
            return gameOverController;
        }

    public static IInputSystem InitInputSystem()
        {
            IInputSystem inputSystem = Substitute.For<IInputSystem>();
            InputSetting.SetInputSystem(inputSystem);
            return inputSystem;
        }
        
        
    }
}