namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class Box
{
    public Position Position;
    public Bounds Bounds;
    public Color Colour;
    private bool fixedSize;
    private int outlineThickness;
    private bool outline;
    public Box()
    {
        
    }
    public Box(Position position, Bounds size, Color colour,bool fixedSize = false,bool outline = false,int outlineThickness = 3)
    {
        this.outline = outline;
        Position = position;
        Bounds = size;
        this.fixedSize = fixedSize;
        Colour = colour;
        this.outlineThickness = outlineThickness;
    }

    public virtual void Draw(bool square = false)
    {
        Vector2 bounds;
        if (fixedSize)
        {
            bounds = new Vector2(Bounds.RelativeBounds.X, Bounds.RelativeBounds.Y);
        }
        else
        {
            bounds = new Vector2(Bounds.X, Bounds.Y);
        }


        Raylib.DrawRectangle(Position.X,Position.Y,(int)bounds.X,(int)bounds.Y,Colour);
        if (outline)
        {
            Vector2 offset = Vector2.Zero;
            if (outlineThickness < 0)
            {
                offset = new Vector2(outlineThickness, outlineThickness);
            }
            Raylib.DrawRectangleLinesEx(new Rectangle(Position.X+offset.X,Position.Y+offset.Y,(int)bounds.X,(int)bounds.Y),Math.Abs(outlineThickness),Pallet.BorderColor);
        }

    }
}