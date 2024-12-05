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
  
    private Action<int,int> drawMethod;
    public Viewport(Position position,Position size, Action<int,int> drawMethod ,int cellAmount = 40)
    {
        this.drawMethod = drawMethod;
        float thickness = 0.0035f;

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

    }

    
    public void HandleViewport()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        
        Rectangle mouseRect = new Rectangle(mousePos, 1, 1);
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) )
        {
            isMouseOverViewport = Raylib.CheckCollisionRecs(mouseRect, viewport);
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            isMouseOverViewport = Raylib.CheckCollisionRecs(mouseRect, viewport);
        }
        viewport = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
        camera.Offset = new Vector2(Position.X, Position.Y);
        ResetZoom();
        if (isMouseOverViewport) HandleMouseDragging();
        
        if(Raylib.CheckCollisionRecs(mouseRect, viewport)) HandleZoom();
        
        


        

        // Set clipping area for objects
        Raylib.BeginScissorMode((int)viewport.X, (int)viewport.Y, (int)viewport.Width, (int)viewport.Height);
        // Use the camera to handle movement inside the viewport
        Raylib.BeginMode2D(camera);

        // Draw draggable objects
        DrawBackground();

        drawMethod((int)camera.Target.X - Position.X, (int)camera.Target.Y - Position.Y);

        Raylib.EndMode2D();
        Raylib.EndScissorMode();
        // Draw the viewport boundap
        Raylib.DrawRectangleRoundedLines(new Rectangle(viewport.X , viewport.Y , viewport.Width , viewport.Height ),0.05f,10,10,Pallet.PrimaryColor);



    }

    private void ResetZoom()
    {
        float maxZoom = viewport.Width/500;
        if (camera.Zoom < maxZoom) camera.Zoom = maxZoom;
        if (camera.Zoom > 3.0f) camera.Zoom = 3.0f;
    }
    private void HandleMouseDragging()
    {
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
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

    private void DrawBackground()
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

    
}