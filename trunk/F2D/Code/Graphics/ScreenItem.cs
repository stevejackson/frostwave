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
using F2D.Graphics;
using F2D;

namespace F2D.Graphics
{
    /// <summary>
    /// Objects drawn to screen coordinates such as mouse, buttons and other GUI.
    /// </summary>
    public abstract class ScreenItem : Renderable
    {
        /// <summary>
        /// Alternates the current state of visibility of the screen item.
        /// </summary>
        public void setVisible()
        {
            if (Director.ScreenItems.Contains(this))
            {
                Director.ScreenItems.Remove(this);
            }
            else
            {
                Director.ScreenItems.Add(this);
            }
        }

        public bool isVisible()
        {
            return Director.ScreenItems.Contains(this);
        }

        public abstract override void Draw();
    }
}
