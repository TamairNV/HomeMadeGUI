
using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class GraphNode
{
    public Vector2 Position;
    public float Radius;
    public List<(GraphNode,float)> Children = new List<(GraphNode,float)>();
   
    public bool IsStartNode;
    public Color CurrentColour;
    public string Name;
    public bool Active = false;
    public NodePlacer NodePlacer;
    private float placementCooldownTimer = 0;
    private Color activeColour;
    private Color searchedColour;
    private Color defaultColour;
    private Color startColour;
    public bool Searched;

    private GraphSearchStepper searcher;
    
    //Astar Fields
    public int state = 0;
    public float GCost = 0;
    public float HCost = 0;
    public GraphNode Parent;
    public List<GraphNode> Path;
 

    public GraphNode(Vector2 position,string name,NodePlacer nodePlacer, float radius = 10, bool isStartNode = false,string algorithm = "DFS")
    {
        
        Position = position;
        Radius = radius;
        IsStartNode = isStartNode;
        Name = name;
        NodePlacer = nodePlacer;
        
        
        if (NodePlacer.GraphNodes.Count == 0)
        {
            IsStartNode = true;
            searcher = new GraphSearchStepper();
            searcher.Initialize(this,NodePlacer,algorithm:algorithm);
            nodePlacer.startNode = this;


        }
        activeColour = Pallet.SecondaryAccentColor;
        searchedColour = CodeRep.Comments;
        defaultColour = Pallet.AccentColor;
        startColour = CodeRep.MagicMethods;

        CurrentColour = defaultColour;
        
        
    }

    private void Draw(int fontSize = 10)
    {
        float currentRadius = Radius;
        if (NodePlacer.Visited.Count > 0 && NodePlacer.Visited[^ 1] == this)
        {
            currentRadius += 1;
            Raylib.DrawCircle((int)Position.X,(int)Position.Y,currentRadius+1,Color.White);
        }
        else
        {
            Raylib.DrawCircle((int)Position.X,(int)Position.Y,currentRadius+1,Color.Black);
        }
        Raylib.DrawCircle((int)Position.X,(int)Position.Y,currentRadius,CurrentColour);
        
        
        Raylib.DrawTextEx(Text.Fonts[3],Name,new Vector2(Position.X-fontSize/2 ,Position.Y - Radius/4),fontSize,1,Pallet.PrimaryTextColor);
        
    }

    public void DrawLines()
    {
        foreach (var child in Children)
        {
            Color color = Color.Black;
            if (child.Item1.state == 4 && (state == 4 || IsStartNode))
            {
                color = Color.Gold;
            }
            Raylib.DrawLineEx(Position,child.Item1.Position,2,color);
            Vector2 direction = Vector2.Normalize(Position - child.Item1.Position);
            
            Vector2 CircleInterceptionPoint = child.Item1.Position + direction * Radius;
            Vector2 perp1 = new Vector2(-direction.Y, direction.X) * 5 + direction* 10; // Perpendicular to the direction
            Vector2 perp2 = new Vector2(direction.Y, -direction.X) * 5+ direction* 10; // Opposite perpendicular
            float distance = Math.Abs(Vector2.Distance(Position, child.Item1.Position));

            Raylib.DrawTriangle(
                CircleInterceptionPoint,
                CircleInterceptionPoint + perp1,
                CircleInterceptionPoint + perp2,
                color
            );
            Raylib.DrawTextEx(Text.Fonts[3],Math.Round(distance/10,1).ToString(),-(direction * distance/2) + Position,10,0,Color.White);



        }
    }

    private bool startAlgor = false;
    private float timer = 0;

    public void HandleAlgor(float value)
    {
        timer += Raylib.GetFrameTime();

        if (value > 0.5f && timer > 2* (1-value))
        {
            if (searcher.algorithm != "A*" || !searcher.algorDone)
            {
                searcher.Step();
            }
            timer = 0;
        }

        if (value < 0.5f  && timer > 2* value)
        {
            searcher.ReverseStep();
            timer = 0;
            
        }
        
        

        
        
    }
    public void HandleNode()
    {

        if (NodePlacer.startNode.searcher.endnode == this)
        {
            CurrentColour = Color.Red;
        }
        else if (state == 4)
        {
            
            CurrentColour = Color.DarkBlue;
        }
        else if (state == 1)
        {
            CurrentColour = Color.Magenta;
        }

        else if (state == 2)
        {
            CurrentColour = Color.Blue;
        }
        else if (Searched)
        {
            CurrentColour = searchedColour;
        }
        else if (Active)
        {
            CurrentColour = activeColour;
        }
        else if (IsStartNode)
        {
            CurrentColour = startColour;
        }
        else
        {
            CurrentColour = defaultColour;
        }
        
        Draw();
        if (placementCooldownTimer < 0.15f)
        {
            placementCooldownTimer += Raylib.GetFrameTime();
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && placementCooldownTimer > 0.15f)
        {
            if (!MouseOverNode())
            {
                return;
            }
            bool isAnotherNodeActive = false;
            GraphNode activeNode = null;
            bool nodeDeactivated = false;
            foreach (GraphNode node in NodePlacer.GraphNodes)
            {
                if (node.Active)
                {
                    if (node != this)
                    {
                        isAnotherNodeActive = true;
                        activeNode = node;
                    }
                    else
                    {
                        Active = false;
                        nodeDeactivated = true;
                    }

                }
            }

            if (isAnotherNodeActive && activeNode != null)
            {
                activeNode.Children.Add((this,Math.Abs(Vector2.Distance(Position, activeNode.Position))));
                Active = false;
                Parent = activeNode;
                activeNode.Active = false;
            }
            else if(!nodeDeactivated)
            {
                if (Raylib.IsKeyDown(KeyboardKey.F))
                {
                    NodePlacer.startNode.searcher.endnode = this;
                    
                }
                Active = true;
            }
            
        }
        
        
    }

    public bool MouseOverNode()
    {
        Vector2 mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), NodePlacer.viewport.camera);
        
        float diff = Math.Abs(Vector2.Distance(mousePos, Position));
        return diff <= Radius;

    }



}

