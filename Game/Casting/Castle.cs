using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
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
            return new Rectangle(pos.X, pos.Y, 250, 400);
        }

    }
}