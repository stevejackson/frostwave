/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework;
using F2D;
using F2D.Math;

namespace F2DUnitTests
{
    /// <summary>
    /// Unit tests for the window management class, Frostwave.
    /// </summary>
    [TestFixture]
    public class FrostwaveTests : TestingGrounds.Game1
    {
        private ContentManager LocalContent;

        [TestFixtureSetUp]
        public void Setup()
        {
            Content.RootDirectory = "Content";

            AssemblyUtilities.SetEntryAssembly();
            Uri codeBaseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
            LocalContent = new ContentManager(Services, System.IO.Directory.GetParent(codeBaseUri.AbsolutePath).FullName);
            
            Run();
        }

        [TestFixtureTearDown]
        public void Destroy()
        {

        }

        /// <summary>
        /// Ensure the GraphicsManager is not null.
        /// </summary>
        [Test]
        public void TestGraphicsManager()
        {
            Frostwave.Initialize(GraphicsManager);
            Assert.IsNotNull(Frostwave.GraphicsManager);
        }

        /// <summary>
        /// Ensure the fullscreen property works.
        /// </summary>
        [Test]
        public void TestFullScreenProperty()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Fullscreen = true;

            Assert.AreEqual(true, Frostwave.Fullscreen);
            Assert.AreEqual(true, Frostwave.GraphicsManager.IsFullScreen); 
        }

        /// <summary>
        /// Ensures the Frostwave resolution property works.
        /// </summary>
        [Test]
        public void TestResolutionProperty()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(800, 600);

            Vector2Int expected = new Vector2Int(800, 600);

            Assert.AreEqual(expected, Frostwave.Resolution);
            Assert.AreEqual(expected.X, Frostwave.GraphicsManager.PreferredBackBufferWidth);
            Assert.AreEqual(expected.Y, Frostwave.GraphicsManager.PreferredBackBufferHeight);
        }


        /// <summary>
        /// Ensure the correct columnboxing value is calculated for 1024x768.
        /// </summary>
        [Test]
        public void TestColumnboxingValue_1024x768()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1024, 768);
            Assert.AreEqual(0, Frostwave.ColumnBoxSize);
        }

        /// <summary>
        /// Ensure the correct columnboxing value is calculated for 1280x1024.
        /// </summary>
        [Test]
        public void TestColumnboxingValue_1280x1024()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1280, 1024);
            Assert.AreEqual(0, Frostwave.ColumnBoxSize);
        }

        /// <summary>
        /// Ensure the correct columnboxing value is calculated for 1680x1050.
        /// </summary>
        [Test]
        public void TestColumnboxingValue_1680x1050()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1680, 1050);
            Assert.AreEqual(140f, Frostwave.ColumnBoxSize, 2.0f);
        }

        /// <summary>
        /// Ensure the viewport values are set properly for 1024x768.
        /// </summary>
        [Test]
        public void TestViewportValues_1024x768()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1024, 768);

            Assert.AreEqual(1024, Frostwave.ClearViewport.Width);
            Assert.AreEqual(1024, Frostwave.SceneViewport.Width);
            Assert.AreEqual(768, Frostwave.SceneViewport.Height);
            Assert.AreEqual(0, Frostwave.SceneViewport.X);
        }

        /// <summary>
        /// Ensure the viewport values are set properly for 1280x1024.
        /// </summary>
        [Test]
        public void TestViewportValues_1280x1024()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1280, 1024);

            Assert.AreEqual(1280, Frostwave.ClearViewport.Width);
            Assert.AreEqual(1280, Frostwave.SceneViewport.Width);
            Assert.AreEqual(1024, Frostwave.SceneViewport.Height);
            Assert.AreEqual(0, Frostwave.SceneViewport.X);
        }

        /// <summary>
        /// Ensure the viewport values are set properly for 1680x1050.
        /// </summary>
        [Test]
        public void TestViewportValues_1680x1050()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(1680, 1050);

            Assert.AreEqual(1680, Frostwave.ClearViewport.Width);
            Assert.AreEqual(1402, Frostwave.SceneViewport.Width);
            Assert.AreEqual(1050, Frostwave.SceneViewport.Height);
            Assert.AreEqual(139, Frostwave.SceneViewport.X);
        }

        /// <summary>
        /// Ensure the base resolution property works.
        /// </summary>
        [Test]
        public void TestBaseResolutionProperty()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(1024, 768);

            Assert.AreEqual(new Vector2Int(1024, 768), Frostwave.BaseResolution);
        }

        /// <summary>
        /// Ensure the scale property is calculated correctly.
        /// </summary>
        [Test]
        public void TestScaleValue_1024x768()
        {
            Frostwave.Initialize(GraphicsManager);
            
            Frostwave.Resolution = new Vector2Int(1024, 768);
            Frostwave.BaseResolution = new Vector2Int(1600, 1200);

            Vector2 expected = new Vector2(1024f / 1600f, 768f / 1200f);
            
            Assert.AreEqual(expected, Frostwave.Scale);
        }
        
        /// <summary>
        /// Ensure the scale matrix is calculated correctly.
        /// </summary>
        [Test]
        public void TestScaleMatrixValue_1024x768()
        {
            Frostwave.Initialize(GraphicsManager);

            Frostwave.Resolution = new Vector2Int(1024, 768);
            Frostwave.BaseResolution = new Vector2Int(1600, 1200);

            Matrix expected = Matrix.CreateScale((float)(1024f / 1600f), (float)(768f / 1200f), 1f);

            Assert.AreEqual(expected, Frostwave.ScaleMatrix);
        }
    }
}
