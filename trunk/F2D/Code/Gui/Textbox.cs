using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Management;
using F2D.Input;
using F2D.StateManager;

namespace F2D.Gui
{
    public class Textbox : F2D.Graphics.Renderable
    {
        private Texture2D texture;

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private Vector2 boxPosition;
        public Vector2 BoxPosition
        {
            get { return boxPosition; }
            set { boxPosition = value; }
        }

        private Vector2 textPosition;
        public Vector2 TextPosition
        {
            get { return textPosition; }
            set { textPosition = value; }
        }

        private Vector2 size;
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        private String text;
        public String Text
        {
            get { return text; }
            set 
            { 
                text = value;
                if (text.Length > 20)
                {
                    text = text.Substring(0, 20);
                }
            }
        }

        private int charLimit;
        public int CharLimit
        {
            get { return charLimit; }
            set { charLimit = value; }
        }

        private Color textColor;
        public Color CextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        private SpriteFont font;
        private KeyboardState oldState;
        private double timerCount;

        private ContentManager content;


        public void Initialize(Vector2 boxPos, Vector2 textPos, Color textColor, int charLimit)
        {
            boxPosition = boxPos;
            textPosition = textPos;
            state = "Idle";
            this.textColor = textColor;
            text = "";
            this.charLimit = charLimit;            
            this.Layer = 0.1f;
            CurCell = new F2D.Math.Vector2Int(-1, -1);
            F2D.Management.Grid.PermaCell.Objects.Add(this);
           
        }

        public void LoadContent(ContentManager contentManager, string fontFilename, string textboxFileName)
        {
            content = contentManager;

            texture = content.Load<Texture2D>(textboxFileName);
            font = content.Load<SpriteFont>(fontFilename);

            this.size = new Vector2(texture.Width, texture.Height);

        }

        public void UnloadContent()
        {
            F2D.Management.Grid.PermaCell.Objects.Remove(this);
            content.Unload();
        }

        /// <summary>
        /// Checks for mouse click and typing for the textbox
        /// </summary>
        public void Update()
        {
            //if the mouse is clicked and released within the textbox
            if (ScreenManager.Rat.Position.X >= boxPosition.X &&
               ScreenManager.Rat.Position.X <= (boxPosition.X + size.X) &&
               ScreenManager.Rat.Position.Y >= boxPosition.Y &&
               ScreenManager.Rat.Position.Y <= (boxPosition.Y + size.Y))
            {
                if (ScreenManager.Rat.LState == F2D.Input.Rat.RatState.Released)
                    state = "Typing";

            }
            //outside of the bounds and clicked
            else if (ScreenManager.Rat.Position.X < boxPosition.X &&
               ScreenManager.Rat.Position.X > (boxPosition.X + size.X) &&
               ScreenManager.Rat.Position.Y < boxPosition.Y &&
               ScreenManager.Rat.Position.Y > (boxPosition.Y + size.Y))
            {

                if (ScreenManager.Rat.LState == F2D.Input.Rat.RatState.Released)
                    state = "Idle";


                if (state == "Typing")
                {
                    UpdateInput();
                }
            }
        }

