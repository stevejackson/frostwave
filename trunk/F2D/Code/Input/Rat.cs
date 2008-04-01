using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using F2D.Management;
using F2D.StateManager;

namespace F2D.Input
{
    public class Rat : F2D.Graphics.Renderable
    {
        private Texture2D cursor;
        private Rectangle drawRect;
        private Vector2 size;

        private ContentManager content;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public enum RatState
        {
            Idle,
            Down,
            Released
        };

        public RatState RState;
        public RatState LState;

        MouseState oldState;

        public void Initialize(Vector2 cursorSize)
        {           
            size = cursorSize;
            drawRect = new Rectangle(0,0, 1600, 1200);
            position = new Vector2(Mouse.GetState().X / ScreenManager.Scale.X, Mouse.GetState().Y / ScreenManager.Scale.Y);
            oldState = Mouse.GetState();
            Layer = 0.0f;
            this.IsVisible = true;
            CurCell = new F2D.Math.Vector2Int(-1, -1);
            F2D.Management.Grid.PermaCell.Objects.Add(this);
        }

        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            cursor = content.Load<Texture2D>(filename);
        }

        public void UnloadContent()
        {
            F2D.Management.Grid.PermaCell.Objects.Remove(this);
            content.Unload();
        }

        public void Update()
        {
            MouseState curState = Mouse.GetState();
            position = new Vector2(curState.X / ScreenManager.Scale.X, curState.Y / ScreenManager.Scale.Y);

            if (position.X > drawRect.Right)
            {
                position.X= drawRect.Right;
            }
            if (position.X < drawRect.Left)
            {
                position.X = drawRect.Left;
            }
            if (position.Y > drawRect.Bottom)
            {
                position.Y = drawRect.Bottom;
            }
            if (position.Y < drawRect.Top)
            {
                position.Y = drawRect.Top;
            }

            if (curState.LeftButton == ButtonState.Pressed ||
                curState.RightButton == ButtonState.Pressed)
            {
                if (curState.LeftButton == ButtonState.Pressed &&
                    oldState.LeftButton == ButtonState.Pressed)
                {
                    LState = RatState.Down;
                }
                if (curState.RightButton == ButtonState.Pressed &&
                    oldState.RightButton == ButtonState.Pressed)
                {
                    RState = RatState.Down;
                }
            }

            else if (curState.LeftButton == ButtonState.Released ||
                     curState.RightButton == ButtonState.Released)
            {
                if (curState.LeftButton == ButtonState.Released &&
                    oldState.LeftButton == ButtonState.Pressed)
                {
                    LState = RatState.Released;
                }
                if (curState.RightButton == ButtonState.Released &&
                    oldState.RightButton == ButtonState.Pressed)
                {
                    RState = RatState.Released;
                }

                if (curState.LeftButton == ButtonState.Released &&
                    oldState.LeftButton == ButtonState.Released)
                {
                    LState = RatState.Idle;
                }

                if (curState.RightButton == ButtonState.Released &&
                    oldState.RightButton == ButtonState.Released)
                {
                    RState = RatState.Idle;
                }      
            }
            
            oldState = curState;

        }

        public override void Draw(Vector2 CamPos)
        {
            if (this.IsVisible)
            {
                ScreenManager.SceneBatch.Draw(cursor, position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
        }
    }
}
