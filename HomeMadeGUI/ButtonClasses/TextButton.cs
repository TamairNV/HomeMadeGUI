namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class TextButton<Tout,Tin> : Button<Tout,Tin>
{


    
    public TextButton(Position position,Func<Tin,Tout> func,Tin input,string defaultStr,Font font,int size)
    {
        Position = position;
        Func = func;
        this.input = input;
        currentColor = textColor;
        this.size = size;
        this.font = font;
        this.defaultStr = defaultStr;
    }

    public override void HandleButton(int xOffset = 0, int yOffset = 0)
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();
        bool overlapping = CheckTextPointCollision(new Vector2(Position.X - xOffset,Position.Y- yOffset),defaultStr,size, mousePos);
        if (!overlapping)
        {
            Idle();
            return;
        };
        bool mouseDown = Raylib.IsMouseButtonDown(MouseButton.Left);
        bool mouseReleased = Raylib.IsMouseButtonReleased(MouseButton.Left);
        if (mouseDown)
        {
            MousePress();
            return;
        }
        if (mouseReleased)
        {
            MouseRelease();
            return;
        }
        MouseHover();
        

    }
    public override void Draw()
    {
        Text.DrawTextCentered(new Vector2(Position.X, Position.Y), defaultStr, font, currentColor, fontSize:size);
    }
    
    public override void MouseHover()
    {
        currentColor = hoverColor;
    }
    public override void Idle()
    {
        currentColor = textColor;
    }

    public override void MousePress()
    {
        
        currentColor = pressedColor;
    }

    public override void MouseRelease()
    {
        Run();
        currentColor = hoverColor;
    }
}