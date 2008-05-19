/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Graphics;


namespace F2DUnitTests.Graphics
{
    /// <summary>
    /// UnitTests for the Renderable class.
    /// </summary>
    [TestFixture]
    public class RenderableTests : TestingGrounds.Game1
    {       

        /// <summary>
        /// Ensure that the layer is less than or equal to 1.0.
        /// </summary>
        [Test]
        public void TestLayerUpperBound()
        {
            WorldImage testImage = new WorldImage();
            testImage.Layer = 1.5f;

            Assert.AreEqual(testImage.Layer, 1.0f);
        }

        /// <summary>
        /// Ensure that the layer is greater than or equal to 0.0.
        /// </summary>
        [Test]
        public void TestLayerLowerBound()
        {
            WorldImage testImage = new WorldImage();
            testImage.Layer = -0.5f;

            Assert.AreEqual(testImage.Layer, 0.0f);
        }

        /// <summary>
        /// Ensure that the layer properties works with an appropriate value.
        /// </summary>
        [Test]
        public void TestLayerAppropriateValue()
        {
            WorldImage testImage = new WorldImage();
            testImage.Layer = 0.5f;

            Assert.AreEqual(testImage.Layer, 0.5f);
        }
    }
}
