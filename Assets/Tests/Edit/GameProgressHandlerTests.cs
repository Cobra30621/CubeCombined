using GameState;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Editor
{
    public class GameProgressHandlerTests
    {
        [Test]
        public void TestSetGameState()
        {
            GameProgressHandler gameProgressHandler = new GameProgressHandler();
            var newState = Substitute.For<GameState.GameState>();
            gameProgressHandler.SetGameState(newState);
            Assert.AreSame(newState, gameProgressHandler.CurrentState);
        }

        [Test]
        public void TestUpdate()
        {
            GameProgressHandler gameProgressHandler = new GameProgressHandler();
            var newState = Substitute.For<GameState.GameState>();
            gameProgressHandler.SetGameState(newState);
            gameProgressHandler.Update();
        }
    }

}