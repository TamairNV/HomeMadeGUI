using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class TextInput
{
    private string defaultStr = "Nothing";
    private string input = "";
    private Position Position;
    private float barPos;
    private int barIndex = 0;
    private int fontSize = 25;
    private Vector2 boundsSize;
    private bool active = false;
    private bool rounded = false;
    private int verticalPadding;
    public TextInputGroup Group;
    public int GroupPosition;

    public TextInput(Position position, int size, int fontSize,string defaultStr, bool rounded = false,int verticalPadding = 4)
    {
        Position = position;
        boundsSize = new Vector2(size, fontSize +  verticalPadding);
        this.fontSize = fontSize;
        this.rounded = rounded;
        this.verticalPadding = verticalPadding;
        this.defaultStr = defaultStr;
    }

    private float timer = 0;
    private float enterInterval = 0.5f;

    public void HandleField()
    {
        
        if (rounded)
        {
            RoundedBox box = new RoundedBox(Position, new Bounds(boundsSize.X, boundsSize.Y), Pallet.ButtonPrimaryColor,
                fixedSize: true);
            box.Draw();
        }
        else
        {
            Box box = new Box(Position, new Bounds(boundsSize.X, boundsSize.Y), Pallet.ButtonPrimaryColor,
                fixedSize: true);
            box.Draw();
        }

        
        CheckIfActive();
        if (active)
        {
            HandleTextInputs();
        }

        if (input.Length < 1)
        {
            Raylib.DrawTextEx(Text.Fonts[3], defaultStr,
                new Vector2(Position.X + verticalPadding/2, Position.Y + verticalPadding/2), fontSize, 0,
                Pallet.SecondaryTextColor);
        }

        else
        {
            DrawInput();
        }
    }
  
    private void CheckIfActive()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            if (Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),
                    new Rectangle(Position.X, Position.Y, boundsSize.X, boundsSize.Y)))
            {
                HandleTextInputs();
                active = true;
            }
            else
            {
                active = false;
            }
        }
    }

    private void HandleTabChanges()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Tab))
        {
            if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
            {
                if (GroupPosition - 1 < 0)
                {
                    return;
                }
                Group.TextInputs[GroupPosition - 1].active = true;
                active = false;
            }
            if (GroupPosition + 1 >= Group.TextInputs.Count)
            {
                return;
            }
            Group.TextInputs[GroupPosition + 1].active = true;
            active = false;
        }
    }

    private void HandleTextInputs()
    {
        HandleTabChanges();
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
        HandleKeyPress();

        // Handle backspace
        HandleBackSpace();


        float currentWidth = 0;
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

                    if (mousePos.X - Raylib.MeasureTextEx(Text.Fonts[3], "f", fontSize, 0f).X <
                        currentWidth + Position.X)
                    {
                        barIndex = i - 1;
                        atEnd = false;
                        break;
                    }

                    currentWidth += charWidth;
                }
            }

            if (atEnd)
            {
                barIndex = input.Length - 1;
            }
        }

        DrawInput();
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            barPos = currentWidth - 3;
        }

        //Handle Position using arrow keys
        HandleArrowPress();


        Raylib.DrawTextEx(Text.Fonts[3], extra,
            new Vector2(barPos + Position.X + verticalPadding/2,  Position.Y + verticalPadding/2), fontSize, 0,
            Pallet.PrimaryTextColor);
    }

    private void DrawInput()
    {
        Raylib.DrawTextEx(Text.Fonts[3], input, new Vector2(Position.X + verticalPadding/2, Position.Y +  verticalPadding/2),
            fontSize, 0, Pallet.PrimaryTextColor);
    }


    bool IsAnyKeyPressed()
    {
        bool keyPressed = false;
        int key = Raylib.GetKeyPressed();

        if ((key >= 32) && (key <= 126)) keyPressed = true;

        return keyPressed;
    }


    void HandleBackSpace()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && input.Length > 0 && barIndex >= 0)
        {

            if (Raylib.IsKeyDown(KeyboardKey.LeftAlt))
            {
                input = "";
                barIndex = 0;
                barPos = - 3;
                return;
            }
            input = input.Remove(barIndex, 1);
            barIndex--;
            barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex + 1), fontSize, 0f).X - 3;
        

        }
    }

    void HandleKeyPress()
    {
        // Checks if an allowed key is pressed and the box is not full
        if (IsAnyKeyPressed() && boundsSize.X + Position.X >
            Raylib.MeasureTextEx(Text.Fonts[3], input + "s", fontSize, 0f).X + Position.X +  verticalPadding/2)
        {
            int key = Raylib.GetCharPressed();


            if (input.Length == 0 || barIndex == input.Length - 1)
            {
                input += (char)key;
                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input, fontSize, 0f).X;
                barIndex = input.Length - 1;
            }
            else
            {
                barIndex++;
                string stringKey = Convert.ToString((char)key);
                input = input.Insert(barIndex, stringKey);

                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex + 1), fontSize, 0f).X - 3;
            }
        }
    }

    void HandleArrowPress()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            if (barIndex < input.Length - 1)
            {
                barIndex++;
                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex + 1), fontSize, 0f).X - 3;
            }
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            if (barIndex >= 0)
            {
                barIndex--;
                barPos = Raylib.MeasureTextEx(Text.Fonts[3], input.Substring(0, barIndex + 1), fontSize, 0f).X - 3;
            }
        }
    }
}