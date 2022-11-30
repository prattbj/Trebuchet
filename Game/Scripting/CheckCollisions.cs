using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;
using Trebuchet.Game.Casting;
namespace Trebuchet.Game.Scripting
{
    public class CheckCollisions
    {
        public void execute(Ball ball, Castle castle)
        {
            if (CheckCollisionCircleRec(ball.getCenter(), 10, castle.getRectangle()))
            {
                ball.SetExists();
            }
        }
    }
}