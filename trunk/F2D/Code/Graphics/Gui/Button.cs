using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Input;
using F2D.Graphics;
using F2D.Core;

namespace F2D.Graphics.Gui
{
    public class Button : ScreenItem
    {
        bool isClicked;
        public bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        bool inBounds;
        public bool InBounds
        {
            get { return inBounds; }
            set { inBounds = value; }
        }

        private Vector2 size;
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public State CurState
        {
            get { return curState; }
            set { curState = value; }
        }

        public enum State
        {
            Idle,
            Hover,
            Depressed
        };

        ContentManager content;
        List<Texture2D> textures;
        string filename;
        State curState;

        public void Initialize(string filename, Vector2 buttonPosition)
        {
            position = buttonPosition;
            textures = new List<Texture2D>();
            curState = State.Idle;
            this.filename = filename;
            this.Layer = 0.1f;
            this.setVisible();
        }

        public void LoadContent(ContentManager contentManager)
        {
            content = contentManager;
            textures.Add(content.Load<Texture2D>(filename + "_i"));
            textures.Add(content.Load<Texture2D>(filename + "_h"));
            textures.Add(content.Load<Texture2D>(filename + "_d"));
            size = new Vector2(textures[0].Width, textures[0].Height);
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
            if (this.isVisible())
            {
                //if the mouse is within the button's position
                if (Director.Rat.Position.X >= position.X &&
                    Director.Rat.Position.X <= (position.X + size.X) &&
                    Director.Rat.Position.Y >= position.Y &&
                    Director.Rat.Position.Y <= (position.Y + size.Y))
                {
                    inBounds = true;
                    curState = State.Hover;
                    isClicked = false;
                    if (Director.Rat.LState == Rat.State.Released)
                    {
                        curState = State.Depressed;
                        isClicked = true;
                    }
                }
                else
                {
                    isClicked = false;
                    inBounds = false;
                    curState = State.Idle;
                }
            }
        }

        public override void Draw()
        {
            if (curState == State.Idle)
            {
                Director.SceneBatch.Draw(textures[0], position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer); 
            }
            else if (curState == State.Hover)
            {
                Director.SceneBatch.Draw(textures[1], position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
            else if (curState == State.Depressed)
            {
                Director.SceneBatch.Draw(textures[2], position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
        }
    }
}
 