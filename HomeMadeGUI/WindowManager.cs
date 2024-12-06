using Raylib_cs;

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
    private RoundedBox codeBox;
    private Viewport codeViewPort;
    private ScrollBar bar;
    private RoundedBox codeLineNums;
    private NodePlacer NodePlacer;
    public Window1()
    {
         //topBox = new Box(new Position(0, 0), new Bounds(1f, 0.12f), Pallet.SecondaryColor);

         
         sideBox = new RoundedBox(new Position(0.03f, 0.11f), new Bounds(0.18f, 0.85f), Pallet.SecondaryColor,roundness:15);
         //mainBox = new RoundedBox(new Position(0.24f, 0.1f), new Bounds(0.5f, 0.87f), Pallet.SecondaryColor,
             //roundness: 15);
             titleBox = new RoundedBox(new Position(0.23f, 0.03f), new Bounds(0.2f, 0.06f), Pallet.SecondaryColor);
             title = new Text("Depth First Search", new Position(0.33f, 0.06f), fontSize: 35,centered:true);
             title.Color = Pallet.AccentColor;
        NodePlacer = new NodePlacer();
          Viewport = new Viewport(new Position(0.23f, 0.11f), new Position(0.40f, 0.85f),NodePlacer.HandlePlacer);
          NodePlacer.viewport = Viewport;
          codeViewPort = new Viewport(new Position(0.67f, 0.0f), new Position(0.6f, 1f), drawCode,noBackground:true,noZoom:true,noMoveBack:true,noMoveForward:true);
          
          //cornerOutline = new Box(new Position(0, 0), new Bounds(0.12f, 0.12f), Pallet.Invisible,outline:true);
          //sideOutline = new Box(new Position(0.12f, 0.12f), new Bounds(1f, 1f), Pallet.Invisible, outline: true,outlineThickness:-3);
          homeButton = new PictureButton<int, int>(new Position(0.05f, 0.05f), OpenHomeWindow,1,"Resources/Icons/houseIcon.png",scale:0.5f );
          codeLineNums = new RoundedBox(new Position(0.65f, 0), new Bounds(0.02f, 1), Pallet.SecondaryColor);
          code = new CodeRep( "Resources/Code/DepthFirstSearch.py" ,new Position(0f,0.01f));
          codeBox = new RoundedBox(new Position(0.67f, 0), new Bounds(0.6f, 1), Pallet.SecondaryColor);
          bar = new ScrollBar(new Position(0.67f, 0.97f), new Bounds(0.31f, 0.01f),defaultValue:0);

    }
    private void viewportDraws(int xOffset = 0, int yOffset = 0)
    {
        
    }

    private void drawCode(int xOffset = 0, int yOffset = 0)
    {
        code.Render(fontSize:21,highlightLine:6);
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
        Viewport.HandleViewport();
        titleBox.Draw();
        //cornerOutline.Draw();
        //sideOutline.Draw();
        //homeButton.HandleButton();
        title.DrawText();
        codeLineNums.Draw();
        codeBox.Draw();
        
        codeViewPort.HandleViewport();
        //float offset = Math.Clamp((Raylib.GetMousePosition().X - code.Position.X)*0.5f,0,200);
        //Console.WriteLine(offset);
        bar.HandelScrollBar();
        
        
        code.xMoveOffset = (int)((bar.Value) * -200);
        code.DrawLineNums(codeViewPort, fontSize:21);
        homeButton.HandleButton();
        

    }
}