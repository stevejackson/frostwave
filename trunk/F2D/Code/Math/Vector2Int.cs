namespace F2D.Math
{
    /// <summary>
    /// Simple class used for managing integer coordinates, etc.
    /// </summary>
    public class Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int()
        {
            X = 0;
            Y = 0;
        }

        public Vector2Int(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        /// <summary>
        /// Returns an instance of Vector2Int with both X and Y as 0.
        /// </summary>
        static public Vector2Int Zero()
        {
            return new Vector2Int(0, 0);
        }

        /// <summary>
        /// Returns an instance of Vector2Int with both X and Y as 1.
        /// </summary>
        static public Vector2Int One()
        {
            return new Vector2Int(1, 1);
        }
    }
}