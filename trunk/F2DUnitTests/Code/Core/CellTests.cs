/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using Microsoft.Xna.Framework;
using F2D.Graphics;
using F2D.Core;
using F2D;

namespace F2DUnitTests.Core
{
    /// <summary>
    /// Unit Tests for the Cell class.
    /// </summary>
    [TestFixture]
    public class CellTests : TestingGrounds.Game1
    {
       
        /// <summary>
        /// Ensure the WorldItems property is working.
        /// </summary>
        [Test]
        public void TestWorldItemsProperty()
        {
            WorldImage testImage = new WorldImage();
            Cell testCell = new Cell(new Vector2(300, 200), 50);

            testCell.WorldItems.Add(testImage);

            Assert.That(testCell.WorldItems.Contains(testImage));
        }

        /// <summary>
        /// Ensure the Position property is working.
        /// </summary>
        [Test]
        public void TestPositionProperty()
        {
            Cell testCell = new Cell(new Vector2(-300, 200), 50);

            testCell.Position = new Vector2(20, 30);

            Assert.AreEqual(new Vector2(20, 30), testCell.Position);
        }

        /// <summary>
        /// Ensure the Size property is working.
        /// </summary>
        [Test]
        public void TestSizeProperty()
        {
            Cell testCell = new Cell(new Vector2(-300, 200), 50);

            testCell.Size = 90;

            Assert.AreEqual(90, testCell.Size);
        }
        
        /// <summary>
        /// Ensure the constructor is assigning proper values to variables.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            Cell testCell = new Cell(new Vector2(200, 300), 75);

            Assert.IsNotNull(testCell.WorldItems);
            Assert.AreEqual(new Vector2(200, 300), testCell.Position);
            Assert.AreEqual(75, testCell.Size);
        }

        
    }
}
