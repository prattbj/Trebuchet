// imports
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System.Numerics;
namespace Trebuchet.Game.Casting
{
    public class Ball
    {
        // ball variables 
        private double dx;
        private double dy;
        private double aDegrees;
        private double aRadians;            // Angle in radians
        private double v;                   // Total velocity
        private bool exists;
        private double x;
        private double y;
        private double scale = .19;

        // functions
        // makes ball visable
        public Ball()
        {
            exists = false;
        }

        // initalize ball
        public Ball(double bW, double cW, double bH, double cH, double degrees)
        {
            exists = false;
            this.v = ComputeVelocity(bW, cW, cH, bH);
            this.aRadians = degrees * Math.PI / 180;
            this.dx = Math.Sin(aRadians) * v * 1280/250;
            this.dy = -Math.Cos(aRadians) * v * 1280/250;
            this.aDegrees = degrees;
            this.x = 100;
            this.y = 666;
        }

        // math to move the ball
        public void move()
        {
            this.x += (dx * (1.0 / 60.0 ));
            this.y += ((dy * 1/60.0) + (.5 * 9.8 * (1.0 / 60.0 ) * (1.0 / 60.0 )));
            this.dy += (9.8 * 1280/250) * 1.0/60.0;
            v = Math.Sqrt(dx * dx + dy * dy) / (1280 / 250);

            if (this.x > 1600 || this.y > 900)
            {
                exists = false;
                v = 0;
            }
        }

        // total velocity
        static double ComputeVelocity(double ballWeight, double counterWeight, double counterHeight, double ballHeight)
        {
            return Math.Sqrt(-2 * (ballWeight * 9.8 * ballHeight - counterWeight *  9.8 * counterHeight)/ballWeight);
        }

        // vertical velocity
        static double ComputeVerticalComponent(double a, double total)
        {
            return Math.Cos(a) * total;
        }

        // horizontal velocity 
        static double ComputeHorizontalComponent(double a, double total)
        {
            return Math.Sin(a) * total;
        }

        static double ComputeTotalComponent(double x, double y)
        {
            return Math.Sqrt((x * x) + (y * y));
        }

        static double radiansFromDegrees(double d)
        {
            return (d * (Math.PI / 180));
        }


        //Getters and Setters
        public bool getExists()
        {
            return exists;
        }
        public void SetExists(bool e)
        {
            exists = e;
        }
        public double getDX()
        {
            return this.dx;
        }
        public double getDY()
        {
            return this.dy;
        }

        public double getADegrees()
        {
            return this.aDegrees;
        }
        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
        public double getARadians()
        {
            return this.aRadians;
        }
        public double getV()
        {
            return this.v;
        }

        public void setDX(double value)
        {
            this.dx = value;
        }
        public void setDY(double value)
        {
            this.dy = value;
        }
        
        public void setADegrees(double value)
        {
            this.aDegrees = value;
        }
        
        public void setARadians(double value)
        {
            this.aRadians = value;
        }
        public void setV(double value)
        {
            this.v = value;
        }
        public double getScale()
        {
            return scale;
        }
        public Vector2 getCenter()
        {
            return new Vector2((float)(x + scale*50), (float)(y - scale*50));
        }
    }
}