/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using F2D.Math;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using F2D.Graphics.Gui;
using Microsoft.Xna.Framework.Graphics;

namespace F2D.Input
{
    /// <summary>
    /// Handles mouse input and the cursor.
    /// </summary>
    static public class Rat
    {
        static private Vector2 position;
        static public Vector2 Position
        {
            get { return position; }
            set 
            { 
                position = value;
                CheckBoundaries();
            }
        }

        static private Rectangle area;
        static public Rectangle Area
        {
            get { return area; }
            set 
            { 
                area = value;
                area.X = Frostwave.ColumnBoxSize;
                area.Y = 0;

                CheckBoundaries();
            }

        }

        static private ScreenImage image;

        static private MouseState curState;
        static public MouseState CurState
        {
            get { return curState; }
        }
        
        static public void Initialize()
        {
            position = new Vector2();
            image = new ScreenImage();
            image.Layer = 0.1f;

            try
            {
                area = new Rectangle(Frostwave.ColumnBoxSize, 0,
                    Frostwave.Resolution.X - Frostwave.ColumnBoxSize * 2, Frostwave.Resolution.Y);
            }
            catch
            {
                // If Frostwave hasn't been instantiated, the user
                // needs to set Area manually.
                area = new Rectangle();
            } 

            curState = Mouse.GetState();
        }

        static public void LoadContent(ContentManager content, string filename)
        {
            image.LoadContent(content, filename);
            image.Origin = new Vector2Int(0, 0);
        }

        static public void Update()
        {
            curState = Mouse.GetState();

            Position = new Vector2(
                curState.X,
                curState.Y);

            image.Position = Position;
        }

        static public void Draw(SpriteBatch batch)
        {
            image.Draw(batch);
        }

        static private void CheckBoundaries()
        {
            if (position.X < area.Left)
                position.X = area.Left;

            if (position.Y < area.Top)
                position.Y = area.Top;

            if (position.X > area.Right)
                position.X = area.Right;

            if (position.Y > area.Bottom)
                position.Y = area.Bottom;
        }
    }
}