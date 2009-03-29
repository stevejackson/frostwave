/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Input;
using Microsoft.Xna.Framework;
using F2D;
using F2D.Math;
using System;

namespace F2DUnitTests.Input
{
    /// <summary>
    /// Unit Tests for the Rat class.
    /// </summary>
    [TestFixture]
    public class RatTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Ensure the default initialization values are acceptable.
        /// </summary>
        [Test]
        public void TestInitialization()
        {
            Rat.Initialize();
            Assert.IsNotNull(Rat.Position);
            Assert.IsNotNull(Rat.Area);
        }

        /// <summary>
        /// Ensure the position works properly for basic functions.
        /// </summary>
        [Test]
        public void TestPositionBasic()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(800, 600);

            Rat.Initialize();
            Rat.Position = new Vector2(400, 400);
            
            Assert.AreEqual(new Vector2(400, 400), Rat.Position);
        }

        /// <summary>
        /// Assert the position cannot pass the left and upper boundaries.
        /// </summary>
        [Test]
        public void TestPositionLeftUpBoundaries()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(800, 600);
            Frostwave.Resolution = new Vector2Int(800, 600);

            Rat.Initialize();
            Rat.Position = new Vector2(-10, -40);

            Assert.AreEqual(new Vector2(0, 0), Rat.Position);
        }

        /// <summary>
        /// Assert the position cannot pass the left and upper boundaries.
        /// </summary>
        [Test]
        public void TestPositionLeftUpBoundariesWidescreen()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1680, 1050);
            Frostwave.CreateDisplay();

            Rat.Initialize();
            Rat.Position = new Vector2(-10, -40);

            Assert.AreEqual(new Vector2(139, 0), Rat.Position);
        }

        /// <summary>
        /// Assert the position cannot pass the right and bottom boundaries.
        /// </summary>
        [Test]
        public void TestPositionRightBottomBoundaries()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.CreateDisplay();

            Rat.Initialize();
            Rat.Position = new Vector2(1000, 800);

            Assert.AreEqual(new Vector2(800, 600), Rat.Position);
        }

        /// <summary>
        /// Assert the position cannot pass the right and bottom boundaries in a widescreen resolution.
        /// </summary>
        [Test]
        public void TestPositionRightBottomBoundariesWidescreen()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1680, 1050);
            Frostwave.CreateDisplay();

            Rat.Initialize();
            Rat.Position = new Vector2(2000, 2000);

            Assert.AreEqual(new Vector2(1680 - 139, 1050), Rat.Position);
        }


        /// <summary>
        /// Ensure that the Rat's movement area can be set.
        /// </summary>
        [Test]
        public void TestAreaProperty()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(800, 600);

            Rat.Initialize();
            Rat.Area = new Rectangle(
                0, 0, Frostwave.Resolution.X, Frostwave.Resolution.Y);

            Assert.AreEqual(
                new Rectangle(0, 0, Frostwave.Resolution.X, Frostwave.Resolution.Y), 
                Rat.Area);
        }
    }
}
