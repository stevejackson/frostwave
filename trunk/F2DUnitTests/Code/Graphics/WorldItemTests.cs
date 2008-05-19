using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using F2D;
using F2D.Math;
using F2D.Graphics;


namespace F2DUnitTests.Graphics
{
    /// <summary>
    /// UnitTests for the WorldImage class.
    /// </summary>
    [TestFixture]
    public class WorldItemTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Make sure CurCell starts out null, so that it isn't automatically added to scene graph.
        /// </summary>
        [Test]
        public void TestCurCellIsNull()
        {
            WorldImage testImage = new WorldImage();
            testImage.Initialize(new Vector2(200, 300));

            Assert.IsNull(testImage.CurCell);
        }

    }
}
