namespace HomeMadeGUI;

public class WindowManager
{
    
}

public class Window1
{
    public Box topBox;
    public RoundedBox sideBox;
    public Viewport Viewport;
    private Box cornerOutline;
    private Box sideOutline;
    private Button<int, int> homeButton;
    private Text title;
    private RoundedBox titleBox;

    private RoundedBox mainBox;
    private CodeRep code;
    public Window1()
    {
         //topBox = new Box(new Position(0, 0), new Bounds(1f, 0.12f), Pallet.SecondaryColor);
         
         
         sideBox = new RoundedBox(new Position(0.03f, 0.11f), new Bounds(0.18f, 0.85f), Pallet.SecondaryColor,roundness:15);
         //mainBox = new RoundedBox(new Position(0.24f, 0.1f), new Bounds(0.5f, 0.87f), Pallet.SecondaryColor,
             //roundness: 15);
             titleBox = new RoundedBox(new Position(0.25f, 0.03f), new Bounds(0.2f, 0.06f), Pallet.SecondaryColor);
             title = new Text("Depth First Search", new Position(0.35f, 0.06f), fontSize: 35,centered:true);
          Viewport = new Viewport(new Position(0.25f, 0.11f), new Position(0.48f, 0.85f),viewportDraws);
          //cornerOutline = new Box(new Position(0, 0), new Bounds(0.12f, 0.12f), Pallet.Invisible,outline:true);
          //sideOutline = new Box(new Position(0.12f, 0.12f), new Bounds(1f, 1f), Pallet.Invisible, outline: true,outlineThickness:-3);
          //homeButton =
              new Button<int, int>(new Position(0.015f, 0.03f),new Bounds(0.06f,0.06f), OpenHomeWindow, 1, " ", Text.Fonts[3], 25,square:true);

              code = new CodeRep("Resources/Code/DepthFirstSearch.py");
              
         
         
    }

    private void viewportDraws(int xOffset = 0, int yOffset = 0)
    {
        
    }

    private int OpenHomeWindow(int i)
    {
        Console.WriteLine("Go Home");
        return i;
    }
    
    public void Draw()
    {
        //topBox.Draw();
        sideBox.Draw();
        //mainBox.Draw();
        //Viewport.HandleViewport();
        titleBox.Draw();
        //cornerOutline.Draw();
        //sideOutline.Draw();
        //homeButton.HandleButton();
        title.DrawText();
        code.Render(400,100);
    }
}