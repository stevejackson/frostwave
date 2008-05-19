/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Graphics.Gui;

namespace F2DUnitTests.Graphics.Gui
{
    /// <summary>
    /// UnitTests for the ScreenItem class.
    /// </summary>
    [TestFixture]
    public class ScreenItemTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Ensure that the isVisible property works. This only tests the boolean itself, 
        /// and does not detect if the ScreenItem is being drawn.
        /// </summary>
        [Test]
        public void TestIsVisibleProperty()
        {
            ScreenImage testImage = new ScreenImage();
            
            //make sure the bool is false by default
            Assert.IsFalse(testImage.isVisible);
        }

        /// <summary>
        /// Ensure that the setVisible method is working. 
        /// This does not detect if the ScreenItem is being drawn.
        /// </summary>
        [Test]
        public void TestSetVisibleMethod()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.setVisible();

            Assert.IsTrue(testImage.isVisible);
        }
    }
}
