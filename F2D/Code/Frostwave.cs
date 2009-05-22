/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using F2D.Math;
using Microsoft.Xna.Framework.Graphics;
using F2D.Core;

namespace F2D
{
    /// <summary>
    /// Window management: interacts with the GraphicsDeviceManager to automatically
    /// deal with aspect ratios, etc.
    /// </summary>
    static public class Frostwave
    {

        #region Properties        
        
        static private GraphicsDeviceManager graphicsManager;
        static public GraphicsDeviceManager GraphicsManager
        {
            get { return graphicsManager; }
            set { graphicsManager = value; }
        }

        static private bool fullscreen;
        static public bool Fullscreen
        {
            get { return fullscreen; }
            set
            {
                fullscreen = value;
                GraphicsManager.IsFullScreen = fullscreen;
            }
        }

        static private Vector2Int resolution;
        static public Vector2Int Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;

                GraphicsManager.PreferredBackBufferHeight = resolution.Y;
                GraphicsManager.PreferredBackBufferWidth = resolution.X;

                Recalculate();
            }
        }

        static private int columnBoxSize;
        static public int ColumnBoxSize
        {
            get { return columnBoxSize; }
        }

        static private Vector2Int baseResolution;
        
        /// <summary>
        /// The base resolution is what everything will be scaled from upon rendering.
        /// </summary>
        static public Vector2Int BaseResolution
        {
            get { return baseResolution; }
            set 
            { 
                baseResolution = value;
                Recalculate();
            }
        }

        static private Viewport clearViewport;

        /// <summary>
        /// This is the viewport rendered in the back - the black bars.
        /// It's the full size of the screen.
        /// </summary>
        static public Viewport ClearViewport
        {
            get { return clearViewport; }
        }

        static private Viewport sceneViewport;

        /// <summary>
        /// This is the viewport representing the area where the game is rendered.
        /// </summary>
        static public Viewport SceneViewport
        {
            get { return sceneViewport; }
        }


        static private Vector2 scale;

        /// <summary>
        /// Represents the scale for all objects to be multiplied by when drawn.
        /// The ratio is (Resolution / BaseResolution)
        /// </summary>
        static public Vector2 Scale
        {
            get { return scale; }
        }

        /// <summary>
        /// Another representation of the scale between Resolution & BaseResolution,
        /// in a Matrix format, for use by SpriteBatch rendering.
        /// </summary>
        static public Matrix ScaleMatrix
        {
            get
            {
                //return Matrix.CreateScale(
                //(float)Resolution.X / (float)BaseResolution.X,
                //(float)Resolution.Y / (float)BaseResolution.Y,
                //1f);

                    ////Matrix.CreateTranslation(Camera.Position.X, Camera.Position.Y, 0) * 
                    //Matrix.CreateTranslation(new Vector3(-Camera.Position.X, -Camera.Position.Y, 0f)) *
                    //Matrix.CreateTranslation(SceneViewport.Width / 2 / Camera.Zoom, SceneViewport.Height / 2 / Camera.Zoom, 0) *
                    ////Matrix.CreateTranslation(new Vector3(-Camera.Position, 0f)) *
                    //Matrix.CreateScale(Camera.Zoom, Camera.Zoom, Camera.Zoom);
                Vector3 matrixRotOrigin = new Vector3(Camera.Position.X + SceneViewport.Width / 2, Camera.Position.Y + SceneViewport.Height / 2, 0);
                Vector3 matrixScreenPos = new Vector3(SceneViewport.Width / 2, SceneViewport.Height / 2, 0);

                return Matrix.CreateTranslation(-matrixRotOrigin) *
                    Matrix.CreateScale(Camera.Zoom, Camera.Zoom, 1f) *
                    Matrix.CreateRotationZ(0) *
                    Matrix.CreateTranslation(matrixScreenPos);



    //                    Vector3 matrixRotOrigin = new Vector3(Position + Offset, 0);
    //Vector3 matrixScreenPos = new Vector3(ScreenPosition, 0.0f); 
        //                return Matrix.CreateTranslation(-matrixRotOrigin) *
        //Matrix.CreateScale(ActiveCamera.Zoom.X, ActiveCamera.Zoom.Y, 1.0f) *
        //Matrix.CreateRotationZ(ActiveCamera.Rotation) *
        //Matrix.CreateTranslation(matrixScreenPos);
            }
        }

        #endregion

        static public void Initialize(GraphicsDeviceManager graphicsMan)
        {
            graphicsManager = graphicsMan;

            // graphics display properties
            fullscreen = false;
            resolution = new Vector2Int(1024, 768);
            baseResolution = new Vector2Int(1600, 1200);

            // set default values for columnbox + viewports
            sceneViewport = new Viewport();
            clearViewport = new Viewport();

            Recalculate();
        }

        /// <summary>
        /// Updates the GraphicsManager with the current properties.
        /// </summary>
        static public void CreateDisplay()
        {
            try
            {
                GraphicsManager.ApplyChanges();
            }
            catch (System.ObjectDisposedException)
            {
                //This is an issue w/ NUnit or XNA, just catch & ignore it
            }
        }

        /// <summary>
        /// This should be called every frame before anything else.
        /// It draws the viewports according to resolution, aspect ratio, etc.
        /// </summary>
        static public void Draw()
        {
            GraphicsManager.GraphicsDevice.Viewport = ClearViewport;
            GraphicsManager.GraphicsDevice.Clear(Color.Black);
            GraphicsManager.GraphicsDevice.Viewport = SceneViewport;
            GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        /// <summary>
        /// Calculates the proper columnbox size for the current resolution, and applies it
        /// to the viewports.
        /// </summary>
        public static void Recalculate()
        {
            // 1280x1024 is a special case, but should still have no columnboxing
            if (Resolution == new Vector2Int(1280, 1024))
                columnBoxSize = (int)((Resolution.X - (Resolution.Y * 1.25f)) / 2);
            else
                columnBoxSize = (int)((Resolution.X - (Resolution.Y * 1.33333333f)) / 2);

            clearViewport.X = 0;
            clearViewport.Y = 0;
            clearViewport.Width = Resolution.X;
            clearViewport.Height = Resolution.Y;

            sceneViewport.X = ColumnBoxSize;
            sceneViewport.Y = 0;
            sceneViewport.Width = Resolution.X - (ColumnBoxSize * 2);
            sceneViewport.Height = Resolution.Y;

            scale = new Vector2((float)Resolution.X / (float)BaseResolution.X, 
                                (float)Resolution.Y / (float)BaseResolution.Y);
        }
    }

}