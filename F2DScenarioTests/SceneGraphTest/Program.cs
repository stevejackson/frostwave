using System;

namespace SceneGraphTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SceneGraphTest game = new SceneGraphTest())
            {
                game.Run();
            }
        }
    }
}

