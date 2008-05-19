using NUnit.Framework;

namespace F2DUnitTests.Input
{
    /// <summary>
    /// Unit Tests for the KeyboardState class.
    /// </summary>
    [TestFixture]
    public class InputStateTests
    {
        /// <summary>
        /// Ensures the object gets created.
        /// </summary>
        [Test]
        public void TestInstantiation()
        {
            InputState input = new InputState();
            Assert.IsNotNull(input);
            Assert.IsNotNull(input.CurrentKeyboardState);
            Assert.IsNotNull(input.LastKeyboardState);
        }

    }
}
