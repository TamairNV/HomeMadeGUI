using System.Collections.Specialized;
using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class RoundedBox : Box
{
    public int Roundness;
    private bool fixedSize;


    public RoundedBox(Position position, Bounds size, Color colour, int roundness = 10, bool fixedSize = false)
    {
        Position = position;
        Bounds = size;
        Colour = colour;
        Roundness = roundness;
        this.fixedSize = fixedSize;
    }


    public override void Draw(bool square = false)
    {
        Vector2 bounds;
        if (fixedSize)
        {
            bounds = new Vector2(Bounds.RelativeBounds.X, Bounds.RelativeBounds.Y);
            if (square)
            {
                bounds = new Vector2(Bounds.RelativeBounds.X, Bounds.RelativeBounds.X);
            }
            
        }
        else
        {
            bounds = new Vector2(Bounds.X, Bounds.Y);
            if (square)
            {
                bounds = new Vector2(Bounds.X, Bounds.X);
            }
        }
        //Draws three rectangles and 4 circle segments to create a rounded rectangle

        //Draw large rect

        Raylib.DrawRectangle((int)Position.X + Roundness, (int)Position.Y, (int)bounds.X - Roundness * 2, (int)bounds.Y,
            Colour);

        //Draw the two smaller side rects
        Raylib.DrawRectangle(
            (int)Position.X,
            (int)Position.Y + Roundness,
            Roundness,
            (int)bounds.Y - Roundness * 2,
            Colour);
        Raylib.DrawRectangle(
            (int)Position.X + (int)bounds.X - Roundness,
            (int)Position.Y + Roundness,
            Roundness,
            (int)bounds.Y - Roundness * 2,
            Colour);

        //draw circle segemnts
        Vector2[] positions = new[]
        {
            new Vector2(Position.X + Roundness, Position.Y + Roundness),
            new Vector2(Position.X + bounds.X - Roundness, Position.Y + Roundness),
            new Vector2(Position.X + bounds.X - Roundness, Position.Y + bounds.Y - Roundness),
            new Vector2(Position.X + Roundness, Position.Y + bounds.Y - Roundness)
        };
        int startAngle = 180;
        int endAngle = 270;
        int i = 0;
        foreach (var pos in positions)
        {
            Raylib.DrawCircleSector(pos, Roundness, startAngle + i * 90, endAngle + i * 90, Roundness, Colour);
            i++;
        }
    }
}