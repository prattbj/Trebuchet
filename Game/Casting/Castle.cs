using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Trebuchet.Game.Casting
{
    public class Castle
    {
        private Vector2 pos;
        public Castle(Vector2 pos)
        {
            this.pos = pos;
        }

        public Vector2 getPos()
        {
            return pos;
        }
        public Rectangle getRec()
        {
            return Rectangle(pos.x, pos.y, 250, 400);
        }

    }
}