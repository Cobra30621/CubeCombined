using NUnit.Framework;
using UnityEngine;
using GameState;

namespace Tests.Editor
{
    public class GameProgressHandlerTests
    {
        [Test]
        public void TestStartGame()
        {
            GameProgressHandler gameProgressHandler = new GameProgressHandler();
            Assert.AreEqual(typeof(IdleState), gameProgressHandler.CurrentState.GetType());
        }

        [Test]
        public void TestSetGameState()
        {
            GameProgressHandler gameProgressHandler = new GameProgressHandler();
            // GameState newState = new MockGameState();
            // gameProgressHandler.SetGameState(newState);
            // Assert.AreSame(newState, gameProgressHandler.CurrentState);
        }

        [Test]
        public void TestUpdate()
        {
            GameProgressHandler gameProgressHandler = new GameProgressHandler();
            // GameState newState = new MockGameState();
            // gameProgressHandler.SetGameState(newState);
            // gameProgressHandler.Update();
            // newState.AssertEnterCalled();
            // newState.AssertUpdateCalled();
        }
    }

}