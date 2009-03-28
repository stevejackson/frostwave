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
    public class ScreenTwo : Screen
    {
        private ScreenImage test;

        private ContentManager content;
        private SpriteBatch batch;

        public ScreenTwo()
        {
            content = new ContentManager(Director.XnaGame.Services, "Content");
            batch = new SpriteBatch(Director.XnaGame.GraphicsDevice);

            test = new ScreenImage(new Vector2(300, 400));
            test.Layer = 0.4f;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            test.LoadContent(content, "test");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            content.Unload();

            Director.RemoveScreen("scr2");
        }

        public override void HandleInput(F2D.Input.InputState inputState)
        {
            base.HandleInput(inputState);

            if (Director.Input.IsNewKeyPress(Keys.Space))
            {
                this.UnloadContent();
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
            test.Draw(batch);
            batch.End();
        }
    }
}
