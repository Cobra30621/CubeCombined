using Cube;
using GameState;
using Input;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Edit
{
    public class GameStateChangedTests
    {
        #region Idle

        [Test]
        public void Idle_WhenStartGame()
        {
            // Arrange
            var handler = InitGameProgressHandler();
            
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
            
            var handler = InitGameProgressHandler();
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
            
            var handler = InitGameProgressHandler();
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

        public void SelectingToProgressing_WhenRelease_AndCanRelease()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = InitGameProgressHandler();
            handler.SetGameState(new SelectingStateChanged());
            
            // Act
            inputSystem.IsRelease().Returns(true);
            handler.CubeManager.CanRelease(1).ReturnsForAnyArgs(true);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Progressing, handler.CurrentState);
        }
        
        [Test]

        public void SelectingToProgressing_WhenRelease_AndCanNotRelease()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = InitGameProgressHandler();
            handler.SetGameState(new SelectingStateChanged());
            
            // Act
            inputSystem.IsRelease().Returns(true);
            handler.CubeManager.CanRelease(1).ReturnsForAnyArgs(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }
        
        [Test]

        public void KeepSelecting_WhenNotRelease()
        {
            // Arrange
            var inputSystem = InitInputSystem();
            
            var handler = InitGameProgressHandler();
            handler.SetGameState(new SelectingStateChanged());
            
            // Act
            inputSystem.IsRelease().Returns(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Selecting, handler.CurrentState);
        }
        
        #endregion

        #region Progressing

        [Test]
        public void KeepProgressing_WhenNotAnimationComplete()
        {
            // Arrange
            var handler = InitGameProgressHandler();
            handler.SetGameState(new ProgressingStateChanged());
            
            // Update
            handler.CubeUI.IsAnimationComplete().Returns(false);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Progressing, handler.CurrentState);
        }
        
        [Test]
        public void ProgressingToIdle_WhenAnimationComplete()
        {
            // Arrange
            var handler = InitGameProgressHandler();
            handler.SetGameState(new ProgressingStateChanged());
            
            // Update
            handler.CubeUI.IsAnimationComplete().Returns(true);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentState);
        }

        #endregion

        #region Tools


        private static GameProgressHandler InitGameProgressHandler()
        {
            var handler = new GameProgressHandler();
            var initCubeManager = InitCubeManager();
            var cubeUI = InitCubeUI();
            handler.Init(initCubeManager, cubeUI);

            return handler;
        }


        private static CubeUI InitCubeUI()
        {
            var cubeUI = Substitute.For<CubeUI>();
            return cubeUI;
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

        #endregion
    }
}