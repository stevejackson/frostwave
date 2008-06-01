/* Frostwave 2D
 * (c) Snowfall Media 2008
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

namespace SceneGraphTest
{
    /// <summary>
    /// A scenario test for SceneGraph
    /// </summary>
    public class SceneGraphTest : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphicsManager;

        private List<WorldImage> wImageNeighbours;
        private WorldImage wImageParent;
        private ScreenImage sImageTest;

        private SpriteBatch batch;

        public SceneGraphTest()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            wImageNeighbours = new List<WorldImage>();
            wImageParent = new WorldImage();
            sImageTest = new ScreenImage();
        }

        protected override void Initialize()
        {
            Frostwave.Initialize(graphicsManager);
            Frostwave.BaseResolution = new Vector2Int(1600, 1200);
            Frostwave.Resolution = new Vector2Int(800, 600);
            Frostwave.Fullscreen = false;
            Frostwave.CreateDisplay();

            Camera.Initialize();

            SceneGraph.Initialize(new Vector2Int(1600,1200), 300, 1);
            SceneGraph.RenderCells = true;

            for (int x = 0; x <= SceneGraph.TotalCells.X; x++)
            {
                for (int y = 0; y <= SceneGraph.TotalCells.Y; y++)
                {
                    wImageNeighbours.Add(new WorldImage(
                        new Vector2(SceneGraph.Cells[x, y].Position.X + 150, 
                        SceneGraph.Cells[x, y].Position.Y + 150)));
                }
            }

            for(int i=0; i< wImageNeighbours.Count; i++)
            {
                SceneGraph.Add(wImageNeighbours[i]);
            }

            SceneGraph.Add(sImageTest);
            SceneGraph.Add(wImageParent);

            sImageTest.Position = new Vector2(1200, 800);
            wImageParent.Position = new Vector2(200, 200);

            wImageParent.CurCell = SceneGraph.GetCell(wImageParent.Position);
            SceneGraph.ParentCell = wImageParent.CurCell;

            batch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SceneGraph.LoadContent(Content, "test");

            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                wImageNeighbours[i].LoadContent(Content, "test2");
            }

            sImageTest.LoadContent(Content, "smiley");
            wImageParent.LoadContent(Content, "smiley");            
        }
       
        protected override void UnloadContent()
        {
            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                wImageNeighbours[i].UnloadContent();
            }

            sImageTest.UnloadContent();
            wImageParent.UnloadContent();

            SceneGraph.UnloadContent();

            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                wImageParent.Position = new Vector2(wImageParent.Position.X + 20, wImageParent.Position.Y);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                wImageParent.Position = new Vector2(wImageParent.Position.X - 20, wImageParent.Position.Y);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                wImageParent.Position = new Vector2(wImageParent.Position.X, wImageParent.Position.Y - 20);

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                wImageParent.Position = new Vector2(wImageParent.Position.X, wImageParent.Position.Y + 20);


            for (int i = 0; i < wImageNeighbours.Count; i++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    wImageNeighbours[i].Position = new Vector2(wImageNeighbours[i].Position.X + 20, wImageNeighbours[i].Position.Y);

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    wImageNeighbours[i].Position = new Vector2(wImageNeighbours[i].Position.X - 20, wImageNeighbours[i].Position.Y);

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    wImageNeighbours[i].Position = new Vector2(wImageNeighbours[i].Position.X, wImageNeighbours[i].Position.Y - 20);

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    wImageNeighbours[i].Position = new Vector2(wImageNeighbours[i].Position.X, wImageNeighbours[i].Position.Y + 20);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                //unload the scene graph
                for(int i=0; i<SceneGraph.MasterlistWorldItems.Count; i++)
                {
                    SceneGraph.MasterlistWorldItems[i].CurCell = null;
                }
                SceneGraph.Masterlist.Clear();
                SceneGraph.MasterlistWorldItems.Clear();
                SceneGraph.ScreenItems.Clear();

                wImageNeighbours.Clear();

                //resize the scene graph
                SceneGraph.Initialize(new Vector2Int(1400, 1100), 300, 1);
                SceneGraph.RenderCells = true;

                for (int x = 0; x <= SceneGraph.TotalCells.X; x++)
                {
                    for (int y = 0; y <= SceneGraph.TotalCells.Y; y++)
                    {
                        wImageNeighbours.Add(new WorldImage(
                            new Vector2(SceneGraph.Cells[x, y].Position.X + 150,
                            SceneGraph.Cells[x, y].Position.Y + 150)));
                    }
                }

                for (int i = 0; i < wImageNeighbours.Count; i++)
                {
                    SceneGraph.Add(wImageNeighbours[i]);
                    wImageNeighbours[i].LoadContent(Content, "test2");
                }

                SceneGraph.Add(sImageTest);
                SceneGraph.Add(wImageParent);

                SceneGraph.LoadContent(Content, "test");

                sImageTest.Position = new Vector2(800, 600);
                wImageParent.Position = new Vector2(200, 200);

                wImageParent.CurCell = SceneGraph.GetCell(wImageParent.Position);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                SceneGraph.RenderCells = !SceneGraph.RenderCells;

            SceneGraph.ParentCell = wImageParent.CurCell;
            SceneGraph.Update();

            Window.Title = wImageNeighbours[0].CurCell.ToString();

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
