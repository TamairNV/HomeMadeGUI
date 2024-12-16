namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;

/// <summary>
/// Represents a button with a picture that can be flipped horizontally.
/// </summary>
public class PictureButton<Tout,Tin> : Button<Tout,Tin>
{
    public Texture2D ButtonTexture;
    private float scale;
    private bool flip;

    public PictureButton(Position position, Func<Tin, Tout> func, Tin input, string texturePath, float scale = 1,
        bool flip = false)
    {
        this.flip = flip;
        Position = position;
        Func = func;
        this.input = input;
        ButtonTexture = Raylib.LoadTexture(texturePath);
        this.scale = scale;
    }
    
    
    public override void HandleButton(int xOffset = 0, int yOffset = 0)
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();

        bool overlapping = Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),
            new Rectangle(Position.X, Position.Y, ButtonTexture.Width*scale, ButtonTexture.Height*scale));
        
        
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
    /// <summary>
    /// Handles the drawing of the button, including flipping the texture if necessary.
    /// </summary>
    public override void Draw()
    {
        if (flip)
        {
            Rectangle sourceRec = new Rectangle(0, 0, ButtonTexture.Width, ButtonTexture.Height); // Full texture
            Rectangle destRec = new Rectangle(Position.X, Position.Y, ButtonTexture.Width * scale, ButtonTexture.Height * scale); // Scaled destination
            Vector2 origin = new Vector2(0, 0); // Top-left corner as the origin

            // Flip the texture horizontally by negating the source width
            sourceRec.Width = -sourceRec.Width;

            // Draw the flipped texture
            Raylib.DrawTexturePro(ButtonTexture, sourceRec, destRec, origin, 0.0f, currentColor);
        }
        else
        {
            Raylib.DrawTextureEx(ButtonTexture, new Vector2(Position.X, Position.Y), 0.0f, scale, currentColor);
        }
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