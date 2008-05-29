/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using F2D.Math;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace F2D.Graphics
{
    /// <summary>
    /// Objects drawn to world coordinates such as sprites and images.
    /// </summary>
    public abstract class WorldItem : Renderable
    {       
        private Vector2Int curCell;

        /// <summary>
        /// Represents the current cell on the grid (Scene Graph) where this object is at.
        /// </summary>
        public Vector2Int CurCell
        {
            get { return curCell; }
            set { curCell = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public abstract override void Draw(SpriteBatch batch);

    }
}
