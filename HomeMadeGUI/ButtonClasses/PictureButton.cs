namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class PictureButton<Tout,Tin> : Button<Tout,Tin>
{
    private Texture2D buttonTexture;
    private float scale;
    
    public PictureButton(Position position, Func<Tin, Tout> func, Tin input, string texturePath, float scale = 1)
    {
        Position = position;
        Func = func;
        this.input = input;
        buttonTexture = Raylib.LoadTexture(texturePath);
        this.scale = scale;
    }
    
    
    public override void HandleButton(int xOffset = 0, int yOffset = 0)
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();

        bool overlapping = Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),
            new Rectangle(Position.X, Position.Y, buttonTexture.Width*scale, buttonTexture.Height*scale));
        
        
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
        Raylib.DrawTextureEx(buttonTexture, new Vector2(Position.X, Position.Y), 0.0f, scale, currentColor);

      
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