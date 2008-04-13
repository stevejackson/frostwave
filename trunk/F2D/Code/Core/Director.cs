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
        #region Properties & Fields
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

        public InputState input = new InputState();
        Texture2D blankTexture;
        bool isInitialized;

        
        static public GameTime GameTime;

        static public bool RenderCells;
        public SpriteFont Font;

        static public Vector2 Resolution;
        static public Matrix ScaleMatrix;
        static public Vector2 Scale;

        static public Viewport SceneViewport;
        static public Viewport ClearViewport;
        static public bool Fullscreen;

        public static ContentManager content;
        static public ContentManager ContentManager
        {
            get { return content; }
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

        // screen switching stuff //

        private GameScreen[] screensToLoad;
        private bool otherScreensFinished;
        private bool hasLoadingScreen;

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
        }


        #endregion

        public Director(Game game, GraphicsDeviceManager gfx)
            : base(game)
        {
            otherScreensFinished = false;
            RenderCells = false;
            isLoading = false;

            graphicsManager = gfx;
            graphicsDev = graphicsManager.GraphicsDevice;
            SceneViewport = new Viewport();
            float letterboxing;

            using (StreamReader reader = new StreamReader("settings.ini"))
            {
                string curLine;

                Fullscreen = Convert.ToBoolean(reader.ReadLine());
                curLine = reader.ReadLine();
                Resolution = new Vector2(Convert.ToSingle(curLine.Substring(0, curLine.IndexOf("x"))),
                    Convert.ToSingle(curLine.Substring(curLine.IndexOf("x") + 1)));
            }

            graphicsManager.IsFullScreen = Fullscreen;
            graphicsManager.PreferredBackBufferWidth = (int)Resolution.X;
            graphicsManager.PreferredBackBufferHeight = (int)Resolution.Y;

            ClearViewport.X = ClearViewport.Y = 0;
            ClearViewport.Width = (int)Resolution.X;
            ClearViewport.Height = (int)Resolution.Y;

            if (Resolution == new Vector2(1280, 1024))
            {
                letterboxing = (Resolution.X - (Resolution.Y * 1.25f)) / 2;
            }
            else
            {
                letterboxing = (Resolution.X - (Resolution.Y * 1.33333f)) / 2;
            }

            SceneViewport.X = (int)letterboxing;
            SceneViewport.Y = 0;

            SceneViewport.Width = (int)Resolution.X - ((int)letterboxing * 2);
            SceneViewport.Height = (int)Resolution.Y;

            ScaleMatrix = Matrix.CreateScale((float)SceneViewport.Width / 1600, (float)SceneViewport.Height / 1200, 1f);
            Scale = new Vector2((float)Resolution.X / 1600, (float)Resolution.Y / 1200);

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

            Font = content.Load<SpriteFont>(@"Content\Graphics\fonts\jbrush");
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
            GameTime = gT;
            tmpFrames++;

            framesPerSecond = 1 / GameTime.ElapsedGameTime.TotalSeconds;

            averageFPS = tmpFrames / GameTime.TotalGameTime.TotalSeconds;

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
                screen.Update(GameTime, otherScreenHasFocus, coveredByOtherScreen);

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

            if (this.IsLoading)
            {
                if (this.otherScreensFinished)
                {
                    foreach (GameScreen screen in screensToLoad)
                        if (screen != null)
                            this.AddScreen(screen);

                    //tell the system not to try & catch up from this extra long frame
                    this.Game.ResetElapsedTime();
                    otherScreensFinished = false; //reset for next time
                    hasLoadingScreen = false;  //reset
                    //this.isLoading = false;
                }
            }
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

            /* spatial partitioning automatic rendering begins here */

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

            //update loading here: make sure the final part of transitions has been shown
            if (this.IsLoading && this.GetScreens().Length == (hasLoadingScreen?1:0))
                otherScreensFinished = true;

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

        public void SwitchScreen(bool hasLoadingScreen,params GameScreen[] screensToLoad)
        {
            this.isLoading = true;
            this.screensToLoad = screensToLoad;
            this.hasLoadingScreen = hasLoadingScreen;

            foreach (GameScreen screen in screens)
            {
                screen.ExitScreen();
            }
        }

        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public void FadeBackBufferToBlack(int alpha)
        {
            Viewport SceneViewport = GraphicsDevice.Viewport;

            SceneBatch.Begin();

            SceneBatch.Draw(blankTexture,
                             new Rectangle(0, 0, SceneViewport.Width, SceneViewport.Height),
                             new Color(0, 0, 0, (byte)alpha));

            SceneBatch.End();
        }

    }
}
