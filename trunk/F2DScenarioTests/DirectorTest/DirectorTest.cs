/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D;
using F2D.Math;
using F2D.Input;
using F2D.Core;
using System.Diagnostics;

namespace DirectorTest
{
    /// <summary>
    /// A scenario test for Director.
    /// </summary>
    public class DirectorTestGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager content;
        protected InputState input;

        public DirectorTestGame()
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

            Director.Initialize(this);

            ScreenOne splash = new ScreenOne();
            Director.AddScreen("scr1", splash);

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
            base.Update(gameTime);

            Director.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            Director.Draw();

            base.Draw(gameTime);
        }
    }
}
