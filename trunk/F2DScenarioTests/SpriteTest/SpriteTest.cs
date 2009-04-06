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

namespace SpriteTest
{
    /// <summary>
    /// A scenario test for Director.
    /// </summary>
    public class SpriteTestGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager GraphicsManager;

        private Sprite knd1;
        private Sprite Etna;

        private SpriteBatch batch;

        public SpriteTestGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(GraphicsManager);
            Frostwave.BaseResolution = new Vector2Int(800, 600);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.Fullscreen = false;
            Frostwave.CreateDisplay();

            SceneGraph.Initialize(new Vector2Int(800, 600), 800, 1);
            SceneGraph.RenderCells = false;

            Director.Initialize(this);

            knd1 = new Sprite("Content\\knd1.xml");
            knd1.Position = new Vector2(400, 300);
            knd1.AnimationActive = false;
            knd1.CurrentAnimation = "WalkLeft";
            knd1.FramesPerSecond = 4;

            Etna = new Sprite("Content\\Etna.xml");
            Etna.Position = new Vector2(500, 400);
            Etna.AnimationActive = false;
            Etna.CurrentAnimation = "Stand";
            Etna.FramesPerSecond = 6;
            Etna.Scale = new Vector2(2);

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            knd1.LoadContent(Content);
            Etna.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            knd1.UnloadContent();
            Etna.UnloadContent();

            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            if(Director.Input.CurrentKeyboardState.IsKeyDown(Keys.A))
            {
                knd1.CurrentAnimation = "WalkLeft";
                knd1.AnimationActive = true;
                knd1.Position = new Vector2(knd1.Position.X - 1.0f, knd1.Position.Y);

                Etna.CurrentAnimation = "WalkLeft";
                Etna.AnimationActive = true;
                Etna.FlipHorizontal = false;
                Etna.Position = new Vector2(Etna.Position.X - 1.5f, Etna.Position.Y);
            }
            else if (Director.Input.CurrentKeyboardState.IsKeyDown(Keys.S))
            {
                knd1.CurrentAnimation = "WalkDown";
                knd1.AnimationActive = true;
                knd1.Position = new Vector2(knd1.Position.X, knd1.Position.Y + 1.0f);
            } 
            else if (Director.Input.CurrentKeyboardState.IsKeyDown(Keys.W))
            {
                knd1.CurrentAnimation = "WalkUp";
                knd1.AnimationActive = true;
                knd1.Position = new Vector2(knd1.Position.X, knd1.Position.Y - 1.0f);

                Etna.CurrentAnimation = "Backflip";
                Etna.AnimationActive = true;
            }
            else if (Director.Input.CurrentKeyboardState.IsKeyDown(Keys.D))
            {
                knd1.CurrentAnimation = "WalkRight";
                knd1.AnimationActive = true;
                knd1.Position = new Vector2(knd1.Position.X + 1.0f, knd1.Position.Y);

                Etna.CurrentAnimation = "WalkLeft";
                Etna.AnimationActive = true;
                Etna.FlipHorizontal = true;
                Etna.Position = new Vector2(Etna.Position.X + 1.5f, Etna.Position.Y);
            }
            else
            {
                knd1.AnimationActive = false;
                Etna.CurrentAnimation = "Stand";
            }

            Etna.Update(gameTime);
            knd1.Update(gameTime);

            Director.Input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();
            Director.Draw();

            batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, Frostwave.ScaleMatrix);

            Etna.Draw(batch);
            knd1.Draw(batch);

            batch.End();

            base.Draw(gameTime);
        }
    }
}
