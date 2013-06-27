using System;
using AXNAEngine.com.axna;

namespace AXNAEngine
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Engine game = new Engine(800, 600))
            {
                game.Run();
            }
        }
    }
#endif
}

