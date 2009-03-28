/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using F2D.Core;
using F2D.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using F2D.Graphics.Gui;
using Microsoft.Xna.Framework.Input;

namespace DirectorTest
{
    public class ScreenOne : Screen
    {
        private ScreenImage smiley;

        private ContentManager content;
        private SpriteBatch batch;

        public ScreenOne()
        {
            content = new ContentManager(Director.XnaGame.Services, "Content");
            batch = new SpriteBatch(Director.XnaGame.GraphicsDevice);

            smiley = new ScreenImage(new Vector2(200, 300));
            smiley.Layer = 0.9f;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            smiley.LoadContent(content, "smiley");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            content.Unload();
        }

        public override void HandleInput(F2D.Input.InputState inputState)
        {
            base.HandleInput(inputState);

            if (inputState.IsNewKeyPress(Keys.Down))
                smiley.Position = new Vector2(smiley.Position.X, smiley.Position.Y + 15f);
            else if (inputState.IsNewKeyPress(Keys.Up))
                smiley.Position = new Vector2(smiley.Position.X, smiley.Position.Y - 15f);
            else if (inputState.IsNewKeyPress(Keys.Left))
                smiley.Position = new Vector2(smiley.Position.X - 15f, smiley.Position.Y);
            else if (inputState.IsNewKeyPress(Keys.Right))
                smiley.Position = new Vector2(smiley.Position.X + 15f, smiley.Position.Y);


            if (Director.Input.IsNewKeyPress(Keys.Space))
            {
                Director.AddScreen("scr2", new ScreenTwo());
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();

            batch.Begin();
            smiley.Draw(batch);
            batch.End();
        }
    }
}
