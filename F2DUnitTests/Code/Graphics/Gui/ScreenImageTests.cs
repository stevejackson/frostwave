/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework;
using F2D.Graphics.Gui;
using F2D.Math;
using Microsoft.Xna.Framework.Content;
using System;


namespace F2DUnitTests.Graphics.Gui
{
    /// <summary>
    /// UnitTests for the ScreenImage class.
    /// </summary>
    [TestFixture]
    public class ScreenImageTests : TestingGrounds.Game1
    {
        private ContentManager LocalContent;

        [TestFixtureSetUp]
        public void Setup()
        {
            AssemblyUtilities.SetEntryAssembly();
            Uri codeBaseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
            LocalContent = new ContentManager(Services, System.IO.Directory.GetParent(codeBaseUri.AbsolutePath).FullName);
            Run();
        }

        /// <summary>
        /// Ensure the position manipulation works.
        /// </summary>
        [Test]
        public void TestPositionProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.Position = new Vector2(-50, 48);

            Assert.AreEqual(new Vector2(-50,48), testImage.Position);
        }

        /// <summary>
        /// Ensure the rotation manipulation works.
        /// </summary>
        [Test]
        public void TestRotationProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.Rotation = 0.25f;

            Assert.AreEqual(0.25f, testImage.Rotation);
        }

        /// <summary>
        /// Ensure that the proper values are being assigned in Initialize.
        /// </summary>
        [Test]
        public void TestInitializeMethod()
        {
            ScreenImage testImage = new ScreenImage(new Vector2(300, -500));

            Assert.AreEqual(new Vector2(300, -500), testImage.Position);
            Assert.AreEqual(0.3f, testImage.Layer);
            Assert.AreEqual(0.0f, testImage.Rotation);
        }


        #region Size Tests (Positive, Negative, Zero sizes)

        /// <summary>
        /// Ensure the Size property works with a positive size.
        /// </summary>
        [Test]
        public void TestPositiveSizeProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.LoadContent(LocalContent, @"Content\test");
            testImage.Size = new Vector2Int(100, 200);

            Assert.AreEqual(new Vector2Int(100, 200), testImage.Size);
        }

        /// <summary>
        /// Ensure the Size property fails with size of zero.
        /// </summary>
        [Test]
        public void TestZeroSizeProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.LoadContent(LocalContent, @"Content\test");
            testImage.Size = new Vector2Int(0, 0);

            Assert.AreEqual(new Vector2Int(1, 1), testImage.Size);
        }

        /// <summary>
        /// Ensure the Size property fails with a negative size.
        /// </summary>
        [Test]
        public void TestNegativeSizeProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.LoadContent(LocalContent, @"Content\test");
            testImage.Size = new Vector2Int(-400, -200);

            Assert.AreEqual(new Vector2Int(1, 1), testImage.Size);
        }

        /// <summary>
        /// Ensure the Scale property works correctly.
        /// </summary>
        [Test]
        public void TestScaleProperty()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.Scale = new Vector2(0.2f, 2.5f);

            Assert.AreEqual(new Vector2(0.2f, 2.5f), testImage.Scale);
        }

        /// <summary>
        /// Ensure the origin defaults to the middle of an image.
        /// </summary>
        [Test]
        public void TestOriginDefaultPosition()
        {
            ScreenImage testImage = new ScreenImage();
            testImage.LoadContent(LocalContent, @"Content\\test");

            Assert.AreEqual(new Vector2Int(150, 150), testImage.Origin);
        }

        #endregion

    }
}
