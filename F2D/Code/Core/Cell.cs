/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using F2D;
using F2D.Graphics;

namespace F2D.Core
{
    /// <summary>
    /// A square that contains a list of the objects within it.
    /// </summary>
    public class Cell
    {
        private Texture2D texture;

        private List<WorldItem> worldItems;
        public List<WorldItem> WorldItems
        {
            get { return worldItems; }
            set { worldItems = value; }
        }

        private Rectangle cellRect;

        public Vector2 Position
        {
            get 
            { 
                return new Vector2(cellRect.X, cellRect.Y); 
            }
            set 
            {
                cellRect.X = (int)value.X;
                cellRect.Y = (int)value.Y;
            }
        }

        public int Size
        {
            get 
            {
                //Width and Height should be the same, since the cells are squares.
                return cellRect.Width; 
            }
            set 
            {
                cellRect.Width = cellRect.Height = value;
            }
        }

        public Cell(Vector2 position, int size)
        {
            worldItems = new List<WorldItem>();

            cellRect.X = (int)position.X;
            cellRect.Y = (int)position.Y;

            cellRect.Width = cellRect.Height = size;
        }
         
        public void LoadContent(ContentManager content, string cellFilename)
        {
            content.Load<Texture2D>(cellFilename);
        }

        public void Draw(SpriteBatch batch)
        {
            if (SceneGraph.RenderCells)
            {
                batch.Draw(
                texture, Position - Camera.Position, null,
                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

            }
            for (int i = 0; i < worldItems.Count; i++)
            {                
                worldItems[i].Draw(batch);
            }
        }


    }
}
