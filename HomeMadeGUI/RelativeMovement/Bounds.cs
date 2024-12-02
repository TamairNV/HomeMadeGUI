namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;

public class Bounds
{
    public static Vector2 ScreenSize=new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    public static List<Bounds> AllBounds = new List<Bounds>();
    
    public int X;
    public int Y;
    public Vector2 RelativeBounds;
    
    public Bounds(float relWidth,float relHeight)
    {
        AllBounds.Add(this);
        RelativeBounds = new Vector2(relWidth,relHeight);
        ReDoBounds();
    }

    public static void CheckWindowChange()
    {
        
        if (ScreenSize.X != Raylib.GetScreenWidth() || ScreenSize.Y != Raylib.GetScreenHeight())
        {
            ScreenSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            foreach (var pos in AllBounds)
            {
                pos.ReDoBounds();
            }
        }
    }

    public void ReDoBounds()
    {
        X = Convert.ToInt16(RelativeBounds.X * ScreenSize.X);
        Y = Convert.ToInt16(RelativeBounds.Y * ScreenSize.Y);
    }
}