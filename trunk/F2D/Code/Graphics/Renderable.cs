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
using F2D.Core;
using F2D.Code.Graphics;
using F2D.Management;
using F2D;
using F2D.Math;

namespace F2D.Code.Graphics
{
    /// <summary>
    /// All visible objects will derive from Renderable.
    /// </summary>
    public abstract class Renderable
    {
        /// <summary>
        /// The current rendering layer of the object.
        /// It's between 0 and 1, with 0 being front and 1 being back.  Default rendering order:
        /// 
        /// <code>
        /// 0.5 = default sprites
        /// 0.9 = interface items
        /// 1.0 = mouse
        /// </code> 
        /// </summary>
        private float layer;
        public float Layer
        {
            get { return layer; }
            set
            {
                if (value <= 1.0f && value >= 0.0f)
                {
                    layer = value;
                }
                else
                {
                    throw new Exception("Invalid renderable layer.");
                }
            }
        }
	
        /// <summary>
        /// All renderables must implement Draw.
        /// </summary>
        public abstract void Draw();
    }
}
