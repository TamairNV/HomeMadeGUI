using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class RoundedBox : Box
{

    public int Roundness;

    public RoundedBox(Position position, Bounds size, Color colour,int roundness = 10) : base() 
    {
        Position = position;
        Bounds = size;
        Colour = colour;
        Roundness = roundness;
    }

    public void Draw()
        {
            //Draws three rectangles and 4 circle segments to create a rounded rectangle
            
            //Draw large rect
            Raylib.DrawRectangle((int)Position.X+Roundness,(int)Position.Y,(int)Bounds.X-Roundness*2,(int)Bounds.Y,Colour);

            //Draw the two smaller side rects
            Raylib.DrawRectangle(
                (int)Position.X,
                (int)Position.Y + Roundness,
                Roundness,
                (int)Bounds.Y - Roundness *2,
                Colour);
            Raylib.DrawRectangle(
                (int)Position.X + Bounds.X - Roundness,
                (int)Position.Y + Roundness,
                Roundness,
                (int)Bounds.Y - Roundness *2,
                Colour);
            
            //draw circle segemnts
            Vector2[] positions = new[]
            {
                new Vector2(Position.X + Roundness, Position.Y + Roundness),
                new Vector2(Position.X + Bounds.X - Roundness, Position.Y + Roundness),
                new Vector2(Position.X + Bounds.X - Roundness, Position.Y + Bounds.Y - Roundness),
                new Vector2(Position.X + Roundness, Position.Y + Bounds.Y - Roundness)

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