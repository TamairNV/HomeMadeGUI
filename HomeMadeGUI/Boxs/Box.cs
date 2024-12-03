namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class Box
{
    protected Position Position;
    protected Bounds Bounds;
    public Color Colour;

    public Box()
    {
        
    }
    public Box(Position position, Bounds size, Color colour)
    {
        Position = position;
        Bounds = size;
        Colour = colour;
    }

    public virtual void Draw()
    {
        Raylib.DrawRectangle(Position.X,Position.Y,Bounds.X,Bounds.Y,Colour);
    }
}