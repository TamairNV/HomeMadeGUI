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
    private float step;
    public ScrollBar(Position position, Bounds bounds, float defaultValue = 0, float step = 0)
    {
        this.step = step;
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
            float diff = (Bounds.X + Position.X - mousePos.X) / Bounds.X;
            float rawValue = Math.Clamp(1 - diff, 0, 1);

            if (step > 0)
            {
                // Adjust value to snap to the nearest step
                Value = (float)Math.Round(rawValue / step) * step;
            }
            else
            {
                // Smooth value
                Value = rawValue;
            }

            HandlePosition.X = (1 - Value) * (Bounds.X + Position.X) + Value * (Bounds.X + Position.X + Bounds.X);

        }
    }
    
    
}