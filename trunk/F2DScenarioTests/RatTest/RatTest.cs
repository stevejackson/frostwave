using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using F2D.Input;
using F2D;
using F2D.Math;

namespace RatTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RatTest : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        private SpriteBatch batch;

        public RatTest()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(1600, 1200);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.Fullscreen = false;
            Frostwave.CreateDisplay();

            Rat.Initialize();

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Rat.LoadContent(Content, "test");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Rat.Update();
            this.Window.Title = Rat.Position.ToString() + " - " + "(L? " + Rat.CurState.LeftButton + ") (R? " + Rat.CurState.RightButton + ")";
        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            batch.Begin();
            Rat.Draw(batch);
            batch.End();

            base.Draw(gameTime);
        }
    }
}
