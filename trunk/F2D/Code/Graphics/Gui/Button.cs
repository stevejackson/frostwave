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
        public bool IsClicked;
        public Vector2 Position;
        public bool InBounds;

        public Vector2 Size;
        public State CurState;

        public enum State
        {
            Idle,
            Hover,
            Depressed
        };

        ContentManager content;
        List<Texture2D> textures;
        string filename;

        public void Initialize(string filename, Vector2 buttonPosition)
        {
            Position = buttonPosition;
            textures = new List<Texture2D>();
            CurState = State.Idle;
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
            Size = new Vector2(textures[0].Width, textures[0].Height);
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
                //if the mouse is within the button's Position
                if (Director.Rat.Position.X >= Position.X &&
                    Director.Rat.Position.X <= (Position.X + Size.X) &&
                    Director.Rat.Position.Y >= Position.Y &&
                    Director.Rat.Position.Y <= (Position.Y + Size.Y))
                {
                    InBounds = true;
                    CurState = State.Hover;
                    IsClicked = false;
                    if (Director.Rat.LState == Rat.State.Released)
                    {
                        CurState = State.Depressed;
                        IsClicked = true;
                    }
                }
                else
                {
                    IsClicked = false;
                    InBounds = false;
                    CurState = State.Idle;
                }
            }
        }

        public override void Draw()
        {
            if (CurState == State.Idle)
            {
                Director.SceneBatch.Draw(textures[0], Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer); 
            }
            else if (CurState == State.Hover)
            {
                Director.SceneBatch.Draw(textures[1], Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
            else if (CurState == State.Depressed)
            {
                Director.SceneBatch.Draw(textures[2], Position, null, Color.White, 0f, Vector2.Zero, 1f,
                    SpriteEffects.None, this.Layer);
            }
        }
    }
}
 