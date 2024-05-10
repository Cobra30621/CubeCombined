using GameState;
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
            var handler = TestsTool.InitGameProgressHandler();
            
            // Act
            handler.StartGame();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentStateType);
        }
        
        
        [Test]
        public void IdleToSelecting_WhenClick()
        {
            // Arrange
            var inputSystem = TestsTool.InitInputSystem();
            
            var handler = TestsTool.InitGameProgressHandler();
            handler.StartGame();
            
            // Act
            inputSystem.IsClick().Returns(true);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Selecting, handler.CurrentStateType);
        }
        
        [Test]
        public void KeepIdle_WhenNotClick()
        {
            // Arrange
            var inputSystem = TestsTool.InitInputSystem();
            
            var handler = TestsTool.InitGameProgressHandler();
            handler.StartGame();
            
            // Act
            inputSystem.IsClick().Returns(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentStateType);
        }
        

        #endregion
        
        #region Selecting

        [Test]

        public void SelectingToProgressing_WhenRelease_AndCanRelease()
        {
            // Arrange
            var inputSystem = TestsTool.InitInputSystem();
            
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            // Act
            inputSystem.IsRelease().Returns(true);
            handler.CubeManager.CanRelease(1).ReturnsForAnyArgs(true);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Progressing, handler.CurrentStateType);
        }
        
        [Test]

        public void SelectingToProgressing_WhenRelease_AndCanNotRelease()
        {
            // Arrange
            var inputSystem = TestsTool.InitInputSystem();
            
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            // Act
            inputSystem.IsRelease().Returns(true);
            handler.CubeManager.CanRelease(1).ReturnsForAnyArgs(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentStateType);
        }
        
        [Test]

        public void KeepSelecting_WhenNotRelease()
        {
            // Arrange
            var inputSystem = TestsTool.InitInputSystem();
            
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new SelectingState());
            
            // Act
            inputSystem.IsRelease().Returns(false);
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Selecting, handler.CurrentStateType);
        }
        
        #endregion

        #region Progressing

        [Test]
        public void KeepProgressing_WhenNotAnimationComplete()
        {
            // Arrange
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new ProgressingState());
            
            // Update
            handler.CubeUI.IsAnimationComplete().Returns(false);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Progressing, handler.CurrentStateType);
        }
        
        [Test]
        public void ProgressingToIdle_WhenAnimationComplete()
        {
            // Arrange
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new ProgressingState());
            
            // Update
            handler.CubeUI.IsAnimationComplete().Returns(true);
            
            handler.Update();
            
            // Assert
            Assert.AreEqual(StateType.Idle, handler.CurrentStateType);
        }

        #endregion

    }
}