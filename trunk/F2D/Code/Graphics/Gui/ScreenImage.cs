/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using F2D.Core;
using F2D.Math;
using Microsoft.Xna.Framework.Graphics;

namespace F2D.Graphics.Gui
{
    /// <summary>
    /// Simple images primarily for use in interfaces.  Based on screen coordinates. Layer defaults to 0.1.
    /// </summary>
    public class ScreenImage : F2D.Graphics.Gui.ScreenItem
    {        
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }	
        
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
                
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

        private Vector2 scale;
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        private Vector2Int origin;
        public Vector2Int Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        private Texture2D image;

        public ScreenImage()
        {
            this.Position = new Vector2();
            this.Rotation = 0f;
            this.setVisible();
            Layer = 0.3f;
            origin = new Vector2Int();
            scale = Vector2.One;
        }

        public ScreenImage(Vector2 position) : this()
        {
            this.Position = position;
            this.Rotation = 0f;
            this.setVisible();
            Layer = 0.3f;
            scale = Vector2.One;
        }

        public void LoadContent(ContentManager content, string filename)
        {
            size = new Vector2Int();
            image = content.Load<Texture2D>(filename);
            size.X = image.Width;
            size.Y = image.Height;
            origin = new Vector2Int(size.X / 2, size.Y / 2);
        }

        public void UnloadContent()
        {
           
        }

        public override void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                batch.Draw(
                    image, Position - Camera.Position, null,
                    Color.White, rotation, Origin.ToVector2(),
                    scale, SpriteEffects.None, Layer);
            }
        }

    }
}
