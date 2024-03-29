/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
namespace F2D.Math
{
    /// <summary>
    /// A simple class for representing integer locations, indices, etc.
    /// </summary>
    public class Vector2Int
    {

        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector2Int()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static public Vector2Int One()
        {
            return new Vector2Int(1, 1);
        }

        public static Vector2Int operator /(Vector2Int a, float b)
        {
            return new Vector2Int((int)((float)a.X / b), (int)(((float)a.Y / b)));
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.X - b.X, a.Y - b.Y);
        }

        public static bool operator ==(Vector2Int a, Vector2Int b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2Int a, Vector2Int b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return "{X:"+ X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Required to implement due to equality operator.
        /// </summary>
        /// <param name="obj">The object to compare this to.</param>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2Int))
                return false;

            if (obj == null)
                return false;

            return this == (Vector2Int)obj;
        }

        /// <summary>
        /// Necessary to bypass compiler warning due to equality operator.
        /// Do not use.
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a Vector2 consisting of this object's coordinates.
        /// </summary>
        public Vector2 ToVector2()
        {
            return new Vector2((float)x, (float)y);
        }
    }
}
