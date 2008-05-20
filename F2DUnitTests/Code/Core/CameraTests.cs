/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Input;
using F2D.Core;
using Microsoft.Xna.Framework;
using F2D.Math;
using F2D;

namespace F2DUnitTests.Core
{
    /// <summary>
    /// Unit Tests for the KeyboardState class.
    /// </summary>
    [TestFixture]
    public class CameraTests : TestingGrounds.Game1
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            Run();
        }

        /// <summary>
        /// Ensure the position property works as expected.
        /// </summary>
        [Test]
        public void TestPositionProperty()
        {
            Camera.Initialize();
            Assert.AreEqual(new Vector2(0, 0), Camera.Position);
            
            Camera.Position = new Vector2(100, 150);
            Assert.AreEqual(new Vector2(100, 150), Camera.Position);
        }

        /// <summary>
        /// Ensure the MapSize property works as expected.
        /// </summary>
        [Test]
        public void TestMapSizeProperty()
        {
            Camera.Initialize();

            Camera.MapSize = new Vector2Int(2000, 40000);
            Assert.AreEqual(new Vector2Int(2000, 40000), Camera.MapSize);
        }

        /// <summary>
        /// Ensure the Camera.Size property works as expected.
        /// </summary>
        [Test]
        public void TestCameraSizeProperty()
        {
            Camera.Initialize();

            Camera.Size = new Vector2Int(640, 480);
            Assert.AreEqual(new Vector2Int(640, 480), Camera.Size);
        }

        /// <summary>
        /// Ensure the Camera.Size property defaults to Frostwave.BaseResolution
        /// if it's been created.
        /// </summary>
        [Test]
        public void TestCameraSizeDefaultsToBaseResolution()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(800, 600);
            Camera.Initialize();
            Assert.AreEqual(Frostwave.BaseResolution, Camera.Size);
        }

        /// <summary>
        /// Ensure the CameraRect contains the proper values.
        /// </summary>
        [Test]
        public void TestCameraRectProperty()
        {
            Camera.Initialize();
            Camera.Size = new Vector2Int(800, 600);
            Camera.Position = new Vector2(50, 75);

            Rectangle expected = new Rectangle(50, 75, 800, 600);

            Assert.AreEqual(expected, Camera.CameraRect);
        }

        /// <summary>
        /// Ensure the camera's position is correct after a simple move delta.
        /// </summary>
        [Test]
        public void TestMoveDeltaSimple()
        {
            Camera.Initialize();
            Camera.Size = new Vector2Int(1600, 1200);
            Camera.Position = new Vector2(100, 100);
            Camera.MapSize = new Vector2Int(5000, 5000);

            Vector2 expected = new Vector2(1000, 50);

            Camera.MoveDelta(new Vector2(900, -50));

            Assert.AreEqual(expected, Camera.Position);
        }

        /// <summary>
        /// Ensure the camera's position is limited by the left and top boundaries.
        /// </summary>
        [Test]
        public void TestMoveDeltaLeftTopBoundaries()
        {
            Camera.Initialize();
            Camera.Size = new Vector2Int(1600, 1200);
            Camera.Position = new Vector2(100, 100);
            Camera.MapSize = new Vector2Int(5000, 5000);

            Vector2 expected = new Vector2(0, 0);

            Camera.MoveDelta(new Vector2(-500, -300));

            Assert.AreEqual(expected, Camera.Position);
        }

        /// <summary>
        /// Ensure the camera's position is limited by the right and bottom boundaries.
        /// </summary>
        [Test]
        public void TestMoveDeltaRightBottomBoundaries()
        {
            Camera.Initialize();
            Camera.Size = new Vector2Int(1600, 1200);
            Camera.Position = new Vector2(0, 0);
            Camera.MapSize = new Vector2Int(5000, 4000);

            Vector2 expected = new Vector2(3400, 2800);

            Camera.MoveDelta(new Vector2(8583, 5024));

            Assert.AreEqual(expected, Camera.Position);
        }

        /// <summary>
        /// Ensure the MoveTo method works.
        /// </summary>
        [Test]
        public void TestMoveToSimple()
        {
            Camera.Initialize();
            Camera.Size = new Vector2Int(1600, 1200);
            Camera.Position = new Vector2(500, 500);
            Camera.MapSize = new Vector2Int(10000, 10000);

            Vector2 expected = new Vector2(1500, 1200);

            Camera.MoveTo(new Vector2(1500, 1200));

            Assert.AreEqual(expected, Camera.Position);
        }
    }
}
