using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Trebuchet.Game.Scripting;
using Trebuchet.Game.Casting;
using System.Numerics;
namespace Trebuchet.Game.Director
{
    
    public class Director
    {
        
        private DrawScreen drawScreen = new DrawScreen();
        
        // private CheckCollisions checkCollisions = new CheckCollisions();
        private Castle castle = new Castle(new Vector2(1400, 750));
        private Ball ball = new Ball();
        private InputField counterWeight = new InputField(1005, 835, 100, 35);
        private InputField ballWeight = new InputField(565, 835, 100, 35);
        private InputField counterHeight = new InputField(240, 835, 100, 35);
        public Director()
        {
            
        }
        public void RunGame() 
        {

            drawScreen.Execute(ball, castle, counterWeight, ballWeight, counterHeight);
            if (ball.getExists())
            {
                ball.move();
            }
            else if (counterWeight.getIfSet() && ballWeight.getIfSet())
            {
                ball = new Ball(ballWeight.getAnswer(), counterWeight.getAnswer(), Constants.PROJECTILE_HEIGHT, counterHeight.getAnswer(), 30);
                ballWeight.setInput("");
                ballWeight.setAnswer();
                counterHeight.setInput("");
                counterHeight.setAnswer();
                counterWeight.setInput("");
                counterWeight.setAnswer();
            }

        }
    }
}