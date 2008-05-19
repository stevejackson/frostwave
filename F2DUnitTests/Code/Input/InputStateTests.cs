/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Input;

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
