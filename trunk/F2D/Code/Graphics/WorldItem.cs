using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using FarseerGames;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using F2D;
using F2D.Core;
using F2D.Graphics;
using F2D.Math;

namespace F2D.Graphics
{
    /// <summary>
    /// Objects drawn to world coordinates such as sprites and images.
    /// </summary>
    public abstract class WorldItem : Renderable
    {
        /// <summary>
        /// Represents the current cell on the grid where this object is at.
        /// </summary>
        private Vector2Int curCell;
        public Vector2Int CurCell
        {
            get { return curCell; }
            set { curCell = value; }
        }

        protected Geom physicsGeometry;
        public Geom PhysicsGeometry
        {
            get { return physicsGeometry; }
            set { physicsGeometry = value; }
        }

        public abstract override void Draw();

    }
}
