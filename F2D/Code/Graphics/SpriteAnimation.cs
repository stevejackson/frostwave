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
using System.Collections.Generic;

namespace F2D.Graphics
{
    /// <summary>
    ///  Represents the animation of a sprite.
    /// </summary>
    public class SpriteAnimation
    {
        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Using the given information, provides rectangles representing the
        /// animated images.
        /// </summary>
        public List<Frame> Frames;

        public int CurrentFrameNumber;

        /// <summary>
        /// Returns the currently active frame of animation.
        /// </summary>
        public Frame CurrentFrame
        {
            get { return Frames[CurrentFrameNumber]; }
        }

        public int Speed;

        #endregion

        public SpriteAnimation()
        {
            name = "";
            CurrentFrameNumber = 0;
            Frames = new List<Frame>();
            Speed = -1;
        }

    }
}