using Raylib_cs;
using Trebuchet.Game.Casting;
namespace Trebuchet.Game.Scripting
{
    public class CheckCollisions
    {
        private bool explode = false;
        public void execute(Ball ball, Castle castle)
        {
            if (Raylib.CheckCollisionCircleRec(ball.getCenter(), 10, castle.getRec()))
            {
                ball.SetExists(false);
                explode = true;
            }            

        }
        public bool getExplode()
        {
            return explode;
        }
        public void setExplode(bool expl)
        {
            explode = expl;
        }
    }
}