using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class Button<Tout,Tin>
{
    protected Position Position;
    protected Func<Tin,Tout> Func;
    protected Tin input;
    protected string defaultStr = "";
    protected int size;
    private Bounds bounds;
    
    protected Color textColor = Pallet.Colour[6];
    protected Color hoverColor = Pallet.Colour[3];
    protected Color pressedColor = Pallet.Colour[0];
    protected Color currentColor;

    private Color buttonColor = Pallet.Colour[2];
    
    private RoundedBox roundedBox = null;
    protected Font font;

    public Button()
    {
        
    }

    public Button(Position position,Bounds bounds, Func<Tin,Tout> func,Tin input,string defaultStr,Font font,int size,bool isRounded = true)
    {
        this.bounds = bounds;
      
        if (isRounded)
        {
            roundedBox = new RoundedBox(position, bounds, currentColor);
           
        }
        Position = position;
        Func = func;
        this.input = input;
        currentColor = textColor;
        this.size = size;
        this.font = font;
        this.defaultStr = defaultStr;

        

    }
    public virtual void HandleButton()
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();

        bool overlapping = Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),new Rectangle(Position.X,Position.Y,bounds.X,bounds.Y));
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
        if (roundedBox != null)
        {
            
            roundedBox.Colour = currentColor;
            roundedBox.Draw();
        }
        else //Ep<People & Ah<Hotel & stay(p h) ^ pool(h)
        {
            Raylib.DrawRectangle(Position.X,Position.Y,bounds.X,bounds.Y,currentColor);
        }
        
        Vector2 pos = new Vector2(Position.X + bounds.X / 2, Position.Y + bounds.Y / 2);
        int textSize = Raylib.MeasureText(defaultStr, size);
        
        Text.DrawTextCentered(pos, defaultStr, font, textColor, fontSize: size * (bounds.X/textSize));
    }
    public virtual void MouseHover()
    {
        currentColor = hoverColor;
    }
    public virtual void Idle()
    {
        currentColor = buttonColor;
    }

    public virtual void MousePress()
    {
        currentColor = pressedColor;
    }

    public virtual void MouseRelease()
    {
        currentColor = buttonColor;
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