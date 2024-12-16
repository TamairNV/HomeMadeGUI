namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class CircleButton<Tout,Tin> : Button<Tout,Tin>
{

    private int radius;
    
    public CircleButton(Position position,int radius,Func<Tin,Tout> func,Tin input,string defaultStr,Font font,int size)
    {
        Position = position;
        Func = func;
        this.input = input;
        currentColor = buttonColor;
        this.size = size;
        this.font = font;
        this.defaultStr = defaultStr;
        this.radius = radius;
    }

    public override void HandleButton(int xOffset = 0, int yOffset = 0)
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();
        bool overlapping = CheckPointCollision(new Vector2(Position.X - xOffset,Position.Y- yOffset),radius, mousePos);
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
        Raylib.DrawCircle(Position.X,Position.Y,radius+2,Color.Black);
        if (currentColor.Equals(pressedColor))
        {
            Raylib.DrawCircle(Position.X,Position.Y,radius-1,currentColor);
        }
        else
        { 
            Raylib.DrawCircle(Position.X,Position.Y,radius,currentColor);
        }
        
        Text.DrawTextCentered(new Vector2(Position.X, Position.Y), defaultStr, font, textColor, fontSize:size);
    }
    
    public override void MouseHover()
    {
        currentColor = hoverColor;
    }
    public override void Idle()
    {
        currentColor = buttonColor;
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