/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework.Input;

namespace F2D.Input
{
    /// <summary>
    /// A class to ease the handling of keyboard input.
    /// </summary>
    /// <example>
    /// InputState input = new InputState();
    /// input.Update();
    /// bool isDown = input.IsNewKeyPress(Keys.Escape);
    /// </example>
    public class InputState
    {

        private KeyboardState currentKeyboardState;
        public KeyboardState CurrentKeyboardState
        {
            get { return currentKeyboardState; }
            set { currentKeyboardState = value; }
        }

        private KeyboardState lastKeyboardState;
        public KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
            set { lastKeyboardState = value; }
        }

        public InputState()
        {
            lastKeyboardState = new KeyboardState();
            currentKeyboardState = new KeyboardState();
        }

        /// <summary>
        /// Called every frame to update the states.
        /// </summary>
        public void Update()
        {
            lastKeyboardState = currentKeyboardState;

            currentKeyboardState = Keyboard.GetState();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) &&
                   LastKeyboardState.IsKeyUp(key);
        }
    }
}
