using Raylib_cs;
using Trebuchet.Game.Casting;
namespace Trebuchet.Game.Scripting
{
    public class CheckCollisions
    {
        public void execute(Ball ball, Castle castle)
        {
            if (Raylib.CheckCollisionCircleRec(ball.getCenter(), 10, castle.getRec()))
            {
                ball.SetExists();
            }
        }
    }
}