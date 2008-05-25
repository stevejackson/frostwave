using System;

namespace DirectorTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DirectorTestGame game = new DirectorTestGame())
            {
                game.Run();
            }
        }
    }
}

