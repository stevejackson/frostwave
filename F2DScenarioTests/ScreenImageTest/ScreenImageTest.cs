/* Frostwave 2D
 * (c) Snowfall Media 2009
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
using F2D.Graphics.Gui;

namespace ScreenImageTest
{
    /// <summary>
    /// A scenario test for Director.
    /// </summary>
    public class ScreenImageTestGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager content;

        private ScreenImage sImg;
        private ScreenImage sImgInvisible;
        private ScreenImage sImgSmall;
        private ScreenImage sImgRotated;

        private SpriteBatch batch;

        public ScreenImageTestGame()
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

            sImg = new ScreenImage(new Vector2(150, 150));
            sImg.Layer = 0.5f;
            sImgInvisible = new ScreenImage(new Vector2(600, 150));
            sImgInvisible.SetVisible();
            sImgSmall = new ScreenImage(new Vector2(200, 200));
            sImgSmall.Layer = 0.2f;
            sImgRotated = new ScreenImage(new Vector2(150, 950));

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sImg.LoadContent(Content, "smiley");
            sImgInvisible.LoadContent(Content, "smiley");
            
            sImgSmall.LoadContent(Content, "smiley");
            sImgSmall.Scale = new Vector2(0.5f, 0.5f);

            sImgRotated.LoadContent(Content, "smiley");
            sImgRotated.Rotation = (float)Math.PI / 4f;
        }

        protected override void UnloadContent()
        {
            sImg.UnloadContent();
            sImgInvisible.UnloadContent();
            sImgSmall.UnloadContent();
            sImgRotated.UnloadContent();

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

            sImg.Draw(batch);
            sImgInvisible.Draw(batch);
            sImgSmall.Draw(batch);
            sImgRotated.Draw(batch);
            
            batch.End();

            base.Draw(gameTime);
        }
    }
}
