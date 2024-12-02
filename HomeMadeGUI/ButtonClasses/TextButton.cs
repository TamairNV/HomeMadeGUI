namespace HomeMadeGUI;
using System.Numerics;
using Raylib_cs;
public class TextButton<Tout,Tin> : Button<Tout,Tin>
{
    private Color textColor = Pallet.Colour[0];
    private Color hoverColor = Pallet.Colour[3];
    private Color pressedColor = Pallet.Colour[6];
    private Color currentColor;
    private Text Text;
    private Font font;
   
    
    public TextButton(Position position,Func<Tin,Tout> func,Tin input,string defaultStr,Font font,int size)
    {
        Position = position;
        Func = func;
        this.input = input;
        currentColor = textColor;
        this.size = size;
        this.font = font;
        this.defaultStr = defaultStr;
    }

    public override void Draw()
    {
        Text.DrawTextCentered(new Vector2(Position.X, Position.Y), defaultStr, font, currentColor, fontSize:size);
    }
    
    public override void MouseHover()
    {
        currentColor = hoverColor;
    }
    public override void Idle()
    {
        currentColor = textColor;
    }

    public override void MousePress()
    {
        
        currentColor = pressedColor;
    }

    public override void MouseRelease()
    {
        Run();
        currentColor = textColor;
    }
}