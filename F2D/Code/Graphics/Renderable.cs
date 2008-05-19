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
using F2D.Graphics;
using F2D;
using F2D.Math;

namespace F2D.Graphics
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
        /// 0.3 = default sprites
        /// 0.5 = interface items
        /// 0.1 = mouse
        /// </code> 
        /// </summary>
        private float layer;
        public float Layer
        {
            get { return layer; }
            set
            {
                if (value > 1.0f)            
                    layer = 1.0f;
                
                else if (value < 0.0f)
                    layer = 0.0f;

                else                
                    layer = value;                
            }
        }

        /// <summary>
        /// All renderables must implement Draw.
        /// </summary>
        public abstract void Draw();
    }
}
