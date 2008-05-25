/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using System.Collections;
using System.Collections.Generic;

namespace F2D.Core
{
    /// <summary>
    /// Director manages everything related to screens.
    /// </summary>
    static public class Director
    {
        #region Properties

        static private Hashtable screens;
        static public Hashtable Screens
        {
            get { return screens; }
            set { screens = value; }
        }

        static private Microsoft.Xna.Framework.Game xnaGame;
        static public Microsoft.Xna.Framework.Game XnaGame
        {
            get { return xnaGame; }
        }

        #endregion

        static public void Initialize(Microsoft.Xna.Framework.Game game)
        {
            screens = new Hashtable();
            xnaGame = game;
        }

        static public void LoadContent()
        {
            foreach (Screen scr in Screens)
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
        }

        /// <summary>
        /// Adds multiple screens at once.
        /// </summary>
        static public void AddScreen(string[] keys, Screen[] screensToAdd)
        {
            for (int i = 0; i < screensToAdd.Length; i++)
                AddScreen(keys[i], screensToAdd[i]);
        }

        /// <summary>
        /// Safely removes the specified screens.
        /// </summary>
        static public void RemoveScreen(params string[] keys)
        {
            foreach (string key in keys)
                Screens.Remove(key);
        }

        /// <summary>
        /// Calls RemoveScreen on all screens.
        /// </summary>
        static public void ClearScreens()
        {
            IDictionaryEnumerator enumerator = screens.GetEnumerator();

            List<string> keys = new List<string>();

            // build a list of keys to remove
            for (int i = 0; i < screens.Count; i++)
            {
                enumerator.MoveNext();
                keys.Add((string)enumerator.Key);
            }

            for(int i = 0; i < keys.Count; i++) {
                RemoveScreen(keys[i]);
            }
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
    }
}