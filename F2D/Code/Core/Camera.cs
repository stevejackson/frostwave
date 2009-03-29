/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using F2D.Math;

namespace F2D.Core
{
    /// <summary>
    /// The camera class allows for simple camera manipulation in the 2D world.
    /// </summary>
    static public class Camera
    {
        static private Rectangle cameraRect;
        static public Rectangle CameraRect
        {
            get { return cameraRect; }
        }

        static private Vector2 position;
        static public Vector2 Position
        {
            get { return position; }
            set 
            { 
                position = value;

                UpdateRect();
            }
        }

        static private Vector2Int mapSize;
        
        /// <summary>
        /// The map size allows the camera to detect its boundaries automatically.
        /// </summary>
        static public Vector2Int MapSize
        {
            get { return mapSize; }
            set { mapSize = value; }
        }

        static private Vector2Int size;
        static public Vector2Int Size
        {
            get { return size; }
            set 
            { 
                size = value;

                UpdateRect();
            }
        }

        static public void Initialize()
        {
            position = new Vector2();

            size = new Vector2Int(1600, 1200);
            mapSize = new Vector2Int(1600, 1200);

            // attempt to give better default values from other classes.
            try
            {
                if (Frostwave.BaseResolution != null)
                {
                    size = Frostwave.BaseResolution;
                    mapSize = Frostwave.BaseResolution;
                }
            }
            catch
            {
            }

            UpdateRect();
        }

        /// <summary>
        /// Updates the cameraRect with the new information.
        /// </summary>
        static private void UpdateRect()
        {
            cameraRect.X = (int)position.X;
            cameraRect.Y = (int)position.Y;
            cameraRect.Width = size.X;
            cameraRect.Height = size.Y;
        }

        /// <summary>
        /// Move the camera relative to its current position.
        /// </summary>
        static public void MoveDelta(Vector2 delta)
        {
            position += delta;

            if (position.X < 0)
                position.X = 0;

            if (position.X + size.X > mapSize.X)
                position.X = mapSize.X - size.X;
            
            if (position.Y < 0)
                position.Y = 0;

            if (position.Y + size.Y > mapSize.Y)
                position.Y = mapSize.Y - size.Y;

            //manually update the camera rect since position changed
            UpdateRect();
        }

        static public void MoveTo(Vector2 destination)
        {
            MoveDelta(destination - position);
        }
    }
}