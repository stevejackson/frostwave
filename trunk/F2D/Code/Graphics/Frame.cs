/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using F2D.Math;
using F2D.Core;
using System.Xml;

namespace F2D.Graphics
{
    /// <summary>
    ///  Represents the animation of a sprite.
    /// </summary>
    public class Frame
    {
        public Rectangle Image;
        public Rectangle Collision;
        public Vector2Int Origin;
    }
}