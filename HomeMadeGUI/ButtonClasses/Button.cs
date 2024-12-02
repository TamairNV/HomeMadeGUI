using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class Button<Tout,Tin>
{
    protected Position Position;
    protected Func<Tin,Tout> Func;
    protected Tin input;
    protected string defaultStr;
    protected int size;
    public void HandleButton()
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();
        bool overlapping = CheckTextPointCollision(new Vector2(Position.X,Position.Y),defaultStr,size, mousePos);
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

    public virtual void Draw()
    {
        
    }
    public virtual void MouseHover()
    {
        
    }
    public virtual void Idle()
    {
        
    }

    public virtual void MousePress()
    {
       
    }

    public virtual void MouseRelease()
    {
        Run();
    }

    public static bool CheckTextPointCollision(Vector2 pos, string text,int size,Vector2 point)
    {
        int textWidth = Raylib.MeasureText(text, size);
        bool xCheck = point.X >= pos.X- textWidth / 2  && point.X <= pos.X- textWidth / 2 + textWidth;
        bool yCheck = point.Y >= pos.Y- size / 2 && point.Y <= pos.Y- size / 2 + size;
        return xCheck && yCheck;
    }

    public void Run()
    {
        Func.Invoke(input);
    }
}