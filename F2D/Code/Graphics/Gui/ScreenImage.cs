/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

        public void Initialize(Vector2 position)
        {
            this.Position = position;
            this.Rotation = 0f;
            this.setVisible();
            Layer = 0.3f;
        }

        public void LoadContent()
        {
            size = new Vector2Int();
        }

        public void UnloadContent()
        {
           
        }

        public override void Draw(SpriteBatch batch)
        {
            
        }

    }
}
