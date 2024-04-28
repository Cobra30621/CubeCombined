using UnityEngine;

public class Map
{
    public int[,] grid;
    public int rows;
    public int cols;

    public Map(int numRows, int numCols)
    {
        rows = numRows;
        cols = numCols;
        grid = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = 0;
            }
        }
    }

    public Map(Map gameMap)
    {
        rows = gameMap.rows;
        cols = gameMap.cols;
        grid = (int[,]) gameMap.grid.Clone();
    }

    public void PrintMap()
    {
        string mapString = "";
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                mapString += grid[i, j] + " ";
            }
            mapString += "\n";
        }
        Debug.Log(mapString);
    }

    public void AddBlock(int column, int value)
    {
        if (column < 0 || column >= cols)
        {
            Debug.LogError("Invalid column number.");
            return;
        }

        for (int i = 0; i < rows; i++)
        {
            if (grid[i, column] == 0)
            {
                grid[i, column] = value;
                break;
            }
        }
    }

    public int[,] GetGrid()
    {
        return grid;
    }
}