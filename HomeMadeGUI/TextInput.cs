using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class TextInput
{
    private string defaultStr = "Nothing";
    private string input = "";
    private Position Position;
    private float barPos;
    private int barIndex= 0 ;
    private int fontSize = 25;

    public TextInput()
    {
        Position = new Position(0.13f, 0.1f);
    }

    private float timer = 0;
    private float enterInterval = 0.5f;

    public void HandleTextInputs()
    {
        timer += Raylib.GetFrameTime();
        string extra = "";

        // Handle blinking effect for the bar
        if (timer >= enterInterval)
        {
            extra = "|";
        }

        if (timer >= enterInterval * 2)
        {
            timer = 0;
        }

        // Handle keyboard input
        float currentWidth = 0;
        if (IsAnyKeyPressed())
        {
            int key = Raylib.GetCharPressed();
            if (key > 0) // Ensure valid character input
            {

                if (input.Length == 0 || barIndex == input.Length-1)
                {
                    input += (char)key;
                    barPos = Raylib.MeasureTextEx(Text.Fonts[3], input, fontSize, 0f).X;
                    barIndex = input.Length-1;
                }
                else
                {
                    barIndex++;
                    string stringKey = Convert.ToString((char)key);
                    input= input.Insert(barIndex,stringKey );
                    
                    barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex+1), fontSize, 0f).X - 3;
                }
                
            }
        }

        // Handle backspace
        if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && input.Length > 0 && barIndex >= 0)
        {
    
            input = input.Remove(barIndex, 1);
            barIndex--;
            barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex+1), fontSize, 0f).X - 3;
        }

    
        Vector2 mousePos = Raylib.GetMousePosition();
        
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            bool atEnd = true;
            for (int i = 0; i <= input.Length; i++)
            {
                // Measure width up to the current character
                if (i < input.Length)
                {
                    string str = "";
                    str += input[i];
                    float charWidth = Raylib.MeasureTextEx(Text.Fonts[3], str, fontSize, 0f).X;

                    if (mousePos.X - Raylib.MeasureTextEx(Text.Fonts[3], "f", fontSize, 0f).X < currentWidth + Position.X)
                    {
                        barIndex = i-1;
                        atEnd = false;
                        break;
                    }

                    currentWidth += charWidth;
                }
            }

            if (atEnd)
            {
                barIndex = input.Length-1 ;
             
            }
        }
        
    
        Raylib.DrawTextEx(Text.Fonts[3], input, new Vector2(Position.X, Position.Y), fontSize, 0, Pallet.PrimaryTextColor);
        
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            barPos = currentWidth - 3;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            if (barIndex < input.Length - 1)
            {
                barIndex++;
                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex+1), fontSize, 0f).X - 3;
            }
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            if (barIndex >= 0 )
            {
                barIndex--;
                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex+1), fontSize, 0f).X - 3;
            }
        }
        
            
        Raylib.DrawTextEx(Text.Fonts[3], extra,
            new Vector2(barPos + Position.X, Position.Y), fontSize, 0,
            Pallet.PrimaryTextColor);
    }


    bool IsAnyKeyPressed()
    {
        bool keyPressed = false;
        int key = Raylib.GetKeyPressed();

        if ((key >= 32) && (key <= 126)) keyPressed = true;

        return keyPressed;
    }
}