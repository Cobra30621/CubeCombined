using Cube;
using GameState;
using Input;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Edit
{
    public class GameStateTests
    {
        #region Idle

        [Test]
        public void Idle_WhenStartGame()
        {
            // Arrange
            var handler = new GameProgressHandler();
            
            // Act
            handler.StartGame();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }
        
        
        [Test]
        public void IdleToSelecting_WhenClick()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = new GameProgressHandler();
            handler.StartGame();
            
            // Act
            inputSystem.IsClick().Returns(true);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Selecting, handler.CurrentState);
        }
        
        [Test]
        public void KeepIdle_WhenNotClick()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = new GameProgressHandler();
            handler.StartGame();
            
            // Act
            inputSystem.IsClick().Returns(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }
        

        #endregion
        
        #region Selecting

        [Test]

        public void SelectingToProgressing_WhenRelease()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = new GameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            var cubeManager = InitCubeManager();
            handler.Init(cubeManager);
            
            // Act
            inputSystem.IsRelease().Returns(true);
            cubeManager.CanRelease(1).ReturnsForAnyArgs(true);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Progressing, handler.CurrentState);
        }
        
        [Test]

        public void KeepSelecting_WhenNotRelease()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = new GameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            // Act
            inputSystem.IsRelease().Returns(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Selecting, handler.CurrentState);
        }
        
        
        [Test]

        public void SelectingToIdle_WhenReleaseFail()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = new GameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            var cubeManager = InitCubeManager();
            handler.Init(cubeManager);
            
            // Act
            inputSystem.IsRelease().Returns(true);
            cubeManager.CanRelease(1).ReturnsForAnyArgs(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }

        #endregion

        #region Progressing

        [Test]
        public void KeepProgressing_WhenNotAnimationComplete()
        {
            // Arrange
            var handler = new GameProgressHandler();
            handler.SetGameState(new ProgressingState());
            
            var cubeManager = InitCubeManager();
            handler.Init(cubeManager);
            
            // Update
            cubeManager.IsPlayingAnimation().Returns(false);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }
        
        [Test]
        public void ProgressingToIdle_WhenAnimationComplete()
        {
            // Arrange
            var handler = new GameProgressHandler();
            handler.SetGameState(new ProgressingState());
            
            var cubeManager = InitCubeManager();
            handler.Init(cubeManager);
            
            // Update
            cubeManager.IsPlayingAnimation().Returns(false);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }

        #endregion


        private static ICubeManager SetCubeManager(GameProgressHandler handler)
        {
            var initCubeManager = InitCubeManager();
            handler.Init(initCubeManager);

            return initCubeManager;
        }
        

        private static ICubeManager InitCubeManager()
        {
            var cubeManager = Substitute.For<ICubeManager>();
            return cubeManager;
        }


        private static IInputSystem InitInputSystem()
        {
            IInputSystem inputSystem = Substitute.For<IInputSystem>();
            InputSetting.SetInputSystem(inputSystem);
            return inputSystem;
        }
    }
}