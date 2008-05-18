using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content;
using F2D.Math;

namespace F2DUnitTests.Math
{
    /// <summary>
    /// UnitTests for the Vector2Int class.
    /// </summary>
    [TestFixture]
    public class Vector2IntTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {

        }

        [TestFixtureTearDown]
        public void Destroy()
        {

        }

        /// <summary>
        /// Ensures the object gets created.
        /// </summary>
        [Test]
        public void TestInstantiation()
        {
            Vector2Int pos = new Vector2Int();
            Assert.IsNotNull(pos);

            Assert.AreEqual(0, pos.X);
            Assert.AreEqual(0, pos.Y);
        }

        /// <summary>
        /// Ensure the position manipulation works.
        /// </summary>
        [Test]
        public void TestPositionProperty()
        {
            Vector2Int pos = new Vector2Int();

            pos.X = 5;
            pos.Y = -2;

            Assert.AreEqual(5, pos.X);
            Assert.AreEqual(-2, pos.Y);
        }

        /// <summary>
        /// Ensure the constructor taking a position works.
        /// </summary>
        [Test]
        public void TestPositionConstructor()
        {
            Vector2Int pos = new Vector2Int(-80, 75);
            Assert.AreEqual(-80, pos.X);
            Assert.AreEqual(75, pos.Y);
        }

        /// <summary>
        /// Ensure the method to retrieve an instance of Vector2Int with values 1,1 works.
        /// </summary>
        [Test]
        public void TestOneMethod()
        {
            Vector2Int p = Vector2Int.One();

            Assert.AreEqual(1, p.X);
            Assert.AreEqual(1, p.Y);
        }

        /// <summary>
        /// Ensures the equality operator works.
        /// </summary>
        [Test]
        public void TestEqualityOperator()
        {
            Vector2Int a = new Vector2Int(5, 4);
            Vector2Int b = new Vector2Int(5, 4);

            Assert.IsTrue(a == b);
        }

        /// <summary>
        /// Ensure the inequality operator works.
        /// </summary>
        [Test]
        public void TestInequalityOperator()
        {
            Vector2Int a = new Vector2Int(5, 4);
            Vector2Int b = new Vector2Int(5, 5);

            Assert.IsTrue(a != b);
        }

        /// <summary>
        /// Ensure the addition operator works.
        /// </summary>
        [Test]
        public void TestAdditionOperator()
        {
            Vector2Int a = new Vector2Int(6, 7);
            Vector2Int b = new Vector2Int(2, 3);

            Vector2Int expected = new Vector2Int(8, 10);
            Vector2Int actual = a + b;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Ensure the subtraction operator works.
        /// </summary>
        [Test]
        public void TestSubtractionOperator()
        {
            Vector2Int a = new Vector2Int(6, 7);
            Vector2Int b = new Vector2Int(2, 3);

            Vector2Int expected = new Vector2Int(4, 4);

            Vector2Int actual = a - b;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Ensure the object is properly converted to a string.
        /// </summary>
        [Test]
        public void TestToString()
        {
            Vector2Int a = new Vector2Int(50, 24);
            string expected = "(50, 24)";

            Assert.AreEqual(expected, a.ToString());
        }
        
        /// <summary>
        /// Ensure the Equals method does not work upon passing a wrong type.
        /// </summary>
        [Test]
        public void TestEqualsDifferentTypes()
        {
            Vector2Int a = new Vector2Int(5, 10);
            Int16 b = new Int16();

            b = 15;

            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Ensure the Equals method does not crash upon passing a null argument.
        /// </summary>
        [Test]
        public void TestEqualsNullComparison()
        {
            Vector2Int a = new Vector2Int(5, 0);
            Vector2Int b = null;

            Assert.IsFalse(a.Equals(b));
        }

        /// <summary>
        /// Ensure the Equals method works when comparing two equal Vector2Ints.
        /// </summary>
        [Test]
        public void TestEqualsTrueComparison()
        {
            Vector2Int a = new Vector2Int(5, 2);
            Vector2Int b = new Vector2Int(5, 2);

            Assert.IsTrue(a.Equals(b));
        }

    }
}
