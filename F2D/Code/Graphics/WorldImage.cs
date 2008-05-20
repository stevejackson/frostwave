/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using F2D.Math;

namespace F2D.Graphics
{
    /// <summary>
    /// Simple images, no physics or collision.  Ideal for non-interactable graphics based on world coordinates.  Layer defaults to 0.5.
    /// </summary>
    public class WorldImage : F2D.Graphics.WorldItem
    {
       
        private Vector2 position;               
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
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
        
        public void Initialize(Vector2 position)
        {
            this.position = position;
            this.Rotation = 0f;
            Layer = 0.5f;
        }

        public void LoadContent()
        {
            this.size = new Vector2Int();
        }

        public void UnloadContent()
        {

        }

        public override void Draw()
        {
           
        }
    }
}
