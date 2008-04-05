using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using F2D.Management;
using F2D.StateManager;
using F2D.Code.Graphics;

namespace F2D.Input
{
    public class Rat : ScreenItem
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
            position = new Vector2(Mouse.GetState().X / ScreenManager.Scale.X, Mouse.GetState().Y / ScreenManager.Scale.Y);
            oldState = Mouse.GetState();
            Layer = 0.0f;

            ScreenManager.ScreenItems.Add(this);
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
                ScreenManager.ScreenItems.Remove(this);
            }
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
                ScreenManager.SceneBatch.Draw(cursor, position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
        }
    }
}
