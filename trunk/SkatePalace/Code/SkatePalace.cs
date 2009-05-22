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

namespace SkatePalace
{
    /// <summary>
    /// A scratch project for ugly code testing out F2D functionality.
    /// </summary>
    public class SkatePalaceGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager content;
        protected InputState input;

        public SkatePalaceGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(GraphicsManager);

            Frostwave.Fullscreen = false;
            Frostwave.Resolution = new Vector2Int(640, 480);

            Frostwave.CreateDisplay();

            //Can we change properties a 2nd time?
            Frostwave.Resolution = new Vector2Int(1280, 800);
            Frostwave.Fullscreen = false;

            Frostwave.CreateDisplay();

            input = new InputState();

            Camera.Initialize();
            //Camera.Size = new Vector2Int(800, 600);
            Camera.Position = new Vector2(50, 75);
            Camera.MapSize = new Vector2Int(2000, 2000);
            /*
            Director.Initialize(this);
            Screen s = new Screen();

            Director.AddScreen("red riding hood", s);
            */
            //Assert.AreEqual(s, Director.Screens["Screen"]);
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
            input.Update();

            if (input.IsNewKeyPress(Keys.Escape))
                Exit();

            if (input.IsNewKeyPress(Keys.Right))
                Camera.MoveDelta(new Vector2(0.01f, 0));

            if (input.IsNewKeyPress(Keys.Left))
                Camera.MoveDelta(new Vector2(-0.01f, 0));
            
            if (input.IsNewKeyPress(Keys.Up))
                Camera.MoveDelta(new Vector2(0f, -0.01f));

            if (input.IsNewKeyPress(Keys.Down))
                Camera.MoveDelta(new Vector2(0f, 0.01f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            //here

            base.Draw(gameTime);
        }
    }
}
