using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class ScrollBar
{

    public Position Position;
    public Bounds Bounds;
    private bool selected = false;
    private Vector2 HandlePosition;
    public float Value;
    public ScrollBar(Position position,Bounds bounds,float defaultValue = 0)
    {
        Position = position;
        Bounds = bounds;
        Value = defaultValue;
        HandlePosition = new Vector2(Position.X + Bounds.X * defaultValue, Position.Y);

    }

    private void Draw()
    {
        HandlePosition = new Vector2(Position.X + Bounds.X * Value, Position.Y);
        Raylib.DrawRectangleRounded(new Rectangle(Position.X,Position.Y,Bounds.X,Bounds.Y/2),5,15,Pallet.AccentColor);
        Raylib.DrawCircle((int)HandlePosition.X,(int)HandlePosition.Y+Bounds.Y/4,6,Pallet.AccentColor);
        Raylib.DrawCircle((int)HandlePosition.X,(int)HandlePosition.Y+Bounds.Y/4,5,Pallet.SecondaryColor);
        
    }

    public void HandelScrollBar()
    {
        Draw();
        Vector2 mousePos = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One),
                new Rectangle(Position.X, Position.Y, Bounds.X, Bounds.Y)))
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                selected = true;


            }
            
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            selected = false;
        }

        if (selected)
        {
            float diff = (Bounds.X + Position.X - mousePos.X)/Bounds.X;
            Value = Math.Clamp(1-diff,0,1);
            HandlePosition.X = mousePos.X;
        }
    }
    
    
}