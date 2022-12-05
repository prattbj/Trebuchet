using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Trebuchet.Game.Casting;
using System;
using System.Numerics;
namespace Trebuchet.Game.Scripting
{
    public class DrawScreen
    {

        //double answer = 0;


        int trebuchetFrame = 0;
        int explosionFrame = 0;
        int framesCounter = 0;

        bool throwing = false;



        bool launch = false;
        float trebuchetTimer = 0.0f;
        float explosionTimer = 0.0f;

        private CheckCollisions checkCollisions = new CheckCollisions();

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public DrawScreen()
        {
            Image idleTrebuchet = LoadImage("Game/Assets/Images/trebuchet/trebuchet idle.png");
            Texture2D idleTexture = LoadTextureFromImage(idleTrebuchet);
            textures["Idle Trebuchet"] = idleTexture;

            Image background = LoadImage("Game/Assets/Images/background v2.png");
            Texture2D bgTexture = LoadTextureFromImage(background);
            textures["Background"] = bgTexture;

            Image ball = LoadImage("Game/Assets/Images/ball.png");
            Texture2D ballTexture = LoadTextureFromImage(ball);
            textures["Ball"] = ballTexture;

            Image castle = LoadImage("Game/Assets/Images/castle2.png");
            Texture2D castleTexture = LoadTextureFromImage(castle);
            textures["Castle"] = castleTexture;

            Image trebuchet = LoadImage("Game/Assets/Images/trebuchet/trebuchet sprite sheet.png");
            Texture2D trebuchetTexture = LoadTextureFromImage(trebuchet);
            textures["Moving Trebuchet"] = trebuchetTexture;

            Image explosion = LoadImage("Game/Assets/Images/explosions/explosion sprite sheet (15 frames).png");
            Texture2D explosionTexture = LoadTextureFromImage(explosion);
            textures["Explosion"] = explosionTexture;
        }
        public void Execute(Ball ball, Castle castle, InputField counterWeight, InputField ballWeight, InputField counterHeight)
        {
            BeginDrawing();
            ClearBackground(RAYWHITE);

            DrawBackground(textures["Background"]);
            if (ball.getExists())
                DrawBall(textures["Ball"], ball);

            DrawCastle(textures["Castle"], castle);
            DrawInputField(counterWeight);
            DrawInputField(ballWeight);
            DrawInputField(counterHeight);
            if (!ball.getExists() && counterWeight.getCharCount() > 0 && ballWeight.getCharCount() > 0 && counterHeight.getCharCount() > 0)
            {
                DrawSubmitButton(1150, 840, 185, 50);
            }
            else
            {
                checkCollisions.execute(ball, castle);
            }
            
            
            DrawEquations(ball);

            //When the submit button is pressed, set the answers for each input field and reset launch
            if (launch == true)
            {
                counterWeight.setAnswer();
                ballWeight.setAnswer();
                counterHeight.setAnswer();
                throwing = true;
                launch = false;
            }

            if (throwing == true)
            {
                trebuchetTimer += GetFrameTime();

                if (trebuchetTimer >= .1f)
                {
                    trebuchetTimer = 0.0f;
                    trebuchetFrame++;                
                }
                DrawTrebuchetAnimation(textures["Moving Trebuchet"], trebuchetFrame);

                if (trebuchetFrame == 13)
                {
                    throwing = false;
                    trebuchetFrame = 0;
                }
                // throw ball on frame 9
                else if (trebuchetFrame == 8)
                {
                    ball.SetExists(true);
                }
            }
            else
                DrawTrebuchet(textures["Idle Trebuchet"]);

            if (checkCollisions.getExplode())
            {
                explosionTimer += GetFrameTime();

                if (explosionTimer >= .1f)
                {
                    explosionTimer = 0.0f;
                    explosionFrame++;                
                }
                DrawExplosion(textures["Explosion"], explosionFrame);

                if (explosionFrame == 14)
                {
                    checkCollisions.setExplode(false);
                    explosionFrame = 0;
                }
            }

            EndDrawing();
        }
        
