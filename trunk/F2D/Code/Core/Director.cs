/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using F2D.Input;

namespace F2D.Core
{
    /// <summary>
    /// Director manages everything related to screens.
    /// </summary>
    static public class Director
    {
        #region Properties

        static private InputState input;
        static public InputState Input
        {
            get { return input; }
        }

        static private Dictionary<string, Screen> screens;
        static public Dictionary<string, Screen> Screens
        {
            get { return screens; }
        }

        static private Microsoft.Xna.Framework.Game xnaGame;
        static public Microsoft.Xna.Framework.Game XnaGame
        {
            get { return xnaGame; }
        }

        #endregion

        static public void Initialize(Microsoft.Xna.Framework.Game game)
        {
            screens = new Dictionary<string, Screen>();
            xnaGame = game;
            input = new InputState();
        }

        static public void LoadContent()
        {
            foreach (Screen scr in Screens.Values)
                scr.LoadContent();
        }

        /// <summary>
        /// This is the base method for adding screens.  All other add methods use this for
        /// the functionality.
        /// </summary>
        /// <param name="key">A key for you to access this screen by at any time.</param>
        /// <param name="screen">An instance of the screen to add.</param>
        static public void AddScreen(string key, Screen screen)
        {
            screens.Add(key, screen);
            screen.LoadContent();
        }

        /// <summary>
        /// Adds multiple screens at once.
        /// </summary>
        static public void AddScreen(string[] keys, Screen[] screensToAdd)
        {
            for (int i = 0; i < screensToAdd.Length; i++)
            {
                AddScreen(keys[i], screensToAdd[i]);
                screensToAdd[i].LoadContent();
            }
        }

        /// <summary>
        /// Safely removes the specified screens.
        /// </summary>
        static public void RemoveScreen(params string[] keys)
        {
            foreach (string key in keys)
                screens.Remove(key);
        }

        /// <summary>
        /// Calls RemoveScreen on all screens.
        /// </summary>
        static public void ClearScreens()
        {
            screens.Clear();
        }

        /// <summary>
        /// Unloads all current screen and loads the given new one.
        /// </summary>
        static public void SwitchScreens(string key, Screen screenToAdd)
        {
            ClearScreens();

            AddScreen(key, screenToAdd);
        }

        /// <summary>
        /// Unloads all currently loaded screens, and loads the given new ones.
        /// </summary>
        static public void SwitchScreens(string[] keys, Screen[] screensToAdd)
        {
            ClearScreens();

            for (int i = 0; i < screensToAdd.Length; i++)
                AddScreen(keys[i], screensToAdd[i]);
        }

        /// <summary>
        /// Returns whether or not the given screen is the current focus.
        /// </summary>
        static public bool IsScreenFocus(string key)
        {            
            IDictionaryEnumerator enumerator = screens.GetEnumerator();
            List<string> keys = new List<string>();

            for (int i = 0; i < screens.Count; i++)
            {
                enumerator.MoveNext();
                keys.Add((string)enumerator.Key);
            }

            return key == keys[keys.Count - 1]; 
        }

        /// <summary>
        /// Handles input in the focused screen, and updates all loaded screens.
        /// </summary>
        static public void Update(GameTime gameTime)
        {
            input.Update();

            // Grab a list of keys for the screens
            IDictionaryEnumerator enumerator = screens.GetEnumerator();
            List<string> keys = new List<string>();

            for (int i = 0; i < screens.Count; i++)
            {
                enumerator.MoveNext();
                keys.Add((string)enumerator.Key);
            }

            // Handle input for the focus screen
            screens[keys[keys.Count - 1]].HandleInput(input);

            // Update the screens
            enumerator = screens.GetEnumerator();
            for (int i = 0; i < screens.Count; i++)
            {
                enumerator.MoveNext();
                screens[(string)enumerator.Key].Update(gameTime);
            }
        }

        /// <summary>
        /// Renders the screens.
        /// </summary>
        static public void Draw()
        {
            // Grab a list of keys for the screens
            IDictionaryEnumerator enumerator = screens.GetEnumerator();

            // Draw the screens
            for (int i = 0; i < screens.Count; i++)
            {
                enumerator.MoveNext();
                screens[(string)enumerator.Key].Draw();
            }
        }
    }
}