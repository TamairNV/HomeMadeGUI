


using HomeMadeGUI;
using Raylib_cs;
using System.Numerics;

class Program
{
    static void Main()
    {
     
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(800, 600, "Custom Font Example");
        Raylib.SetTargetFPS(60);
        // Load the custom font
        Text.InitFonts();
        
        
        
        Text hello = new Text("text", new Position(0.5f, 0.5f), centered: true, fontSize:45);
        
        
        RoundedBox box = new RoundedBox(new Position(0f, 0f), new Bounds(0.1f, 1), new Color(10,20,255,128));

        TextButton<int, int> textButton =
            new TextButton<int, int>(new Position(0.5f, 0.1f), test, 2, "Hello World",Text.Fonts[3], 30);

        Viewport viewport = new Viewport(new Position(0.25f, 0.25f), new Position(0.5f, 0.5f));

        Button<int, int> baseButton = new Button<int, int>(new Position(0.8f, 0.1f), new Bounds(0.05f, 0.05f), test, 2,
            "print 2", Text.Fonts[3], 10);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            box.Draw();
            textButton.HandleButton();
            baseButton.HandleButton();
            //textButton.Draw();
            viewport.HandleViewport();
            Position.CheckWindowChange();
            Bounds.CheckWindowChange();
            hello.DrawText();

            Raylib.EndDrawing();
        }

        // Unload the custom font
        
        Raylib.CloseWindow();
    }


    public static int test(int num)
    {
        Console.WriteLine(num);
        return 0;
    }

    
    
}


