using System;
using System.Diagnostics;
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
using F2D;
using F2D.Input;
using F2D.Graphics;
using F2D.Core;

namespace F2D.Core
{

    public class Director : DrawableGameComponent
    {
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>(); 

        /// <summary>
        /// List that holds all of the items using world coordinates (such as sprites).
        /// </summary>
        public static List<WorldItem> WorldItems = new List<WorldItem>();

        /// <summary>
        /// List that holds all of the items using screen coordinates (such as GUI).
        /// </summary>
        public static List<ScreenItem> ScreenItems = new List<ScreenItem>();

        GraphicsDeviceManager graphicsManager;

        InputState input = new InputState();

        Texture2D blankTexture;

        bool isInitialized;
        bool traceEnabled;

        #region Properties & Fields

        static private bool renderCells;
        static public bool RenderCells
        {
            get { return renderCells; }
            set { renderCells = value; }
        }

        public static ContentManager content;
        static public ContentManager ContentManager
        {
            get { return content; }
        }


        private SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
        }

        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        static private GraphicsDevice graphicsDev;
        static public GraphicsDevice graphicsDevice
        {
            get { return graphicsDev; }
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

        static private Rat rat;
        static public Rat Rat
        {
            get { return rat; }
        }

        static private GameTime gameTime;
        static public GameTime GameTime
        {
            get { return gameTime; }
            set { gameTime = value; }
        }

        #endregion

        public Director(Game game, GraphicsDeviceManager gfx)
            : base(game)
        {
            renderCells = false;

            graphicsManager = gfx;
            graphicsDev = graphicsManager.GraphicsDevice;
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

            rat = new Rat();
            graphicsManager.ApplyChanges();
        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            if (content == null)
                content = new ContentManager(Game.Services);

            font = content.Load<SpriteFont>(@"Content\Graphics\fonts\jbrush");
            blankTexture = content.Load<Texture2D>(@"Content\Graphics\global\blank");

            sceneBatch = new SpriteBatch(graphicsDevice);

            F2D.Core.Grid.LoadContent(content, @"Content\Graphics\bgCell");
            rat.LoadContent(content, @"Content\Graphics\Cursor");
            rat.Initialize(new Vector2(50, 50));

            
            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }

        public override void Update(GameTime gT)
        {
            gameTime = gT;
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

            rat.Update();

            input.Update();

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }

        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Trace.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        public override void Draw(GameTime gameTime)
        {
            F2D.Core.Director.SceneBatch.Begin(SpriteBlendMode.AlphaBlend,
                                   SpriteSortMode.BackToFront,
                                   SaveStateMode.None,
                                   F2D.Core.Director.ScaleMatrix);

            /* we call each screen's draw.  however, these don't do all the
             * drawing - they only do things that are irrelevant to spatial partitioning,
             * which is drawn after this.
             */
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }

            for (int i = 0; i < ScreenItems.Count; i++)
            {
                ScreenItems[i].Draw();
            }

            F2D.Core.Director.SceneBatch.End();

            /* spatial partitioning automatic rendering begins here */

            F2D.Core.Director.SceneBatch.Begin(SpriteBlendMode.AlphaBlend,
                                   SpriteSortMode.BackToFront,
                                   SaveStateMode.None,
                                   F2D.Core.Director.ScaleMatrix);

            Grid.Cells[Grid.ParentCell.X, Grid.ParentCell.Y].Draw();
            Grid.NeighbourCells.Clear();

            for (int layer = 1; layer < Grid.MaxNeighbour + 1; layer++)
            {
                int x = Grid.ParentCell.X, y = Grid.ParentCell.Y;
                //x = center cell location, the coord of the parent cell
                //y = center cell location

                //Start at the top left, draw to top right corner
                for (int distance = -layer; distance < layer; distance++)
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
                if (Grid.NeighbourCells[j].Position.X + Grid.NeighbourCells[j].Size > Camera.Position.X &&
                    Grid.NeighbourCells[j].Position.X < Camera.Position.X + Camera.Size.X &&
                    Grid.NeighbourCells[j].Position.Y + Grid.NeighbourCells[j].Size > Camera.Position.Y &&
                    Grid.NeighbourCells[j].Position.Y < Camera.Position.Y + Camera.Size.Y)
                {
                    Grid.NeighbourCells[j].Draw();
                }
            }

            Rat.Draw();

            F2D.Core.Director.SceneBatch.End();
        }

        public void AddScreen(GameScreen screen)
        {
            screen.Director = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public void FadeBackBufferToBlack(int alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            SceneBatch.Begin();

            SceneBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             new Color(0, 0, 0, (byte)alpha));

            SceneBatch.End();
        }

    }
}
