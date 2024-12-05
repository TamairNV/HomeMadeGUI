class Node:
    def __init__(self, name):
        self.name = name
        self.neighbors = [] # List of neighboring nodes.

    def add_neighbor(self, neighbor):
        self.neighbors.append(neighbor)


def depth_first_search(start):
    # Basic Depth First Search (DFS) Algorithm using Node objects.
    visited = [] # List to keep track of visited nodes.
    stack = [start] # Stack to manage the nodes to visit.

    while stack:
        current = stack.pop() # Get the last node from the stack.

        if current not in visited:
            visited.append(current) # Mark it as visited.

            # Add all unvisited neighbors to the stack.
            for neighbor in current.neighbors:
                if neighbor not in visited:
                    stack.append(neighbor)

    return [node.name for node in visited]  # Return the names of visited nodes.


