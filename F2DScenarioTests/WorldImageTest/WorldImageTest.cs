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

        private WorldImage wImgRegular;
        private WorldImage wImgRotated;
        private WorldImage smiley;
        private WorldImage smileyScaled;
        private WorldImage testOrigin;

        private SpriteBatch batch;

        public WorldImageTestGame()
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

            wImgRegular = new WorldImage(new Vector2(150, 150));

            wImgRotated = new WorldImage(new Vector2(1400, 200));
            wImgRotated.Rotation = (float)Math.PI / 4;

            smiley = new WorldImage(new Vector2(200, 300));
            smiley.Layer = 0.9f;

            smileyScaled = new WorldImage(new Vector2(1400, 1000));
            smileyScaled.Scale = new Vector2(2.0f, 2.0f);

            testOrigin = new WorldImage(new Vector2(0, 900));

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            wImgRegular.LoadContent(Content, @"test");
            wImgRotated.LoadContent(Content, @"test");
            smiley.LoadContent(Content, @"smiley");
            smileyScaled.LoadContent(Content, @"smiley");
            testOrigin.LoadContent(Content, @"test");
            testOrigin.Origin = new Vector2Int(0, 0);
        }

        protected override void UnloadContent()
        {
            wImgRegular.UnloadContent();
            wImgRotated.UnloadContent();
            smiley.UnloadContent();
            smileyScaled.UnloadContent();
            testOrigin.UnloadContent();

            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, Frostwave.ScaleMatrix);

            wImgRegular.Draw(batch);
            wImgRotated.Draw(batch);
            smiley.Draw(batch);
            smileyScaled.Draw(batch);
            testOrigin.Draw(batch);

            batch.End();

            base.Draw(gameTime);
        }
    }
}
