using System;

namespace SpriteTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpriteTestGame game = new SpriteTestGame())
            {
                game.Run();
            }
        }
    }
}

