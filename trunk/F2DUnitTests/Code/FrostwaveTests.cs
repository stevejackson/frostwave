/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework;
using F2D;

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
        /// Ensure the Frostwave instance is created.
        /// </summary>
        [Test]
        public void TestInstantiation()
        {
            Frostwave fw = new Frostwave(GraphicsManager);
            Assert.IsNotNull(fw);
            fw = null;
        }

        /// <summary>
        /// Ensure the GraphicsManager is not null.
        /// </summary>
        [Test]
        public void TestGraphicsManager()
        {
            Frostwave fw = new Frostwave(GraphicsManager);
            Assert.IsNotNull(fw.GraphicsManager);
            fw = null;
        }


    }
}
