using System.Numerics;
using Raylib_cs;

namespace HomeMadeGUI;

public class GraphSearchStepper
{
    private Stack<GraphNode> stack = new Stack<GraphNode>();
    private Queue<GraphNode> queue = new Queue<GraphNode>();
    private PriorityQueue<GraphNode, float> priorityQueue = new PriorityQueue<GraphNode, float>();

    private NodePlacer nodePlacer = null;
    public bool algorDone = false;

    public string algorithm;

    //AStar fields
    public GraphNode endnode = null;

    public void Initialize(GraphNode start, NodePlacer nodePlacer, string algorithm = "DFS")
    {
        this.algorithm = algorithm;
        if (algorithm == "DFS")
        {
            stack.Clear();
            stack.Push(start);
        }
        else if (algorithm == "BFS")
        {
            queue.Clear();
            queue.Enqueue(start);
        }
        else if (algorithm == "A*")
        {
            priorityQueue.Clear();
            priorityQueue.Enqueue(start, 100);
        }

        this.nodePlacer = nodePlacer;
        nodePlacer.Visited = new List<GraphNode>();
    }
    

    public void Step()
    {
        if (algorithm == "DFS")
        {
            DepthFirstSearchStep();
            return;
        }

        if (algorithm == "BFS")
        {
            BreadthFirstSearchStep();
            return;
        }

        if (algorithm == "A*")
        {
            AStarStep();
            return;
        }
        
    }

    public void ReverseStep()
    {
        if (algorithm == "DFS" || algorithm == "BFS")
        {
            if (nodePlacer == null || nodePlacer.Visited.Count <= 0)
            {
                return;
            }

            GraphNode lastNode = nodePlacer.Visited[^1];
            nodePlacer.Visited.RemoveAt(nodePlacer.Visited.Count - 1);

            if (algorithm == "DSF")
            {
                stack.Push(lastNode);
            }
            else
            {
                List<GraphNode> tempList = queue.ToList();
                tempList.Insert(0, lastNode);
                queue = new Queue<GraphNode>(tempList);
            }

            lastNode.Searched = false;
        }
        else
        {
            AStarStepBack();
        }

    }

    private void DepthFirstSearchStep()
    {
        if (stack.Count > 0)
        {
            // Pop the next node to process
            var current = stack.Pop();

            // If it hasn't been visited, mark it and push its neighbors
            if (!nodePlacer.Visited.Contains(current))
            {
                nodePlacer.Visited.Add(current);
                current.Searched = true;

                // Push neighbors to the stack in reverse order
                // to ensure they are processed in the correct order
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(current.Children[i].Item1);
                }
            }

            return;
        }

    }
    private void BreadthFirstSearchStep()
    {
        if (queue.Count > 0)
        {
            // Pop the next node to process
            var current = queue.Dequeue();

            // If it hasn't been visited, mark it and push its neighbors
            if (!nodePlacer.Visited.Contains(current))
            {
                nodePlacer.Visited.Add(current);
                current.Searched = true;

                // Push neighbors to the stack in reverse order
                // to ensure they are processed in the correct order
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    queue.Enqueue(current.Children[i].Item1);
                }
            }

            return; // Indicates there are more steps to process
        }

        return; // All nodes have been processed
    }

    private void AstarGetPath(GraphNode endnode)
    {
        GraphNode currentNode = endnode;

        while (currentNode.Parent != null)
        {
            currentNode.state = 4;

            currentNode = currentNode.Parent;
        }
    }

    private void AStarStepBack()
    {
        endnode.state = 0;
        if (nodePlacer.Visited.Count == 0)
        {
            return;
        }

        nodePlacer.Visited[^1].state = 0;
        nodePlacer.Visited.RemoveAt(nodePlacer.Visited.Count - 1);
        foreach (var node in priorityQueue.UnorderedItems)
        {
            node.Element.state = 0;
        }

        priorityQueue = new PriorityQueue<GraphNode, float>();
        List<GraphNode> visited = new List<GraphNode>(nodePlacer.Visited);
        foreach (var node in visited)
        {
            AStarCheckVisit(node);
        }

        return;
    }

    private void AStarStep()
    {
        if (endnode.state == 4)
        {
            return;
        }

        if (priorityQueue.Count == 0)
        {
            priorityQueue.Enqueue(nodePlacer.startNode, 0);
            Console.WriteLine("more burgers");
        }

        GraphNode currentNode = priorityQueue.Dequeue();
        nodePlacer.Visited.Add(currentNode);
        currentNode.state = 2;
        if (currentNode == endnode)
        {
            AstarGetPath(currentNode);
            return;
        }
        
        AStarCheckVisit(currentNode);
    }

    private void AStarCheckVisit(GraphNode currentNode)
    {
        foreach (var child in currentNode.Children)
        {
            if (child.Item1.state == 0)
            {
                child.Item1.Parent = currentNode;
                child.Item1.GCost = currentNode.GCost + child.Item2;
                child.Item1.HCost = Math.Abs(Vector2.Distance(child.Item1.Position, endnode.Position));
                priorityQueue.Enqueue(child.Item1, child.Item1.GCost + child.Item1.HCost);

                child.Item1.state = 1;
            }
            else if (child.Item1.state == 1 && child.Item1.Parent.GCost >= currentNode.GCost)
            {
                child.Item1.Parent = currentNode;
                child.Item1.GCost = currentNode.GCost + child.Item2;
            }
        }
    }
}