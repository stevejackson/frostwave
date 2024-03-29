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
        /// Ensure that the isVisible property works and is true on default.
        /// This only tests the boolean itself, and does not detect if the ScreenItem is being drawn.
        /// </summary>
        [Test]
        public void TestIsVisibleProperty()
        {
            ScreenImage testImage = new ScreenImage();
            
            //make sure the bool is true by default
            Assert.IsTrue(testImage.IsVisible);
        }

        /// <summary>
        /// Ensure that the setVisible method is working. 
        /// This does not detect if the ScreenItem is being drawn.
        /// </summary>
        [Test]
        public void TestSetVisibleMethod()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.SetVisible();

            Assert.IsFalse(testImage.IsVisible);
        }
    }
}
