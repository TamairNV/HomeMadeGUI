using System.Numerics;
using Raylib_cs;
namespace HomeMadeGUI;

public class Position
{
    public static Vector2 ScreenSize= new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    public static List<Position> AllPositions = new List<Position>();

    public int X;
    public int Y;
    public Vector2 RelativePosition;
    
    public Position(float relX,float relY)
    {
        AllPositions.Add(this);
        RelativePosition = new Vector2(relX,relY);
        ReDoPosition();
    }

    public static void CheckWindowChange()
    {
        
        if (ScreenSize.X != Raylib.GetScreenWidth() || ScreenSize.Y != Raylib.GetScreenHeight())
        {
            ScreenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            foreach (var pos in AllPositions)
            {
                pos.ReDoPosition();
            }
        }
    }

    public void ReDoPosition()
    {
        X = Convert.ToInt16(RelativePosition.X * ScreenSize.X);
        Y = Convert.ToInt16(RelativePosition.Y * ScreenSize.Y);
    }
}