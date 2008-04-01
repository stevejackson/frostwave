using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Graphics;
using F2D;
using F2D.Input;
/*
namespace F2D.Management
{
    static public class Scene
    {
        static public List<Renderable> Objects = new List<Renderable>();
        static private GraphicsDeviceManager graphicsManager;

        static private GraphicsDevice graphicsDevice;
        static public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }

        static private ContentManager contentManager;
        static public ContentManager ContentManager
        {
            get { return contentManager; }
        }

        static private SpriteBatch sceneBatch;
        static public SpriteBatch SceneBatch
        {
            get { return sceneBatch; }
        }

        static private Vector2 resolution;
        static public Vector2 Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        static private Matrix scaleMatrix;
        static public Matrix ScaleMatrix
        {
            get { return scaleMatrix; }
            set { scaleMatrix = value; }
        }

        static private Matrix cameraMatrix;
        static public Matrix CameraMatrix
        {
            get { return cameraMatrix; }
            set { cameraMatrix = value; }
        }

        static private Vector2 scale;
        static public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        static private Viewport viewport;
        static public Viewport SceneViewport
        {
            get { return viewport; }
            set { viewport = value; }
        }

        static private Viewport clearViewport;
        static public Viewport ClearViewport
        {
            get { return clearViewport; }
            set { clearViewport = value; }
        }

        static private bool fullscreen;
        static public bool Fullscreen
        {
            get { return fullscreen; }
            set { fullscreen = value; }
        }

        static private double framesPerSecond;
        static private double lowestFPS, highestFPS, averageFPS;
        static private int tmpFrames;
        static public double FramesPerSecond
        {
            get { return framesPerSecond; }
        }


        static public void Initialize(Game game, GraphicsDeviceManager graphicsMan, ContentManager content)
        {
            graphicsManager = graphicsMan;
            graphicsDevice = graphicsManager.GraphicsDevice;
            contentManager = content;

            viewport = new Viewport();
            float letterboxing;

            using (StreamReader reader = new StreamReader("settings.ini"))
            {
                string curLine;

                fullscreen = Convert.ToBoolean(reader.ReadLine());
                curLine = reader.ReadLine();
                resolution = new Vector2(Convert.ToSingle(curLine.Substring(0, curLine.IndexOf("x"))),
                    Convert.ToSingle(curLine.Substring(curLine.IndexOf("x") + 1)));
            }

            graphicsManager.IsFullScreen = fullscreen;
            graphicsManager.PreferredBackBufferWidth = (int)resolution.X;
            graphicsManager.PreferredBackBufferHeight = (int)Resolution.Y;

            clearViewport.X = clearViewport.Y = 0;
            clearViewport.Width = (int)resolution.X;
            clearViewport.Height = (int)resolution.Y;

            if (resolution == new Vector2(1280, 1024))
            {
               letterboxing = (resolution.X - (resolution.Y * 1.25f)) / 2;
            }
            else
            {
               letterboxing = (resolution.X - (resolution.Y * 1.33333f)) / 2;
            }

            viewport.X = (int)letterboxing;
            viewport.Y = 0;

            viewport.Width = (int)resolution.X - ((int)letterboxing * 2);
            viewport.Height = (int)resolution.Y;

            scaleMatrix = Matrix.CreateScale((float)viewport.Width / 1600, (float)viewport.Height / 1200, 1f);
            scale = new Vector2((float)viewport.Width / 1600, (float)viewport.Height / 1200);

            graphicsManager.ApplyChanges();       
        }

        static public void LoadContent()
        {
            sceneBatch = new SpriteBatch(graphicsDevice);
            Grid.LoadContent();
            Rat.LoadContent();
        }

        static public void Update(GameTime gameTime)
        {
            tmpFrames++;

            framesPerSecond = 1 / gameTime.ElapsedGameTime.TotalSeconds;

            averageFPS = tmpFrames / gameTime.TotalGameTime.TotalSeconds;

            // highest fps               

            if (framesPerSecond > highestFPS)
                highestFPS = framesPerSecond;

            // lowest fps               

            if (framesPerSecond < lowestFPS)
                lowestFPS = framesPerSecond;

            if (framesPerSecond > highestFPS && !double.IsInfinity(framesPerSecond))
                highestFPS = framesPerSecond;

            //F2D.Core.Farseer.Physics.Update(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
        }


        static public void Draw()
        {
            Grid.Cells[(int)Grid.ParentCell.X, (int)Grid.ParentCell.Y].Draw();
            Grid.NeighbourCells.Clear();     
                
            for (int layer = 1; layer < Grid.MaxNeighbour + 1; layer++)
            {
                int x = (int)Grid.ParentCell.X, y = (int)Grid.ParentCell.Y;
                //x = center cell location, the coord of the parent cell
                //y = center cell location

                //Start at the top left, draw to top right corner
                for(int distance = - layer; distance < layer; distance++) 
                {
                    //Top bound / right bound / left bound
                    if (y - layer >= 0 && x + distance <= Grid.TotalXCells && x + distance >= 0)
                    {
                        //Grid.Cells[(int)x + distance, (int)y - layer].Draw();
                        Grid.NeighbourCells.Add(Grid.Cells[(int)x + distance, (int)y - layer]);
                    }
                }

                //Start at top right, go to bottom right
                for (int distance = -layer; distance < layer; distance++)
                {
                    //Right bound / bottom bound / top bound
                    if (x + layer <= Grid.TotalXCells && y + distance <= Grid.TotalYCells && y + distance >= 0)
                    {
                        //Grid.Cells[(int)x + layer, (int)y + distance].Draw();
                        Grid.NeighbourCells.Add(Grid.Cells[(int)x + layer, (int)y + distance]);
                    }
                }

                //Start at bottom right, go to bottom left
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Right bound / bottom bound / left bound
                    if (x + distance <= Grid.TotalXCells && y + layer <= Grid.TotalYCells && x + distance >= 0)
                    {
                        //Grid.Cells[(int)x + distance, (int)y + layer].Draw();
                        Grid.NeighbourCells.Add(Grid.Cells[(int)x + distance, (int)y + layer]);
                    }
                }

                //Start at bottom left, go to top left
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Left bound, top bound, bottom bound
                    if (x - layer >= 0 && y + distance >= 0 && y + distance <= Grid.TotalYCells)
                    {
                        //Grid.Cells[(int)x - layer, (int)y + distance].Draw();
                        Grid.NeighbourCells.Add(Grid.Cells[(int)x - layer, (int)y + distance]);
                    }
                }
            }

            //check if the neighbouring cells are within the bounds of the camera
            //and if so, draw them
            for (int j = 0; j < Grid.NeighbourCells.Count; j++)
            {
                if (Grid.NeighbourCells[j].Position.X + Grid.NeighbourCells[j].Size.X > Camera.Position.X &&
                    Grid.NeighbourCells[j].Position.X < Camera.Position.X + Camera.Size.X &&
                    Grid.NeighbourCells[j].Position.Y + Grid.NeighbourCells[j].Size.Y> Camera.Position.Y &&
                    Grid.NeighbourCells[j].Position.Y < Camera.Position.Y + Camera.Size.Y)
                {
                    Grid.NeighbourCells[j].Draw();
                }
            }                   

        }


    }
}

*/