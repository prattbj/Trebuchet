using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Trebuchet.Game.Scripting;
using Trebuchet.Game.Casting;
namespace Trebuchet.Game.Director
{
    
    public class Director
    {
        
        private DrawScreen drawScreen = new DrawScreen();
        
        private Ball ball = new Ball();
        private InputField counterWeight = new InputField(1010, 835, 100, 35);
        public Director()
        {
            
        }
        public void RunGame() 
        {

            drawScreen.Execute(ball, counterWeight);
            if (ball.getExists())
            {
                ball.move();
            }
            else if (counterWeight.getIfSet())
            {
                ball = new Ball(100, counterWeight.getAnswer(), 1, 5.5, 30);
            }
            

        }
    }
}