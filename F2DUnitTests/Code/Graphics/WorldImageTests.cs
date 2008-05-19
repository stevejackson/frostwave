/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;


namespace F2DUnitTests.Graphics
{
    /// <summary>
    /// UnitTests for the WorldImage class.
    /// </summary>
    [TestFixture]
    public class WorldImageTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Ensure the position manipulation works.
        /// </summary>
        [Test]
        public void TestPositionProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.Position = new Vector2(25, -18);

            Assert.AreEqual(new Vector2(25, -18), testImage.Position);          
        }

        /// <summary>
        /// Ensure the rotation manipulation works.
        /// </summary>
        [Test]
        public void TestRotationProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.Rotation = 0.25f;

            Assert.AreEqual(0.25f, testImage.Rotation);
        }

        /// <summary>
        /// Ensure that the proper values are being assigned in Initialize.
        /// </summary>
        [Test]
        public void TestInitializeMethod()
        {
            WorldImage testImage = new WorldImage();
            testImage.Initialize(new Vector2(-300, 200));
            
            Assert.AreEqual(new Vector2(-300, 200), testImage.Position);
            Assert.AreEqual(0.5f, testImage.Layer);
            Assert.AreEqual(0.0f, testImage.Rotation);
        }

        
        #region Size Tests (Positive, Negative, Zero sizes)

        /// <summary>
        /// Ensure the Size property works with a positive size.
        /// </summary>
        [Test]
        public void TestPositiveSizeProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.LoadContent(content, "Testfile");
            testImage.Size = new Vector2Int(200,300);

            Assert.AreEqual(new Vector2Int(200, 300), testImage.Size);
        }

        /// <summary>
        /// Ensure the Size property fails with size of zero.
        /// </summary>
        [Test]
        public void TestZeroSizeProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.LoadContent(content, "Testfile");
            testImage.Size = new Vector2Int(0, 0);

            Assert.AreEqual(new Vector2Int(1, 1), testImage.Size);
        }

        /// <summary>
        /// Ensure the Size property fails with a negative size.
        /// </summary>
        [Test]
        public void TestNegativeSizeProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.LoadContent(content, "Testfile");
            testImage.Size = new Vector2Int(-200, -300);

            Assert.AreEqual(new Vector2Int(1, 1), testImage.Size);
        }

        #endregion

    }
}
