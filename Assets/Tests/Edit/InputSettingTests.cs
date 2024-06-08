using Input;
using NUnit.Framework;

namespace Tests.Edit
{
    public class InputSettingTests
    {
        [Test]
        public void TestInputSettingDefaultConstructor()
        {
            // Assert
            Assert.IsNotNull(InputSetting.InputSystem, "InputSystem should not be null");
        }

        [Test]
        public void TestInputSettingSetInputSystem()
        {
            // Arrange
            IInputSystem customInputSystem = new PCInputSystem(); // Replace with your custom input system

            // Act
            InputSetting.SetInputSystem(customInputSystem);

            // Assert
            Assert.AreSame(InputSetting.InputSystem, customInputSystem, "InputSystem should be the same instance as the one passed to SetInputSystem");
        }
    }
}