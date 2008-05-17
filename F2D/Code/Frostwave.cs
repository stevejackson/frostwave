/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */
using Microsoft.Xna.Framework;

namespace F2D
{
    /// <summary>
    /// Window management.
    /// </summary>
    public class Frostwave
    {
        private GraphicsDeviceManager graphicsManager;
        public GraphicsDeviceManager GraphicsManager
        {
            get { return graphicsManager; }
            set { graphicsManager = value; }
        }

        public Frostwave(GraphicsDeviceManager graphicsManager)
        {
            this.graphicsManager = graphicsManager;
        }

    }

}