using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class Button<Tout,Tin>
{
    protected Vector2 Position;
    protected Func<Tin,Tout> Func;
    private Tin input;
    protected Rectangle bounds; 

    public Button()
    {
        
    }


    public void Run()
    {
        Func.Invoke(input);
    }
}