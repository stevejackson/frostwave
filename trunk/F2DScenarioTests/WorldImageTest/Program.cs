using System;

namespace WorldImageTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (WorldImageTestGame game = new WorldImageTestGame())
            {
                game.Run();
            }
        }
    }
}

