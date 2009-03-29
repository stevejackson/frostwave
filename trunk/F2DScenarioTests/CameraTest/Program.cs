using System;

namespace CameraTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CameraTest game = new CameraTest())
            {
                game.Run();
            }
        }
    }
}

