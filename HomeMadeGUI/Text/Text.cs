using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class Text
{
    public static int MaxFonts = 4;
    public string DisplayText;
    public static Font[] Fonts = new Font[MaxFonts];
    private bool centered;
    public Position Position;
    public int FontSize;
    private Font font;
    

    public Text(string text, Position pos, int fontSize = 20, bool centered = false,int fontIndex = 0)
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
            //Draws texted centered at the position
            DrawTextCentered(new Vector2(Position.X, Position.Y), DisplayText, font, Pallet.PrimaryTextColor);
        }
        else
        {
            //Draws texted starting at the position
            Raylib.DrawTextEx(font,DisplayText, new Vector2(Position.X,Position.Y), FontSize,2, Pallet.PrimaryTextColor);
        }
    }
    
    public static void DrawTextCentered(Vector2 position, string text,Font font,Color color, int fontSize = 50)
    {
        int textWidth = Raylib.MeasureText(text, fontSize);
        
        int xPosition = (int)(position.X - textWidth / 2);
        int yPosition = (int)(position.Y - fontSize / 2);
        Raylib.DrawTextEx(font,text, new Vector2(xPosition,yPosition), fontSize,1, color);
    }

    public static void InitFonts()
    {
        Fonts[0] = Raylib.LoadFontEx("Resources/Fonts/Anonymous.ttf",64,null,0);
        Fonts[1] = Raylib.LoadFontEx("Resources/Fonts/Almo_Andrea_FontlabARROW.ttf",64,null,0);
        Fonts[2] = Raylib.LoadFontEx("Resources/Fonts/Comic Sans MS.ttf",64,null,0);
        Fonts[3] = Raylib.LoadFontEx("Resources/Fonts/Arial.ttf",64,null,0);
        foreach (var font in Fonts)
        {
            Raylib.SetTextureFilter(font.Texture, TextureFilter.Bilinear);
        }
    }
    
}