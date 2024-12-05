
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

    private List<Word> Words = new List<Word>();

    public CodeRep(string filePath)
    {

        Word lastWord = null;
        char[] wordSplitters = {' ', ']' , '[' , '{' , '}' , ':' , '(' , ')', ':','.',','};
        // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);
                string currentWord = "";
                int i = 1;
                void addWord()
                {
                    Words.Add(new Word(currentWord,lastWord));
                    lastWord = Words[Words.Count-1];
                    
                }
                foreach (string line in lines)
                {
                    bool isComment = false;
                    currentWord = Convert.ToString(i) + "  ";
                    if (currentWord.Length == 3)
                    {
                        currentWord += " ";
                    }
                    i++;
                    addWord();
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
                    Words.Add(new Word("tab",lastWord));
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

    

    public void Render(int startX, int startY, int fontSize = 15)
    {
        int lineHeight = (int)Raylib.MeasureTextEx(Text.Fonts[3], "Y", 20, 2).Y;
        int yOffset = 0;

        int xOffset = startX;
        foreach (var word in Words)
        {
            if (word.ColouredWord == "tab")
            {
                yOffset += lineHeight; // Move the Y offset for the next line
                xOffset = startX;
            }
            else
            {
                if (word.ColouredWord.Length == 1)
                {
                    xOffset += 2;
                }
                Raylib.DrawTextEx(Text.Fonts[3], word.ColouredWord, new Vector2(xOffset, startY + yOffset), fontSize, 2, word.Color);
                xOffset += (int)Raylib.MeasureTextEx(Text.Fonts[3], word.ColouredWord, fontSize, 2).X;
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
        
        else if (ColouredWord == "tab")
        {
            Color = new Color(0, 0, 0, 0);
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

