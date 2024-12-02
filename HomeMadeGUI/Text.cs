using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class Text
{
    public static int MaxFonts = 3;
    public string DisplayText;
    public static Font[] Fonts = new Font[MaxFonts];
    private bool centered;
    public Vector2 Position;
    public int FontSize;
    private Font font;
    

    public Text(string text, Vector2 pos, int fontSize = 20, bool centered = false,int fontIndex = 0)
    {
        DisplayText = text;
        this.centered = centered;
        Position = pos;
        FontSize = fontSize;
        font = Fonts[fontIndex];
    }

    public void DrawText()
    {
        if (centered)
        {
            int textWidth = Raylib.MeasureText(DisplayText, FontSize);

            int xPosition = (int)(Position.X - textWidth / 2);
            int yPosition = (int)(Position.Y - FontSize / 2);
            Raylib.DrawText(DisplayText, xPosition, yPosition, FontSize, Color.Black);
        }
        else
        {
            Raylib.DrawTextEx(font,DisplayText, Position, FontSize,2, Color.Black);
        }
    }
    
    public static void DrawTextCentered(Rectangle rect, string text,int fontSize = 20)
    {
        int textWidth = Raylib.MeasureText(text, fontSize);
        int textHeight = fontSize;  
        
        int xPosition = (int)(rect.X + (rect.Width - textWidth) / 2);
        int yPosition = (int)(rect.Y + (rect.Height - textHeight) / 2);

       
        Raylib.DrawText(text, xPosition, yPosition, fontSize, Color.Black);
    }

    public static void InitFonts()
    {
        Fonts[0] = Raylib.LoadFontEx("Resources/Fonts/rm_almanack.ttf",256,null,0);
        Fonts[1] = Raylib.LoadFontEx("Resources/Fonts/Almo_Andrea_FontlabARROW.ttf",256,null,0);
        Fonts[1] = Raylib.LoadFontEx("Resources/Fonts/Comic Sans MS.ttf",256,null,0);

    }
    
}