using System;

namespace ScreenImageTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ScreenImageTestGame game = new ScreenImageTestGame())
            {
                game.Run();
            }
        }
    }
}

