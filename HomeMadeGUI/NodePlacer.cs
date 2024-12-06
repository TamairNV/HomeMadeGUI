using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class NodePlacer
{
    public List<GraphNode> GraphNodes = new List<GraphNode>();
    public Viewport viewport;
    public  HashSet<GraphNode> Visited = new HashSet<GraphNode>();
    public NodePlacer()
    {
        
    }

    public void HandlePlacer(int xOffset = 0, int yOffset = 0)
    {

        Vector2 mousePos = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionRecs(new Rectangle(mousePos, Vector2.One), viewport.viewport))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                bool isOverCurrentNode = false;
                foreach (var node  in GraphNodes)
                {
                    if (node.MouseOverNode())
                    {
                        isOverCurrentNode = true;
                        break;
                    }
                }

                if (!isOverCurrentNode)
                {
                    Vector2 position = Raylib.GetScreenToWorld2D(mousePos, viewport.camera);
                    GraphNode newNode = new GraphNode(position, Convert.ToString(GraphNodes.Count), this);
                    GraphNodes.Add(newNode);
                    Console.WriteLine("make new node");

                }
            }
        }

        foreach (var node in GraphNodes)
        {
            node.DrawLines();
            
  
        }
        foreach (var node in GraphNodes)
        {
         
            node.HandleNode();
        }

    }
    
    
}

public class GraphNode
{
    public Vector2 Position;
    public float Radius;
    public List<GraphNode> Children = new List<GraphNode>();
    public GraphNode Parent;
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
    private DepthFirstSearchStepper DFSsearcher = new DepthFirstSearchStepper();

    public GraphNode(Vector2 position,string name,NodePlacer nodePlacer, float radius = 20, bool isStartNode = false)
    {
        
        Position = position;
        Radius = radius;
        IsStartNode = isStartNode;
        Name = name;
        NodePlacer = nodePlacer;
        
        
        if (NodePlacer.GraphNodes.Count == 0)
        {
            IsStartNode = true;
            DFSsearcher.Initialize(this,NodePlacer);
            
            
        }
        activeColour = Color.Blue;
        searchedColour = Color.Gold;
        defaultColour = Pallet.AccentColor;
        startColour = Color.Pink;

        CurrentColour = defaultColour;
        
        
    }

    private void Draw(int fontSize = 14)
    {

        Raylib.DrawCircle((int)Position.X,(int)Position.Y,Radius,CurrentColour);
        
        
        Raylib.DrawTextEx(Text.Fonts[3],Name,new Vector2(Position.X-fontSize/2 ,Position.Y - Radius/4),fontSize,1,Pallet.PrimaryTextColor);
        
    }

    public void DrawLines()
    {
        foreach (var child in Children)
        {
            Raylib.DrawLineEx(Position,child.Position,2,Color.Black);
            Vector2 direction = Vector2.Normalize(Position - child.Position);
            
            Vector2 CircleInterceptionPoint = child.Position + direction * Radius;
            Vector2 perp1 = new Vector2(-direction.Y, direction.X) * 5 + direction* 10; // Perpendicular to the direction
            Vector2 perp2 = new Vector2(direction.Y, -direction.X) * 5+ direction* 10; // Opposite perpendicular
            
            Raylib.DrawTriangle(
                CircleInterceptionPoint,
                CircleInterceptionPoint + perp1,
                CircleInterceptionPoint + perp2,
                Color.Black
            );


        }
    }

    private bool startAlgor = false;
    private float timer = 0;
    public void HandleNode()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter) && IsStartNode)
        {
            startAlgor = true;
        }

        timer += Raylib.GetFrameTime();
        if (startAlgor && timer > 0.5f)
        {
            DFSsearcher.Step();
            timer = 0;
        }
        if (Searched)
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
        if (placementCooldownTimer < 0.5f)
        {
            placementCooldownTimer += Raylib.GetFrameTime();
        }
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && placementCooldownTimer > 0.5f)
        {
            if (!MouseOverNode())
            {
                return;
            }
            bool isAnotherNodeActive = false;
            GraphNode activeNode = null;
            foreach (GraphNode node in NodePlacer.GraphNodes)
            {
                if (node.Active && node != this)
                {
                    isAnotherNodeActive = true;
                    activeNode = node;
                    
                    
                }
                else
                {
           
                }
            }

            if (isAnotherNodeActive && activeNode != null)
            {
                activeNode.Children.Add(this);
                Active = false;
                Parent = activeNode;
                activeNode.Active = false;
            }
            else
            {
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

    public void DepthFirstSearch(GraphNode start)
    {

        // Mark the node as visited
        if (!NodePlacer.Visited.Contains(start))
        {
            NodePlacer.Visited.Add(start);
            start.Searched = true;
            // Recur for each neighbor
            foreach (var neighbor in start.Children)
            {
                DepthFirstSearch(neighbor);
            }
        }

    }

}



public class DepthFirstSearchStepper
{
    private Stack<GraphNode> _stack = new Stack<GraphNode>();
    private NodePlacer nodePlacer;

    public void Initialize(GraphNode start,NodePlacer nodePlacer)
    {
        _stack.Clear();
        _stack.Push(start);
        this.nodePlacer = nodePlacer;
    }

    public bool Step()
    {
        if (_stack.Count > 0)
        {
            // Pop the next node to process
            var current = _stack.Pop();

            // If it hasn't been visited, mark it and push its neighbors
            if (!nodePlacer.Visited.Contains(current))
            {
                nodePlacer.Visited.Add(current);
                current.Searched = true;

                // Push neighbors to the stack in reverse order
                // to ensure they are processed in the correct order
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    _stack.Push(current.Children[i]);
                }
            }

            return true; // Indicates there are more steps to process
        }

        return false; // All nodes have been processed
    }
}


