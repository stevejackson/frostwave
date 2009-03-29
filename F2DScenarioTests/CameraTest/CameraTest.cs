/* Frostwave 2D
 * (c) Snowfall Media
 * Steven Jackson, Vedran Budimcic
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using F2D;
using F2D.Core;
using F2D.Graphics;
using F2D.Graphics.Gui;
using F2D.Math;

namespace CameraTest
{
    /// <summary>
    /// A scenario test for SceneGraph
    /// </summary>
    public class CameraTest : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphicsManager;

        private List<WorldImage> wImageNeighbours;

        private SpriteBatch batch;

        public CameraTest()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            wImageNeighbours = new List<WorldImage>();
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(graphicsManager);
            Frostwave.BaseResolution = new Vector2Int(1600, 1200);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.Fullscreen = false;
            Frostwave.CreateDisplay();


            SceneGraph.Initialize(new Vector2Int(3000, 2000), 300, 10);
            SceneGraph.RenderCells = true;

            Camera.Initialize();
            Camera.MapSize = SceneGraph.SceneSize;

            wImageNeighbours.Add(new WorldImage(new Vector2(100, 100)));
            wImageNeighbours.Add(new WorldImage(new Vector2(2800, 100)));
            wImageNeighbours.Add(new WorldImage(new Vector2(2800, 1800)));
            wImageNeighbours.Add(new WorldImage(new Vector2(100, 1800)));
            wImageNeighbours.Add(new WorldImage(new Vector2(1400, 900)));

            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                SceneGraph.Add(wImageNeighbours[i]);
            }

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SceneGraph.LoadContent(Content, "test");

            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                wImageNeighbours[i].LoadContent(Content, "smiley");
            }
        }

        protected override void UnloadContent()
        {
            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                wImageNeighbours[i].UnloadContent();
            }

            SceneGraph.UnloadContent();

            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Camera.MoveDelta(new Vector2(5f, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Camera.MoveDelta(new Vector2(-5f, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Camera.MoveDelta(new Vector2(0, -5f));

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Camera.MoveDelta(new Vector2(0, 5f));

            // Set the centercell to the center of the camera's area.
            SceneGraph.ParentCell = new Vector2Int((int)(Camera.Position.X + (Camera.Size.X / 2)) / SceneGraph.CellSize,
                                                   (int)(Camera.Position.Y + (Camera.Size.Y / 2)) / SceneGraph.CellSize);
            SceneGraph.Update();

            Window.Title = SceneGraph.ParentCell.ToString() + Camera.Position.ToString();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            Frostwave.Draw();

            batch.Begin(
                SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront,
                SaveStateMode.None, Frostwave.ScaleMatrix);


            SceneGraph.Draw(batch);

            batch.End();

            base.Draw(gameTime);
        }
    }
}