        public void UpdateInput()
        {
            KeyboardState curState = Keyboard.GetState();

            if (text.Length > 0 && curState.IsKeyDown(Keys.Back) && oldState.IsKeyUp(Keys.Back))
            {
               text = text.Substring(0, text.Length - 1);
            }
            else if (curState.IsKeyDown(Keys.LeftShift) && text.Length < charLimit ||
                curState.IsKeyDown(Keys.RightShift) && text.Length < charLimit)
            {
                if (curState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    text += "A";
                if (curState.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
                    text += "B";
                if (curState.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
                    text += "C";
                if (curState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
                    text += "D";
                if (curState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
                    text += "E";
                if (curState.IsKeyDown(Keys.F) && oldState.IsKeyUp(Keys.F))
                    text += "F";
                if (curState.IsKeyDown(Keys.G) && oldState.IsKeyUp(Keys.G))
                    text += "G";
                if (curState.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
                    text += "H";
                if (curState.IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
                    text += "I";
                if (curState.IsKeyDown(Keys.J) && oldState.IsKeyUp(Keys.J))
                    text += "J";
                if (curState.IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K))
                    text += "K";
                if (curState.IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L))
                    text += "L";
                if (curState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
                    text += "M";
                if (curState.IsKeyDown(Keys.N) && oldState.IsKeyUp(Keys.N))
                    text += "N";
                if (curState.IsKeyDown(Keys.O) && oldState.IsKeyUp(Keys.O))
                    text += "O";
                if (curState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
                    text += "P";
                if (curState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
                    text += "Q";
                if (curState.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
                    text += "R";
                if (curState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
                    text += "S";
                if (curState.IsKeyDown(Keys.T) && oldState.IsKeyUp(Keys.T))
                    text += "T";
                if (curState.IsKeyDown(Keys.U) && oldState.IsKeyUp(Keys.U))
                    text += "U";
                if (curState.IsKeyDown(Keys.V) && oldState.IsKeyUp(Keys.V))
                    text += "V";
                if (curState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
                    text += "W";
                if (curState.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
                    text += "X";
                if (curState.IsKeyDown(Keys.Y) && oldState.IsKeyUp(Keys.Y))
                    text += "Y";
                if (curState.IsKeyDown(Keys.Z) && oldState.IsKeyUp(Keys.Z))
                    text += "Z";
            }

            else if(text.Length < charLimit)
            {
                if (curState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    text += "a";
                if (curState.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
                    text += "b";
                if (curState.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
                    text += "c";
                if (curState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
                    text += "d";
                if (curState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
                    text += "e";
                if (curState.IsKeyDown(Keys.F) && oldState.IsKeyUp(Keys.F))
                    text += "f";
                if (curState.IsKeyDown(Keys.G) && oldState.IsKeyUp(Keys.G))
                    text += "g";
                if (curState.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
                    text += "h";
                if (curState.IsKeyDown(Keys.I) && oldState.IsKeyUp(Keys.I))
                    text += "i";
                if (curState.IsKeyDown(Keys.J) && oldState.IsKeyUp(Keys.J))
                    text += "j";
                if (curState.IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K))
                    text += "k";
                if (curState.IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L))
                    text += "l";
                if (curState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
                    text += "m";
                if (curState.IsKeyDown(Keys.N) && oldState.IsKeyUp(Keys.N))
                    text += "n";
                if (curState.IsKeyDown(Keys.O) && oldState.IsKeyUp(Keys.O))
                    text += "o";
                if (curState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
                    text += "p";
                if (curState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
                    text += "q";
                if (curState.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
                    text += "r";
                if (curState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
                    text += "s";
                if (curState.IsKeyDown(Keys.T) && oldState.IsKeyUp(Keys.T))
                    text += "t";
                if (curState.IsKeyDown(Keys.U) && oldState.IsKeyUp(Keys.U))
                    text += "u";
                if (curState.IsKeyDown(Keys.V) && oldState.IsKeyUp(Keys.V))
                    text += "v";
                if (curState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
                    text += "w";
                if (curState.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
                    text += "x";
                if (curState.IsKeyDown(Keys.Y) && oldState.IsKeyUp(Keys.Y))
                    text += "y";
                if (curState.IsKeyDown(Keys.Z) && oldState.IsKeyUp(Keys.Z))
                    text += "z";
            }
                oldState = curState;

        }

        public override void Draw(Vector2 CamPos)
        {
            ScreenManager.SceneBatch.Draw(texture, boxPosition, null, Color.White, 0f, Vector2.Zero,
                1f, SpriteEffects.None, this.Layer);
            ScreenManager.SceneBatch.DrawString(font, text, boxPosition + textPosition, Color.Black);

            GameTime gameTime = F2D.StateManager.ScreenManager.GameTime;
            double elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            timerCount += elapsedTime;

            if (timerCount > 1000 && timerCount < 2000)
            {
                ScreenManager.SceneBatch.DrawString(font, "|", boxPosition + textPosition +
                    new Vector2(font.MeasureString(text).X, 0f), Color.Black);
                
            }
            if (timerCount > 2000)
            {
                timerCount = 0;
            }
        }
    }
}
