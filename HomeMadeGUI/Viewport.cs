using Raylib_cs;

namespace HomeMadeGUI;
using System.Numerics;
public class Viewport
{
    public Position Position;
    public Position Size;
    private Camera2D camera = new Camera2D();
    private Rectangle viewport;
    private Rectangle[] rectangles;
    private bool isMouseOverViewport = false;
    private Texture2D backgroundTex;
    private Vector2 texOrigin;
    private Rectangle texSourceRec;
    private RoundedBox outline;
    public Viewport(Position position,Position size, int cellAmount = 40)
    {
        float thickness = 0.0035f;
        outline = new RoundedBox(new Position(position.RelativePosition.X - thickness, position.RelativePosition.Y - thickness),
            new Bounds(size.RelativePosition.X + thickness*2, size.RelativePosition.Y + thickness*2),
            Pallet.Colour[4]);
        texOrigin = new Vector2(1000, 1000); 
        backgroundTex = Raylib.LoadTexture("Resources/checks.png");
        texSourceRec = new Rectangle( 0.0f, 0.0f, backgroundTex.Width * cellAmount, backgroundTex.Height * cellAmount );
        
        // Set texture wrapping to REPEAT
        Raylib.SetTextureWrap(backgroundTex, TextureWrap.Repeat);

        // Set texture filtering for better visual quality
        Raylib.SetTextureFilter(backgroundTex, TextureFilter.Point);

        
        Position = position;
        Size = size;
        camera.Target = new Vector2(0, 0);
        camera.Offset = new Vector2(400, 300);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f ;
        rectangles = new [] {
            new Rectangle(25, 30, 100, 100),
            new Rectangle(300, 200, 76, 10),
            new Rectangle(150, 150, 50, 30)
        };
    }

    
    public void HandleViewport()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        
        Rectangle mouseRect = new Rectangle(mousePos, 1, 1);
        if (Raylib.IsMouseButtonPressed(MouseButton.Middle) )
        {
            isMouseOverViewport = Raylib.CheckCollisionRecs(mouseRect, viewport);
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Middle))
        {
            isMouseOverViewport = Raylib.CheckCollisionRecs(mouseRect, viewport);
        }
        viewport = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
        camera.Offset = new Vector2(Position.X, Position.Y);
        ResetZoom();
        if (isMouseOverViewport) HandleMouseDragging();
        
        if(Raylib.CheckCollisionRecs(mouseRect, viewport)) HandleZoom();
        outline.Draw();
        


        
        // Draw the viewport boundary
        Raylib.DrawRectangleLinesEx(viewport, 2, Color.Black);

        // Set clipping area for objects
        Raylib.BeginScissorMode((int)viewport.X, (int)viewport.Y, (int)viewport.Width, (int)viewport.Height);

        // Use the camera to handle movement inside the viewport
        Raylib.BeginMode2D(camera);

        // Draw draggable objects
        DrawBackground(25, Color.Black, Color.DarkGray);

        foreach (Rectangle rect in rectangles)
        {
            Raylib.DrawRectangleRec(rect, Color.Red);
        }

        Raylib.EndMode2D();
        Raylib.EndScissorMode();

    }

    private void ResetZoom()
    {
        float maxZoom = viewport.Width/500;
        if (camera.Zoom < maxZoom) camera.Zoom = maxZoom;
        if (camera.Zoom > 3.0f) camera.Zoom = 3.0f;
    }
    private void HandleMouseDragging()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Middle))
        {
            Vector2 delta = Raylib.GetMouseDelta();
            camera.Target.X -= delta.X / camera.Zoom;
            camera.Target.Y -= delta.Y / camera.Zoom;
        }
        
        if (camera.Target.X < -250) camera.Target.X = -250;
        if (camera.Target.X > 250) camera.Target.X = 250;
        if (camera.Target.Y < -250) camera.Target.Y = -250;
        if (camera.Target.Y > 250) camera.Target.Y = 250;
     
    }

    private void DrawBackground(int tileSize, Color colour1, Color colour2)
    {
        
        Rectangle destRec = new Rectangle((int)750 ,(int)750, 1000,  1000 ); // Destination rectangle
        

        Raylib.DrawTexturePro(
            backgroundTex,
            texSourceRec, // Source rectangle with larger UVs for repetition
            destRec,   // Destination rectangle on the screen
            texOrigin,    // Origin of the texture
            0.0f,      
            Color.White      
        );
    }

    private void HandleZoom()
    {
        // Handle zoom
        float wheel = Raylib.GetMouseWheelMove();

        {
            // Get mouse position in screen space
            Vector2 mouseScreenPos = Raylib.GetMousePosition();

            // Convert mouse position to world space
            Vector2 mouseWorldPosBeforeZoom = Raylib.GetScreenToWorld2D(mouseScreenPos, camera);

            // Adjust zoom level
            camera.Zoom += wheel * 0.1f;
            ResetZoom();
            

            // Convert mouse position back to world space after zoom
            Vector2 mouseWorldPosAfterZoom = Raylib.GetScreenToWorld2D(mouseScreenPos, camera);

            // Adjust camera target to keep the mouse's world position consistent
            camera.Target.X += mouseWorldPosBeforeZoom.X - mouseWorldPosAfterZoom.X;
            camera.Target.Y += mouseWorldPosBeforeZoom.Y - mouseWorldPosAfterZoom.Y;
        }
    }


    private void DrawBoundary(int thickness ,Color Colour, int Roundness = 10 )
    {
        
    }
}