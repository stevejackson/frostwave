using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using F2D.Core;
using F2D.Graphics;

namespace F2D.Input
{
    public class Rat : ScreenItem
    {
        private Texture2D cursor;
        private Rectangle drawRect;
        private Vector2 size;

        private ContentManager content;

        public Vector2 Position;

        public enum State
        {
            Idle,
            Down,
            Released
        };

        public State RState;
        public State LState;

        MouseState oldState;


        public void Initialize(Vector2 cursorSize)
        {           
            size = cursorSize;
            drawRect = new Rectangle(0,0, 1600, 1200);
            Position = new Vector2(Mouse.GetState().X / Director.Scale.X, Mouse.GetState().Y / Director.Scale.Y);
            oldState = Mouse.GetState();
            Layer = 0.0f;

            Director.ScreenItems.Add(this);
        }

        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            cursor = content.Load<Texture2D>(filename);
        }

        public void UnloadContent()
        {
            content.Unload();

            if (this.isVisible())
            {
                Director.ScreenItems.Remove(this);
            }
        }

        public void Update()
        {
            MouseState curState = Mouse.GetState();
            Position = new Vector2(curState.X / Director.Scale.X, curState.Y / Director.Scale.Y);

            if (Position.X > drawRect.Right)
            {
                Position.X = drawRect.Right;
            }
            if (Position.X < drawRect.Left)
            {
                Position.X = drawRect.Left;
            }
            if (Position.Y > drawRect.Bottom)
            {
                Position.Y = drawRect.Bottom;
            }
            if (Position.Y < drawRect.Top)
            {
                Position.Y = drawRect.Top;
            }

            if (curState.LeftButton == ButtonState.Pressed ||
                curState.RightButton == ButtonState.Pressed)
            {
                if (curState.LeftButton == ButtonState.Pressed &&
                    oldState.LeftButton == ButtonState.Pressed)
                {
                    LState = State.Down;
                }
                if (curState.RightButton == ButtonState.Pressed &&
                    oldState.RightButton == ButtonState.Pressed)
                {
                    RState = State.Down;
                }
            }

            else if (curState.LeftButton == ButtonState.Released ||
                     curState.RightButton == ButtonState.Released)
            {
                if (curState.LeftButton == ButtonState.Released &&
                    oldState.LeftButton == ButtonState.Pressed)
                {
                    LState = State.Released;
                }
                if (curState.RightButton == ButtonState.Released &&
                    oldState.RightButton == ButtonState.Pressed)
                {
                    RState = State.Released;
                }

                if (curState.LeftButton == ButtonState.Released &&
                    oldState.LeftButton == ButtonState.Released)
                {
                    LState = State.Idle;
                }

                if (curState.RightButton == ButtonState.Released &&
                    oldState.RightButton == ButtonState.Released)
                {
                    RState = State.Idle;
                }      
            }
            
            oldState = curState;

        }

        public override void Draw()
        {
            if (this.isVisible())
            {
                Director.SceneBatch.Draw(cursor, Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
        }
    }
}
