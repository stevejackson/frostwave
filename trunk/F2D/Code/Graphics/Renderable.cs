/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

namespace F2D.Graphics
{
    /// <summary>
    /// All visible objects will derive from Renderable.
    /// </summary>
    public abstract class Renderable
    {
        
        private float layer;

        /// <summary>
        /// The current rendering layer of the object.
        /// It's between 0 and 1, with 0 being front and 1 being back.  Default rendering order:
        /// 
        /// <code>
        /// 0.3 = default sprites
        /// 0.5 = interface items
        /// 0.1 = mouse
        /// </code> 
        /// </summary>
        public float Layer
        {
            get { return layer; }
            set
            {
                if (value > 1.0f)            
                    layer = 1.0f;
                
                else if (value < 0.0f)
                    layer = 0.0f;

                else                
                    layer = value;                
            }
        }

        /// <summary>
        /// All renderables must implement Draw.
        /// </summary>
        public abstract void Draw();
    }
}
