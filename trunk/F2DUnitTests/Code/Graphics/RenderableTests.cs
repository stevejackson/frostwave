using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content;
using F2D;
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
