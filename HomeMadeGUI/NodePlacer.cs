using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class NodePlacer
{
    public List<GraphNode> GraphNodes = new List<GraphNode>();
    public Viewport viewport;
    public List<GraphNode> Visited = new List<GraphNode>();
    public float value = 0.5f;
    public GraphNode startNode;
    public string algorithm;
    public NodePlacer(string algorithm)
    {
        this.algorithm = algorithm;
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
                    GraphNode newNode = new GraphNode(position, Convert.ToString(GraphNodes.Count), this,algorithm:algorithm);
                    GraphNodes.Add(newNode);

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
            if (node == startNode)
            {
                node.HandleAlgor(value);
            }
            
        }

    }
    
    
}



