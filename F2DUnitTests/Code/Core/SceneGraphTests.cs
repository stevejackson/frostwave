/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using F2D;
using F2D.Core;
using F2D.Math;
using F2D.Graphics;
using F2D.Graphics.Gui;

namespace F2DUnitTests.Core
{
    /// <summary>
    /// Unit Tests for the SceneGraph class.
    /// </summary>
    [TestFixture]
    public class SceneGraphTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Ensure the parent cell property is working
        /// </summary>
        [Test]
        public void TestParentCellProperty()
        {
            SceneGraph.ParentCell = new Vector2Int(5, 3);

            Assert.AreEqual(new Vector2Int(5, 3), SceneGraph.ParentCell);
        }

        /// <summary>
        /// Ensure the render cell boolean property is working.
        /// </summary>
        [Test]
        public void TestRenderCellsProperty()
        {
            SceneGraph.RenderCells = true;

            Assert.IsTrue(SceneGraph.RenderCells);
        }
        
        /// <summary>
        /// Ensure the initialize method is assigning variables correctly.
        /// Also ensure that the total number of cells calculated for the scene graph is correct.
        /// </summary>
        [Test]
        public void TestInitializeMethod()
        {
            SceneGraph.Initialize(new Vector2Int(1280, 800), 300, 3);

            Assert.IsFalse(SceneGraph.RenderCells);
            
            Assert.AreEqual(new Vector2Int(1280, 800), SceneGraph.SceneSize);
            Assert.AreEqual(300, SceneGraph.CellSize);

            Assert.IsNotNull(SceneGraph.ParentCell);
            Assert.IsNotNull(SceneGraph.Masterlist);
            Assert.IsNotNull(SceneGraph.MasterlistWorldItems);
            Assert.IsNotNull(SceneGraph.ScreenItems);
            Assert.IsNotNull(SceneGraph.ToBeUpdated);

            Assert.IsNotNull(SceneGraph.DrawnCells);

            Assert.AreEqual(new Vector2(5, 3), SceneGraph.TotalCells.ToVector2());
        }

        /// <summary>
        /// Ensure that the Add function is adding world items to the appropriate lists
        /// </summary>
        /// <param name="worldItem"></param>
        [Test]
        public void TestAddWorldItemMethod()
        {
            WorldImage testWorldImage = new WorldImage();

            SceneGraph.Initialize(new Vector2Int(1024, 768), 75, 3);
            SceneGraph.Add(testWorldImage);

            Assert.That(SceneGraph.Masterlist.Contains(testWorldImage));
            Assert.That(SceneGraph.MasterlistWorldItems.Contains(testWorldImage));
            Assert.That(SceneGraph.ToBeUpdated.Contains(testWorldImage));

            SceneGraph.Masterlist.Clear();
            SceneGraph.MasterlistWorldItems.Clear();
            SceneGraph.ToBeUpdated.Clear();
        }

        /// <summary>
        /// Ensure that the Add function is adding screen items to the appropriate lists
        /// </summary>
        /// <param name="worldItem"></param>
        [Test]
        public void TestAddScreenItemMethod()
        {
            ScreenImage testScreenImage = new ScreenImage();

            SceneGraph.Initialize(new Vector2Int(1024, 768), 75, 3);
            SceneGraph.Add(testScreenImage);

            Assert.That(SceneGraph.Masterlist.Contains(testScreenImage));
            Assert.That(SceneGraph.ScreenItems.Contains(testScreenImage));

            SceneGraph.Masterlist.Clear();
            SceneGraph.MasterlistWorldItems.Clear();
        }





    }
}
