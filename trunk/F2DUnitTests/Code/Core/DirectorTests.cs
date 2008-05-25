/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using NUnit.Framework;
using F2D.Core;

namespace F2DUnitTests.Core
{
    /// <summary>
    /// Unit Tests for the Director class.
    /// </summary>
    [TestFixture]
    public class DirectorTests : TestingGrounds.Game1
    {
        /// <summary>
        /// Ensure the Game property gets set properly.
        /// </summary>
        [Test]
        public void TestXnaGameProperty()
        {
            Director.Initialize(this);
            Assert.AreEqual(this, Director.XnaGame);

            string currentTitle = this.Window.Title;
            this.Window.Title = "New window title";

            Assert.AreEqual(this.Window.Title, Director.XnaGame.Window.Title);

            this.Window.Title = currentTitle;
        }

        /// <summary>
        /// Assert that the Screen HT begins empty.
        /// </summary>
        [Test]
        public void TestHashTableStartsEmpty()
        {
            Director.Initialize(this);
            Assert.AreEqual(0, Director.Screens.Count);
        }

        /// <summary>
        /// Attempts to add a screen to the hashtable.
        /// </summary>
        [Test]
        public void TestHashTableAddScreen()
        {
            Director.Initialize(this);
            Screen scr = new Screen();

            Director.AddScreen("test screen", scr);

            Assert.AreEqual(1, Director.Screens.Count);
            Assert.IsNotNull(Director.Screens["test screen"]);
        }

        /// <summary>
        /// Ensure the AddScreens method successfully adds multiple screens.
        /// </summary>
        [Test]
        public void TestHashTableAddScreenPlural()
        {
            Director.Initialize(this);

            Screen[] scr = new Screen[3];
            scr[0] = new Screen();
            scr[1] = new Screen();
            scr[2] = new Screen();

            string[] keys = new string[3];
            keys[0] = "huckleberry";
            keys[1] = "boo radley";
            keys[2] = "Screen";

            Director.AddScreen(keys, scr);

            Assert.AreEqual(3, Director.Screens.Count);
            Assert.IsNotNull(Director.Screens["huckleberry"]);
            Assert.IsNotNull(Director.Screens["boo radley"]);
            Assert.IsNotNull(Director.Screens["Screen"]);
        }

        /// <summary>
        /// Ensure that screens get removed from the hashtable.
        /// </summary>
        [Test]
        public void TestHashTableRemoveScreen()
        {
            Director.Initialize(this);
            Screen scr = new Screen();

            Director.AddScreen("red tank top", scr);
            Director.RemoveScreen("red tank top");

            Assert.AreEqual(0, Director.Screens.Count);
            Assert.IsNull(Director.Screens["red tank top"]);
        }

        /// <summary>
        /// Ensure that all screens are removed from the hashtable.
        /// </summary>
        [Test]
        public void TestHashTableClearScreens()
        {
            Director.Initialize(this); 
            
            Screen[] scr = new Screen[3];
            scr[0] = new Screen();
            scr[1] = new Screen();
            scr[2] = new Screen();

            string[] keys = new string[3];
            keys[0] = "huckleberry";
            keys[1] = "boo radley";
            keys[2] = "Screen";

            Director.AddScreen(keys, scr);
            Director.ClearScreens();

            Assert.AreEqual(0, Director.Screens.Count);
        }

        /// <summary>
        /// Ensure the plural version of SwitchScreens functions correctly.
        /// </summary>
        [Test]
        public void TestHashtableSwitchScreensPlural()
        {
            Director.Initialize(this);

            Screen[] scr = new Screen[2];
            scr[0] = new Screen();
            scr[1] = new Screen();

            string[] keys = new string[2];
            keys[0] = "huckleberry";
            keys[1] = "boo radley";

            Director.AddScreen(keys, scr);

            // create some new screens to add
            Screen[] newScreens = new Screen[3];
            for (int i = 0; i < newScreens.Length; i++)
                newScreens[i] = new Screen();

            string[] newKeys = new string[3];
            newKeys[0] = "major";
            newKeys[1] = "corporal";
            newKeys[2] = "captain";

            Director.SwitchScreens(newKeys, newScreens);

            Assert.IsNull(Director.Screens["huckleberry"]);
            Assert.IsNull(Director.Screens["boo radley"]);
            Assert.IsNotNull(Director.Screens["major"]);
            Assert.IsNotNull(Director.Screens["corporal"]);
            Assert.IsNotNull(Director.Screens["captain"]);
        }

        [Test]
        public void TestHashtableSwitchScreensSingle()
        {
            Director.Initialize(this);

            Screen scr = new Screen();
            Director.AddScreen("original", scr);

            Screen newScr = new Screen();
            Director.SwitchScreens("new age", newScr);

            Assert.IsNull(Director.Screens["original"]);
            Assert.IsNotNull(Director.Screens["new age"]);
        }
    }
}
