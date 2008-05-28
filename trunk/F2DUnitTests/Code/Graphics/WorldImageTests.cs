/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework;
using F2D.Graphics;
using F2D.Math;
using Microsoft.Xna.Framework.Content;
using System;

namespace F2DUnitTests.Graphics
{
    /// <summary>
    /// UnitTests for the WorldImage class.
    /// </summary>
    [TestFixture]
    public class WorldImageTests : TestingGrounds.Game1
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
            WorldImage testImage = new WorldImage(new Vector2(-300, 200));
            
            Assert.AreEqual(new Vector2(-300, 200), testImage.Position);
            Assert.AreEqual(0.5f, testImage.Layer);
            Assert.AreEqual(0.0f, testImage.Rotation);
        }

        /// <summary>
        /// Ensure LoadContent loads the image file.
        /// </summary>
        [Test]
        public void TestLoadContent()
        {
            WorldImage testImage = new WorldImage();

            testImage.LoadContent(LocalContent, @"Content\\test");
        }

        /// <summary>
        /// Ensure the Size property works with a positive size.
        /// </summary>
        [Test]
        public void TestSizeProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.LoadContent(LocalContent, @"Content\\test");

            Assert.AreEqual(new Vector2Int(300, 300), testImage.Size);
        }

        /// <summary>
        /// Ensure the Scale property works correctly.
        /// </summary>
        [Test]
        public void TestScaleProperty()
        {
            WorldImage testImage = new WorldImage();
            testImage.Scale = new Vector2(0.2f, 2.5f);

            Assert.AreEqual(new Vector2(0.2f, 2.5f), testImage.Scale);
        }

        /// <summary>
        /// Ensure the origin defaults to the middle of an image.
        /// </summary>
        [Test]
        public void TestOriginDefaultPosition()
        {
            WorldImage testImage = new WorldImage();
            testImage.LoadContent(LocalContent, @"Content\\test");

            Assert.AreEqual(new Vector2Int(150, 150), testImage.Origin);
        }

    }
}
