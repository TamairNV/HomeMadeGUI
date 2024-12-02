namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class Box
{
    public Position Position;
    public Bounds Bounds;
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

    public void Draw()
    {
        Raylib.DrawRectangle(Position.X,Position.Y,Bounds.X,Bounds.Y,Colour);
    }
}