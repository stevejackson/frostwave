/* Frostwave 2D
 * (c) Snowfall Media 2009
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using F2D.Core;
using F2D.Graphics;
using F2D.Math;
using System;

namespace F2DUnitTests.Graphics
{
    /// <summary>
    /// UnitTests for the WorldImage class.
    /// </summary>
    [TestFixture]
    public class SpriteAnimationTests : TestingGrounds.Game1
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

        [Test]
        public void TestReadXML()
        {
            Sprite spr = new Sprite(@"Content\testAnim.xml");

            Assert.AreEqual(2, spr.Animations.Count);

            Assert.AreEqual("WalkUp", spr.Animations["WalkUp"].Name);
            Assert.AreEqual(2, spr.Animations["WalkUp"].Frames.Count);

            Assert.AreEqual(0, spr.Animations["WalkUp"].Frames[0].Image.X);
            Assert.AreEqual(1, spr.Animations["WalkUp"].Frames[0].Image.Y);
            Assert.AreEqual(2, spr.Animations["WalkUp"].Frames[0].Image.Width);
            Assert.AreEqual(3, spr.Animations["WalkUp"].Frames[0].Image.Height);
            Assert.AreEqual(4, spr.Animations["WalkUp"].Frames[0].Origin.X);
            Assert.AreEqual(5, spr.Animations["WalkUp"].Frames[0].Origin.Y);
            Assert.AreEqual(6, spr.Animations["WalkUp"].Frames[0].Collision.X);
            Assert.AreEqual(7, spr.Animations["WalkUp"].Frames[0].Collision.Y);
            Assert.AreEqual(8, spr.Animations["WalkUp"].Frames[0].Collision.Width);
            Assert.AreEqual(9, spr.Animations["WalkUp"].Frames[0].Collision.Height);

            Assert.AreEqual(10, spr.Animations["WalkUp"].Frames[1].Image.X);
            Assert.AreEqual(11, spr.Animations["WalkUp"].Frames[1].Image.Y);
            Assert.AreEqual(12, spr.Animations["WalkUp"].Frames[1].Image.Width);
            Assert.AreEqual(13, spr.Animations["WalkUp"].Frames[1].Image.Height);
            Assert.AreEqual(14, spr.Animations["WalkUp"].Frames[1].Origin.X);
            Assert.AreEqual(15, spr.Animations["WalkUp"].Frames[1].Origin.Y);
            Assert.AreEqual(16, spr.Animations["WalkUp"].Frames[1].Collision.X);
            Assert.AreEqual(17, spr.Animations["WalkUp"].Frames[1].Collision.Y);
            Assert.AreEqual(18, spr.Animations["WalkUp"].Frames[1].Collision.Width);
            Assert.AreEqual(19, spr.Animations["WalkUp"].Frames[1].Collision.Height);

            // Animation #2
            Assert.AreEqual("WalkDown", spr.Animations["WalkDown"].Name);

            Assert.AreEqual(20, spr.Animations["WalkDown"].Frames[0].Image.X);
            Assert.AreEqual(21, spr.Animations["WalkDown"].Frames[0].Image.Y);
            Assert.AreEqual(22, spr.Animations["WalkDown"].Frames[0].Image.Width);
            Assert.AreEqual(23, spr.Animations["WalkDown"].Frames[0].Image.Height);
            Assert.AreEqual(24, spr.Animations["WalkDown"].Frames[0].Origin.X);
            Assert.AreEqual(25, spr.Animations["WalkDown"].Frames[0].Origin.Y);
            Assert.AreEqual(26, spr.Animations["WalkDown"].Frames[0].Collision.X);
            Assert.AreEqual(27, spr.Animations["WalkDown"].Frames[0].Collision.Y);
            Assert.AreEqual(28, spr.Animations["WalkDown"].Frames[0].Collision.Width);
            Assert.AreEqual(29, spr.Animations["WalkDown"].Frames[0].Collision.Height);

            Assert.AreEqual(30, spr.Animations["WalkDown"].Frames[1].Image.X);
            Assert.AreEqual(31, spr.Animations["WalkDown"].Frames[1].Image.Y);
            Assert.AreEqual(32, spr.Animations["WalkDown"].Frames[1].Image.Width);
            Assert.AreEqual(33, spr.Animations["WalkDown"].Frames[1].Image.Height);
            Assert.AreEqual(34, spr.Animations["WalkDown"].Frames[1].Origin.X);
            Assert.AreEqual(35, spr.Animations["WalkDown"].Frames[1].Origin.Y);
            Assert.AreEqual(36, spr.Animations["WalkDown"].Frames[1].Collision.X);
            Assert.AreEqual(37, spr.Animations["WalkDown"].Frames[1].Collision.Y);
            Assert.AreEqual(38, spr.Animations["WalkDown"].Frames[1].Collision.Width);
            Assert.AreEqual(39, spr.Animations["WalkDown"].Frames[1].Collision.Height);
        }
    }
}
