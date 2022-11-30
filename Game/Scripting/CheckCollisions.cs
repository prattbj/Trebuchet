using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;
namespace Trebuchet_Game.Game.Scripting
{
    public class CheckCollisions
    {
        public execute(Ball ball, Castle castle)
        {
            if (CheckCollisionCircleRec(ball.getCenter(), 10, castle.getRectangle()))
            {
                ball
            }
        }
    }
}