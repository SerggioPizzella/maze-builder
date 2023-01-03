using Assets.Scripts;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

// MazeGenerator generates a maze using the depth-first search algorithm.
public class MazeGenerator
{
    // Width and height of the maze.
    private int width;
    private int height;

    // Grid of cells representing the maze.
    private Cell[,] grid;

    public MazeGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Cell[width, height];

        // Initialize each cell in the grid.
        for (int i = 0; i < this.height; i++)
        {
            for (int j = 0; j < this.width; j++)
            {
                grid[j, i] = new Cell(j, i);
            }
        }
    }

    // GenerateMaze generates a maze using the depth-first search algorithm.
    // It returns a list of transitions representing the paths taken to generate the maze.
    public List<(Position, Position)> GenerateMaze()
    {
        // List of transitions representing the paths taken to generate the maze.
        List<(Position, Position)> transitions = new List<(Position, Position)>();

        // Stack of cells representing the path taken to generate the maze.
        Stack<Cell> cellStack = new Stack<Cell>();

        // Start the maze generation from the top-left cell.
        Cell start = grid[0, 0];

        // Push the start cell onto the stack and mark it as visited.
        cellStack.Push(start);
        start.Visited = true;

        // While there are cells in the stack, continue generating the maze.
        while (cellStack.Count > 0)
        {
            Cell currentCell = cellStack.Peek();
            Cell[] neighbours = GetUnvisitedNeighbours(currentCell);

            // If there are no unvisited neighbours, pop the current cell from the stack.
            // Otherwise, choose a random unvisited neighbour and add it to the maze.
            if (neighbours.Length == 0)
            {
                cellStack.Pop();
                continue;
            }

            int index = Random.Range(0, neighbours.Length);
            Cell nextCell = neighbours[index];

            transitions.Add((currentCell.Position, nextCell.Position));
            cellStack.Push(nextCell);
            nextCell.Visited = true;
        }

        return transitions;
    }

    private Cell[] GetUnvisitedNeighbours(Cell currentCell)
    {
        // Offsets for the x- and y-coordinates to get the neighbours of the current cell.
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // List of unvisited neighbours.
        List<Cell> cells = new List<Cell>();

        // Check the neighbours of the current cell.
        for (int i = 0; i < 4; i++)
        {
            // Calculate the x- and y-coordinates of the neighbour.
            int x = currentCell.Position.x + dx[i];
            int y = currentCell.Position.y + dy[i];

            // If the neighbour is within the bounds of the grid, add it to the list of unvisited neighbours.
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                Cell cell = grid[x, y];
                if (!cell.Visited)
                {
                    cells.Add(cell);
                }
            }
        }

        return cells.ToArray();
    }
}
