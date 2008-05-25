/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using F2D;
using F2D.Math;
using F2D.Input;
using F2D.Core;
using F2D.Graphics;

namespace WorldImageTest
{
    /// <summary>
    /// A scenario test for Director.
    /// </summary>
    public class WorldImageTestGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager content;

        private WorldImage wimg;

        public WorldImageTestGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.Fullscreen = false;
            Frostwave.CreateDisplay();

            wimg = new WorldImage();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            wimg.Initialize(new Vector2());
            wimg.LoadContent();
        }

        protected override void UnloadContent()
        {
            wimg.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            //here
            wimg.Draw();

            base.Draw(gameTime);
        }
    }
}
