/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using F2D.Input;

namespace TestingGrounds
{
    /// <summary>
    /// Base class for unit tests requiring an XNA window.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager content;
        private InputState inputState;

        public Game1()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            inputState = new InputState();
        }

        protected override void Initialize()
        {
            //here
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            inputState.Update();
            if (inputState.IsNewKeyPress(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            //here

            base.Draw(gameTime);
        }
    }
}
