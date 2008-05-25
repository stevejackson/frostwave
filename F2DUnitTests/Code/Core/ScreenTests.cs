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

        /// <summary>
        /// Ensure that a ContentManager can be created if Director is instantiated.
        /// </summary>
        [Test]
        public void TestContentManagerInstantion()
        {
            Director.Initialize(this);
            Screen s = new Screen();
            Assert.IsNotNull(s.Content);
        }

        /// <summary>
        /// Ensure the ContentManager throws an exception when instantiated before Director.
        /// </summary>
        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestContentManagerException()
        {
            // nullify the game so content can't load
            Director.Initialize(null);

            Screen s = new Screen();
        }
    }
}
