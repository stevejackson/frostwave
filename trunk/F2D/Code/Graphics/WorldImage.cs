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
        public Vector2 Position
        {
            get { return Position; }
            set
            {
                Position = value;

                CurCell = F2D.Core.Grid.GetCell(Position, this);
            }
        }

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

            CurCell = new F2D.Math.Vector2Int();
            Director.WorldItems.Add(this);
            CurCell = F2D.Core.Grid.GetCell(position, this);

            Layer = 0.5f;
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

            CurCell = new F2D.Math.Vector2Int();
            Director.WorldItems.Add(this);
            CurCell = F2D.Core.Grid.GetCell(position, this);

            Layer = 0.5f;
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
            F2D.Core.Grid.Cells[this.CurCell.X, this.CurCell.Y].Objects.Remove(this);
            F2D.Core.Director.WorldItems.Remove(this);
            content.Unload();
        }

        /// <summary>
        /// Draws the image.
        /// </summary>
        public override void Draw()
        {
            Vector2 posBuffer = Position - Camera.Position;

            Director.SceneBatch.Draw(texture, posBuffer, null,
                Color.White, Rotation, new Vector2(Size.X / 2, Size.Y / 2), Vector2.One,
                SpriteEffects.None, Layer);
        }

        #endregion
    }
}
