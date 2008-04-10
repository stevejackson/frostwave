using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using FarseerGames;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using F2D.Core;
using F2D;
using F2D.Graphics;
using F2D.Math;

namespace F2D.Graphics.Gui
{
    /// <summary>
    /// Simple images primarily for use in interfaces.  Based on screen coordinates. Layer defaults to 0.1.
    /// 
    /// Example usage:
    /// <code> 
    /// F2D.Graphics.Gui.ScreenImage wImg = new F2D.Graphics.Gui.ScreenImage();
    /// wImg.Initialize(new Vector2(500, 200));
    /// wImg.LoadContent(this.content, @"Content\Graphics\Logo"); //loads Logo.png
    /// </code>
    /// </summary>
    public class ScreenImage : F2D.Graphics.ScreenItem
    {
        /// <summary>
        /// The object location in world coordinates.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The rotation of the object.
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Size of the image, (width, height)
        /// </summary>
        public Vector2Int Size;

        //Drawing vars
        Texture2D texture;

        ContentManager content;

        #region Main Methods (Initialize, LoadContent, Draw, Update)

        /// <summary>
        /// Initializes the image.
        /// </summary>
        /// <param name="position">Object's starting position.</param>
        public void Initialize(Vector2 position)
        {
            this.Position = position;
            this.Rotation = 0f;
            this.Size = new Vector2Int();

            this.setVisible();        

            Layer = 0.1f;
        }

        /// <summary>
        /// Initializes the image.
        /// </summary>
        /// <param name="position">Object's starting position.</param>
        /// <param name="rotation">Object's starting rotation value in radians.</param>
        public void Initialize(Vector2 position, float rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = new Vector2Int();

            Director.ScreenItems.Add(this);         

            Layer = 0.1f;
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="contentManager">Pass F2D.Director.content.</param>
        /// <param name="filename">Path to the image file.</param>
        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            texture = content.Load<Texture2D>(filename);
        }

        /// <summary>
        /// Removes the image from the Grid & unloads content.
        /// </summary>
        public void UnloadContent()
        {
            content.Unload();

            if (this.isVisible())
            {
                Director.ScreenItems.Remove(this);
            }
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        public override void Draw()
        {
            if (this.isVisible())
            {
                Vector2 posBuffer = Position - Camera.Position;

                Director.SceneBatch.Draw(texture, posBuffer, null,
                    Color.White, Rotation, new Vector2(Size.X / 2, Size.Y / 2), Vector2.One,
                    SpriteEffects.None, Layer);
            }
        }

        #endregion
    }
}
