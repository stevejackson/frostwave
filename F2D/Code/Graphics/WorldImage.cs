/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using F2D.Math;
using Microsoft.Xna.Framework.Graphics;
using F2D.Core;

namespace F2D.Graphics
{
    /// <summary>
    /// Simple images, no physics or collision.  Ideal for non-interactable graphics based on world coordinates.  Layer defaults to 0.5.
    /// </summary>
    public class WorldImage : F2D.Graphics.WorldItem
    {
        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
            }
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

        public WorldImage()
        {
            Position = new Vector2();
            rotation = 0f;
            scale = Vector2.One;
            Layer = 0.5f;
            origin = new Vector2Int();
        }

        public WorldImage(Vector2 position) : this()
        {
            this.Position = position;
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
            batch.Draw(
                image, Position - Camera.Position, null, 
                Color.White, rotation, Origin.ToVector2(),
                scale, SpriteEffects.None, Layer);
        }
    }
}
