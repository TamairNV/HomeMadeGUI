
using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class CodeRep
{
    public static Color Keywords;
    public static Color Strings;
    public static Color Comments;
    public static Color Numbers;
    public static Color Methods;
    public static Color Variables;
    public static Color Operators;
    public static Color Classes;
    public static Color MagicMethods;
    public int xMoveOffset = 0;

    public Position Position;

    private List<Word> Words = new List<Word>();
    private int lineNumbers = 0;

    public CodeRep(string filePath,Position position)
    {
        Position = position;
        Word lastWord = null;
        char[] wordSplitters = {' ', ']' , '[' , '{' , '}' , ':' , '(' , ')', ':','.',','};
        // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);
                string currentWord = "";
             
                void addWord()
                {
                    Words.Add(new Word(currentWord,lastWord));
                    lastWord = Words[Words.Count-1];
                    
                }
                foreach (string line in lines)
                {
                    lineNumbers += 1;
                    bool isComment = false;
                    
                    
                    currentWord = "";
                    foreach (char letter in line)
                    {
                        if (letter == '#') { isComment = true; }
                        if (isComment)
                        {
                            currentWord += letter;
                            continue;
                        }
                        if (wordSplitters.Contains(letter))
                        {
                            if (currentWord != "") { addWord(); }

                            Words.Add(new Word(Convert.ToString(letter),lastWord));
                            lastWord = Words[Words.Count-1];
                            
                            currentWord = "";
                        }
                        else
                        {
                            currentWord += letter;
                        }
                    }
                    if (isComment)
                    {
                        addWord();
                    }
                    else if (currentWord != "")
                    {
                        addWord();
                    }
                    Words.Add(new Word("   ",lastWord));
                    lastWord = Words[Words.Count-1];
                }
            }
            else
            {
                Console.WriteLine($"The file '{filePath}' does not exist.");
            }
    }

    public static void InitCodeColours()
    {
        Keywords = new Color(106,139,212,255);
        Strings = new Color(206, 145, 120, 255);
        Comments = new Color(0, 128, 0, 255);
        Numbers = new Color(130, 160, 160, 190);
        Methods = new Color(149, 117, 188, 255);
        Variables = new Color(156, 220, 254, 255);
        Classes = new Color(158, 206, 106, 255);
        MagicMethods = new Color(159,20,161,255);
    }

    public void DrawLineNums(Viewport viewport,int fontSize = 15)
    {
        Vector2 textSize = Raylib.MeasureTextEx(Text.Fonts[3], "n", fontSize, 2);
        int lineHeight = (int)textSize.Y;
        int yOffset = 0;
        for (int i = 1; i < lineNumbers; i++)
        {
            Raylib.DrawTextEx(Text.Fonts[3], Convert.ToString(i), new Vector2(Position.X+viewport.Position.X-textSize.X*2.5f,  Position.Y + yOffset+viewport.Position.Y), fontSize, 0,
                Numbers);
            yOffset += lineHeight;
        }
    }
    public void Render(int fontSize = 15, int highlightLine = 0)
    {
        int lineHeight = (int)Raylib.MeasureTextEx(Text.Fonts[3], "n", fontSize, 2).Y;
        int yOffset = 0;
        int lineNum =1;
        int xOffset = Position.X;
        foreach (var word in Words)
        {
            int moveOffset = xMoveOffset;
            if (word.ColouredWord == "   ")
            {
                yOffset += lineHeight; // Move the Y offset for the next line
                xOffset = Position.X;
                lineNum += 1;
            }
            else
            {
                if (lineNum == highlightLine && word.Parent != null && word.Parent.ColouredWord != "   ")
                {
                    Raylib.DrawRectangleRounded(new Rectangle(Position.X,yOffset + Position.Y,5000,lineHeight),0.5f,15,new Color(20,20,80,10));
                    
                }
                if(word.Color.Equals(Numbers))
                {
                    moveOffset = 0;
                }
                if (word.ColouredWord.Length == 1)
                {
                    xOffset += 2;
                }
                

                Raylib.DrawTextEx(Text.Fonts[3], word.ColouredWord, new Vector2(xOffset + moveOffset,  Position.Y + yOffset), fontSize, 0,
                    word.Color);
                xOffset += (int)Raylib.MeasureTextEx(Text.Fonts[3], word.ColouredWord, fontSize, 0).X;
            }

        }
    }
}

class Word
{
    
    public static  string[] Keywords = { "def", "if", "else", "elif", "while", "for", "return", "import", "from", "as", "try", "except", "finally", "with", "lambda", "pass", "break", "continue","in", "not" };
    public static string[] Functions = { "append", "pop" };
    public string ColouredWord;
    public Color Color;
    public Word Parent;
    
    public Word(string word,Word parent)
    {
        Parent = parent;
        ColouredWord = word;
        if (ColouredWord[0] == '#')
        {
            Color = CodeRep.Comments;
        }
        
        else if (char.IsDigit(ColouredWord[0]))
        {
            Color = CodeRep.Numbers;
        }
        
        else if (ColouredWord == "   ")
        {
            Color = new Color(0, 0, 0, 255);
        }
        else if (ColouredWord[0] == '_' && ColouredWord[1] == '_')
        {
            Color = CodeRep.MagicMethods;
        }
        else if (Parent != null && Parent.Parent != null &&Parent.Parent.ColouredWord == "def" )
        {
            Color = CodeRep.Methods;
        }
        else if (ColouredWord == "self")
        {
            Color = new Color(144, 90, 139, 255);
        }
        
        else if (Keywords.Contains(ColouredWord))
        {
            Color = CodeRep.Keywords;
        }
        else if (Functions.Contains(ColouredWord))
        {
            Color = CodeRep.Methods;
        }
        else if (ColouredWord == "class")
        {
            Color = CodeRep.Classes;
        }
        else if (ColouredWord == " " )
        {
            Color = CodeRep.Variables;
            ColouredWord += " ";
        }
        else if (ColouredWord.Length > 1)
        {
            Color = CodeRep.Variables;
        }
        
        else if (ColouredWord[0] == '"')
        {
            Color = CodeRep.Strings;
        }

        else if (ColouredWord.Length == 1)
        {
            Color = Color.White;
        }
   
    }
}

