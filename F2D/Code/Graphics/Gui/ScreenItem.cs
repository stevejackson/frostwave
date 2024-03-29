/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework.Graphics;
namespace F2D.Graphics.Gui
{
    /// <summary>
    /// Objects drawn to screen coordinates such as mouse, buttons and other GUI.
    /// </summary>
    public abstract class ScreenItem : Renderable
    {
        protected bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
        }

        /// <summary>
        /// Alternates the current state of visibility
        /// </summary>
        public void SetVisible()
        {
            isVisible = !isVisible;
        }

        public abstract override void Draw(SpriteBatch batch);
    }
}
