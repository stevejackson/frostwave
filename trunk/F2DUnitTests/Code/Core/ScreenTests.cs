/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Core;

namespace F2DUnitTests.Core
{
    /// <summary>
    /// Unit Tests for the Screen class.
    /// </summary>
    [TestFixture]
    public class ScreenTests : TestingGrounds.Game1
    {

        [TestFixtureSetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Ensure a screen can be created.
        /// </summary>
        [Test]
        public void TestScreenInstantiation()
        {
            Screen s = new Screen();

            Assert.IsNotNull(s);
        }
    }
}
