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
    /// F2D.Graphics.Gui.ScreenImage sImg = new F2D.Graphics.Gui.ScreenImage();
    /// sImg.Initialize(new Vector2(500, 200));
    /// sImg.LoadContent(this.content, @"Content\Graphics\Logo"); //loads Logo.png
    /// </code>
    /// </summary>
    public class ScreenImage : F2D.Graphics.Gui.ScreenItem
    {
        /// <summary>
        /// The object location in world coordinates.
        /// </summary>
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }	

        /// <summary>
        /// The rotation of the object.
        /// </summary>
        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Size of the image, (width, height)
        /// </summary>
        private Vector2Int size;

        public Vector2Int Size
        {
            get { return size; }
            set
            {
                if (value.X <= 0)
                    size.X = 1;
                else
                    size.X = value.X;

                if (value.Y <= 0)
                    size.Y = 1;
                else
                    size.Y = value.Y;
            }
        }	

        //Drawing vars
        Texture2D texture;

        #region Main Methods (Initialize, LoadContent, Draw, Update)

        /// <summary>
        /// Initializes the image.
        /// </summary>
        /// <param name="position">Object's starting position.</param>
        public void Initialize(Vector2 position)
        {
            this.Position = position;
            this.Rotation = 0f;
            this.setVisible();
            Layer = 0.3f;
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="contentManager">Pass F2D.Director.content.</param>
        /// <param name="filename">Path to the image file.</param>
        public void LoadContent(ContentManager contentManager, string filename)
        {
            size = new Vector2Int();
            /*content = contentManager;
            texture = content.Load<Texture2D>(filename);*/
        }

        /// <summary>
        /// Removes the image from the Grid & unloads content.
        /// </summary>
        public void UnloadContent()
        {
            /*content.Unload();

            if (this.isVisible())
            {
                Director.ScreenItems.Remove(this);
            }*/
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        public override void Draw()
        {
            if (this.isVisible)
            {
                /*Vector2 posBuffer = Position - Camera.Position;

                Director.SceneBatch.Draw(texture, posBuffer, null,
                    Color.White, Rotation, new Vector2(Size.X / 2, Size.Y / 2), Vector2.One,
                    SpriteEffects.None, Layer);*/
            }
        }

        #endregion
    }
}
