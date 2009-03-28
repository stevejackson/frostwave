/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using F2D.Input;
namespace F2D.Core
{

    /// <summary>
    /// A class to derive screens from to be managed by Director.
    /// </summary>
    public class Screen
    {
        public Screen()
        {

        }

        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }

        public virtual void Update (GameTime gameTime) { }

        // HandleInput is called only when the current screen is in focus
        public virtual void HandleInput(InputState inputState) { }

        public virtual void Draw() { }

    }
}