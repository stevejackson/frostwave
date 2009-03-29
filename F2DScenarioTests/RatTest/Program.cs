using System;

namespace RatTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RatTest game = new RatTest())
            {
                game.Run();
            }
        }
    }
}