        bool IsAnyKeyPressed()
        {
            bool keyPressed = false;
            int key = GetKeyPressed();

            if ((key >= 32) && (key <= 126)) keyPressed = true;

            return keyPressed;
        }
        private void DrawBackground(Texture2D texture)
        {
            DrawTexture(texture,  0, 0, WHITE);
        }

        private void DrawBall(Texture2D texture, Ball ball)
        {
            DrawTextureEx(texture, new Vector2((int)ball.getX(), (int)ball.getY()), 0, (float)ball.getScale(), WHITE);

        }

        private void DrawCastle(Texture2D texture, Castle castle)
        {
            DrawTextureEx(texture, new Vector2((int)castle.getPos().X, (int)castle.getPos().Y), 0, 1, WHITE);
        }

        private void DrawEquations(Ball ball)
        {
            //Positions
            Vector2 defPosition = new Vector2(15, 10);
            Vector2 defPosition2 = new Vector2(15, 40);
            Vector2 cwHeightPos = new Vector2(15, 840);
            Vector2 gPos = new Vector2(15, 870);
            Vector2 pMassPos = new Vector2(385, 840);
            Vector2 pHeightPos = new Vector2(385, 870);
            Vector2 velPos = new Vector2(805, 870);
            Vector2 cwMassPos = new Vector2(805, 840);

            // draw the help equation at the top of screen
            DrawTextEx(Constants.font, "v = sqrt(-2(mgh - MgH)/m). M is mass of counterweight, g is gravity , H is height of the counterweight,", defPosition, 30, 1, BLACK);
            DrawTextEx(Constants.font, "m is mass of projectile and h is height of projectile at launch.", defPosition2, 30, 1, BLACK);

            // draw the variables to the screen
            DrawTextEx(Constants.font, $"Gravity = {Constants.GRAVITY}m/s", gPos, 30, 1, BLACK);
            DrawTextEx(Constants.font, $"Height of CW =          m", cwHeightPos, 30, 1, BLACK);
            DrawTextEx(Constants.font, $"Mass of P =           kg", pMassPos, 30, 1, BLACK);
            DrawTextEx(Constants.font, $"Height of PL = {Constants.PROJECTILE_HEIGHT}m", pHeightPos, 30, 1, BLACK);
            DrawTextEx(Constants.font, $"Velocity = {String.Format("{0:0.00}", ball.getV())}m/s^2", velPos, 30, 1, BLACK);
            DrawTextEx(Constants.font, "Mass of CW =          kg", cwMassPos, 30, 1, BLACK);
        }
        private void DrawInputField(InputField inputField)
        {

            if (CheckCollisionPointRec(GetMousePosition(), inputField.getRectangle()))
            {
                if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                    inputField.setTyping(true);
            }

            if (inputField.getTyping())
            {
                // Set the window's cursor to the I-Beam
                SetMouseCursor(MouseCursor.MOUSE_CURSOR_IBEAM);

                // Get char pressed (unicode character) on the queue
                int key = GetCharPressed();

                // Check if more characters have been pressed on the same frame
                while (key > 0)
                {
                    
                    // NOTE: Only allow keys in range [48..57]
                    if (((key >= 48 && key <= 57) || key == 46) && (inputField.getInput().Length < Constants.MAX_INPUT_CHARS))
                    {
                        if ((!(inputField.getInput().Contains('.'))) || (key != 46))
                        {
                            inputField.addChar((char)key);
                            inputField.setCharCount(1);
                                
                        }
                        
                    }

                    key = GetCharPressed();  // Check next character in the queue
                }

                // backspace functionality
                if (IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                {
                    if (inputField.getInput().Length > 0)
                    {
                        inputField.setInput(inputField.getInput().Remove(inputField.getInput().Length - 1, 1));
                        inputField.setCharCount(-1);
                    }
                }
                if (CheckCollisionPointRec(GetMousePosition(), inputField.getRectangle()) == false)
                {
                    if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                        inputField.setTyping(false);
                }
            }
            else SetMouseCursor(MouseCursor.MOUSE_CURSOR_DEFAULT);

            if (inputField.getTyping()) framesCounter++;
            else framesCounter = 0;
            // Update
            //———————————————————————————-
            DrawRectangleRec(inputField.getRectangle(), LIGHTGRAY);
            if (inputField.getTyping()) 
                DrawRectangleLines((int)inputField.getRectangle().x, (int)inputField.getRectangle().y, (int)inputField.getRectangle().width, (int)inputField.getRectangle().height, RED);
            else 
                DrawRectangleLines((int)inputField.getRectangle().x, (int)inputField.getRectangle().y, (int)inputField.getRectangle().width, (int)inputField.getRectangle().height, DARKGRAY);

            Vector2 textPos = new Vector2((int)inputField.getRectangle().x + 5, (int)inputField.getRectangle().y + 2);
            DrawTextEx(Constants.font, inputField.getInput(), textPos, 35, 1, BLACK);

            if (inputField.getTyping())
                {
                    if (inputField.getCharCount() < Constants.MAX_INPUT_CHARS)
                    {
                        // Draw blinking underscore char
                         Vector2 cursorPos = new Vector2((int)inputField.getRectangle().x + 8 + MeasureText(inputField.getInput(), 30), (int)inputField.getRectangle().y + 8);
                        if (((framesCounter / 20) % 2) == 0)
                            DrawTextEx(Constants.font, "_", cursorPos, 30, 1, MAROON);
                    }
                }   
        }
        private void DrawSubmitButton(int x, int y, int l, int w)
        {
            Rectangle button = new Rectangle(x, y, l, w);
            DrawRectangleRec(button, DARKGRAY);
            Vector2 submitPosition = new Vector2(x + 16, y + 5);
            DrawTextEx(Constants.font, "LAUNCH", submitPosition, 40, 1, WHITE);

            if (CheckCollisionPointRec(GetMousePosition(), button))
            {
                DrawRectangleRec(button, MAROON);
                //DrawText("LAUNCH", (int)button.x + 8, (int)button.y + 8, 40, LIGHTGRAY);
                DrawTextEx(Constants.font, "LAUNCH", submitPosition, 40, 1, WHITE);
                if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                    launch = true;
            }
        }

        private void DrawPlayButton(int x, int y, int width, int height)
        {
            Rectangle button = new Rectangle(x, y, width, height);
            DrawRectangleRec(button, DARKGRAY);
            DrawText("Play Again", (int)button.x + 8, (int)button.y + 8, 40, WHITE);

            if (CheckCollisionPointRec(GetMousePosition(), button))
            {
                DrawRectangleRec(button, MAROON);
                DrawText("Play Again", (int)button.x + 8, (int)button.y + 8, 40, LIGHTGRAY);
            }
        }
        private void DrawExitButton(int x, int y, int width, int height)
        {
            Rectangle button = new Rectangle(x, y, width, height);
            DrawRectangleRec(button, DARKGRAY);
            DrawText("Exit", (int)button.x + 8, (int)button.y + 8, 40, WHITE);

            if (CheckCollisionPointRec(GetMousePosition(), button))
            {
                DrawRectangleRec(button, MAROON);
                DrawText("Exit", (int)button.x + 8, (int)button.y + 8, 40, LIGHTGRAY);
                if (IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                    // close game window
                    CloseWindow();   
            }
        }

        private void DrawTrebuchet(Texture2D texture)
        {
            DrawTextureEx(texture, new Vector2(0, 400), 0, 1, WHITE);
        }
        private void DrawTrebuchetAnimation(Texture2D texture, int frame)
        {
            float frameWidth = 400;

            Rectangle border = new Rectangle(frameWidth * frame, 0, frameWidth, (float)texture.height);
            Vector2 pos = new Vector2(0, 400);
            DrawTextureRec(texture, border, pos, WHITE);
        }
        private void DrawExplosion(Texture2D texture, int frame)
        {
            float frameWidth = 300;
            Rectangle border = new Rectangle(frameWidth * frame, 0, frameWidth, (float)texture.height);
            Vector2 pos = new Vector2(1300, 650);
            DrawTextureRec(texture, border, pos, WHITE);

        }
    }
}