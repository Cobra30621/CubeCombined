using GameState;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Edit
{
    public class GameStateAndCubeUITests
    {
        #region Selecting
        // TODO
        public void ShowSelecting_WhenSelectingState()
        {
            // Arrange
            var handler = TestsTool.InitGameProgressHandler();
            handler.SetGameState(new SelectingState());
            var inputSystem = TestsTool.InitInputSystem();
            inputSystem.IsRelease().ReturnsForAnyArgs(false);
            
            // Act
            handler.Update();
            
            // Assert
            // Assert.IsTrue(handler.CubeUI.IsShowSelecting());
        }
        

        #endregion
    }
}