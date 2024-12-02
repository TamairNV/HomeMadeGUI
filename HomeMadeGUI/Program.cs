


using HomeMadeGUI;
using Raylib_cs;
using System.Numerics;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Custom Font Example");
        Raylib.SetTargetFPS(60);

        // Load the custom font
        Text.InitFonts();
        Console.WriteLine(Environment.CurrentDirectory);
        Text hello = new Text("hellow world", new Vector2(0, 0), centered: false, fontSize:45);
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            hello.DrawText();

            Raylib.EndDrawing();
        }

        // Unload the custom font
        
        Raylib.CloseWindow();
    }
    
}