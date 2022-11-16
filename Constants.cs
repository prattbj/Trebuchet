using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Trebuchet
{
    public class Constants
    {
        public static double GRAVITY = 9.81;
        public static double PROJECTILE_MASS = 1.5;
        public static double COUNTERWEIGHT_HEIGHT = 10.1;
        public static double PROJECTILE_HEIGHT = 5.25;
        public static double VELOCITY = 25.1;

        // public static double COUNTERWEIGHT_MASS = ((VELOCITY*VELOCITY)*PROJECTILE_MASS + 2*PROJECTILE_MASS*PROJECTILE_HEIGHT*GRAVITY)/(2*COUNTERWEIGHT_HEIGHT*GRAVITY);

        public static int MAX_INPUT_CHARS = 5;

        public static Font font =  LoadFont("./Game/Assets/Fonts/Romulus.ttf");

    }
}