/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework.Content;
namespace F2D.Core
{
    /// <summary>
    /// Represents the possible states of a screen at any given moment.
    /// </summary>
    public enum ScreenState
    {
        Active,
        Hidden,
    }

    /// <summary>
    /// A class to derive screens from to be managed by Director.
    /// </summary>
    public class Screen
    {
        private ContentManager content;
        public ContentManager Content
        {
            get { return content; }
        }

        public Screen()
        {
            try
            {
                content = new ContentManager(Director.XnaGame.Services, "Content");
            }
            catch (System.NullReferenceException)
            {
                throw new System.InvalidOperationException("Failed to instantiate Director.XnaGame " +
                    "before creating a Screen.");
            }
        }

        public void LoadContent()
        {
            
        }
    }
}