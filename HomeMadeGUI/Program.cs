


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
        Pallet.InitializeColors();

        

        Text hello = new Text("text", new Position(0.5f, 0.5f), centered: true, fontSize:45);
        
        
        RoundedBox box = new RoundedBox(new Position(0f, 0f), new Bounds(0.1f, 1), Pallet.SecondaryColor);

        TextButton<int, int> textButton =
            new TextButton<int, int>(new Position(0.3f, 0.1f), test, 2, "Hello World",Text.Fonts[3], 30);
        Button<int, int> baseButton = new Button<int, int>(new Position(0.3f, 0.1f), new Bounds(0.05f, 0.05f), test, 2,
            "print 2", Text.Fonts[3], 10);
        void HandleButtons(int xOffset = 0, int yOffset = 0)
        {
            textButton.HandleButton(xOffset,yOffset);
            baseButton.HandleButton(xOffset,yOffset);
        }
        Viewport viewport = new Viewport(new Position(0.25f, 0.25f), new Position(0.5f, 0.5f),HandleButtons);


        TextInput testInput = new TextInput();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Pallet.PrimaryColor);
            box.Draw();
            
            testInput.HandleTextInputs();
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


