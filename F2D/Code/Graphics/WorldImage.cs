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

namespace F2D.Graphics
{
    /// <summary>
    /// Simple images, no physics or collision.  Ideal for non-interactable graphics based on world coordinates.  Layer defaults to 0.5.
    /// 
    /// Example usage:
    /// <code> 
    /// F2D.Graphics.WorldImage wImg = new F2D.Graphics.WorldImage();
    /// wImg.Initialize(new Vector2(1000, 200));
    /// wImg.LoadContent(this.content, @"Content\Graphics\MyImage"); //loads MyImage.png
    /// </code>
    /// </summary>
    public class WorldImage : F2D.Graphics.WorldItem
    {
        /// <summary>
        /// The object location in world coordinates.
        /// </summary>
        /// 
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
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
      
        /// <summary>
        /// Initializes the image.
        /// </summary>
        /// <param name="position">Object's starting position.</param>
        public void Initialize(Vector2 position)
        {
            this.position = position;
            this.Rotation = 0f;
            Layer = 0.5f;
        }

        /// <summary>
        /// Loads the image.
        /// </summary>
        /// <param name="contentManager">Pass F2D.Director.content.</param>
        /// <param name="filename">Path to the image file.</param>
        public void LoadContent(ContentManager contentManager, string filename)
        {
            this.size = new Vector2Int();
            //content = contentManager;

            //texture = content.Load<Texture2D>(filename);
        }

        /// <summary>
        /// Removes the image from the Grid & unloads content.
        /// </summary>
        public void UnloadContent()
        {
            //content.Unload();
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        public override void Draw()
        {
            //Vector2 posBuffer = Position - Camera.Position;

            /*Director.SceneBatch.Draw(texture, posBuffer, null,
                Color.White, Rotation, new Vector2(Size.X / 2, Size.Y / 2), Vector2.One,
                SpriteEffects.None, Layer);*/
        }
    }
}
