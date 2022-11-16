using System;
using Trebuchet;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Trebuchet.Game.Director;
namespace Trebuchet
{
    public class core_basic_window
    {
        
        public static int Main()
        {
            // Initialization
            //————————————————————————————–
            const int screenWidth = 1600;
            const int screenHeight = 900;
            InitWindow(screenWidth, screenHeight, "Trebuchet Terror");
            SetTargetFPS(60);

            //————————————————————————————–
            // Main game loop
            Director director = new Director();
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {


                //———————————————————————————-
                // Draw
                //———————————————————————————-
                director.RunGame();


                
                //———————————————————————————-
            }
            // De-Initialization
            //————————————————————————————–
            CloseWindow();        // Close window and OpenGL context
            //————————————————————————————–
            return 0;
        }
    }
}
