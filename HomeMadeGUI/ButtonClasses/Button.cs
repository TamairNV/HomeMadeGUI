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
    
    protected Color textColor = Pallet.PrimaryTextColor;
    protected Color hoverColor = Pallet.ButtonHoverColor;
    protected Color pressedColor = Pallet.ButtonActiveColor;
    protected Color currentColor;

    private Color buttonColor = Pallet.ButtonPrimaryColor;
    
    private RoundedBox roundedBox = null;
    protected Font font;

    private bool square;

    public Button()
    {
        
    }

    public Button(Position position,Bounds bounds, Func<Tin,Tout> func,Tin input,string defaultStr,Font font,int size,bool isRounded = true,bool square = false)
    {
        this.square = square;
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

    public virtual void HandleButton(int xOffset = 0, int yOffset = 0)
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();

        bool overlapping = Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),new Rectangle(Position.X - xOffset,Position.Y - yOffset,bounds.X,bounds.Y));
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
            if (square)
            {
                roundedBox.Draw(square:true);
            }
            else
            {
                roundedBox.Draw();
            }
        }
        else 
        {
            if (square)
            {
                Raylib.DrawRectangle(Position.X,Position.Y,bounds.X,bounds.X,currentColor);
            }
            else
            {
                Raylib.DrawRectangle(Position.X,Position.Y,bounds.X,bounds.Y,currentColor);
            }
        }
        
        int fontSize = 10; // Start with a small font size

        while (Raylib.MeasureText(defaultStr, fontSize) <= bounds.X )
        {
            fontSize++;
        }

        float diff = bounds.X - Raylib.MeasureText(defaultStr, fontSize);
        Vector2 pos = new Vector2(Position.X - diff/2  , (Position.Y + bounds.Y / 2) - fontSize/2);
        Raylib.DrawTextEx(font,defaultStr,pos,fontSize * 0.9f,1,textColor);
  
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
        currentColor = hoverColor;
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