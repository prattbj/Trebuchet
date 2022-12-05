using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Trebuchet.Game.Casting
{
    public class InputField
    {
        private Rectangle rectangle;
        private double answer = 0;
        private string input = "";
        private int charCount;
        private bool typing;
        public InputField(int x, int y, int width, int height)
        {
            rectangle = new Rectangle(x, y, width, height);
        }
        public void setCharCount(int num)
        {
            charCount += num;
        }
        public int getCharCount()
        {
            return charCount;
        }
        public void setTyping(bool typing)
        {
            this.typing = typing;
        }
        public bool getTyping()
        {
            return typing;
        }
        public Rectangle getRectangle()
        {
            return rectangle;
        }

        public double getAnswer()
        {
            return answer;
        }

        public void setInput(string input)
        {
            this.input = input;
        }
        public void addChar(char key)
        {
            this.input += key;
        }
        
        public void setAnswer()
        {
            answer = Convert.ToDouble('0' + input);
        }
        public string getInput()
        {
            return input;
        }
        public bool getIfSet()
        {
            if (answer == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}