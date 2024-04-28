using System.Collections.Generic;
using UnityEngine;

public class BlockHandler
{
    private List<Map> mergedMaps;
    private Map gameMap;

    public BlockHandler(int numRows, int numCols)
    {
        mergedMaps = new List<Map>();
        gameMap = new Map(numRows, numCols);
    }

    public void PrintMergedMaps()
    {
        Debug.Log(mergedMaps.Count);
        foreach (var map in mergedMaps)
        {
            map.PrintMap();
        }
    }

    public void PrintMap()
    {
        gameMap.PrintMap();
    }

    public void AddBlock(int column, int value)
    {
        gameMap.AddBlock(column, value);
        mergedMaps.Clear();
        mergedMaps.Add(new Map(gameMap)); // 新增一個 Map 的複本到 mergedMaps
        CheckAndMergeBlocks(gameMap);

    }

    public List<Map> GetMergedMaps()
    {
        return mergedMaps;
    }

    private void CheckAndMergeBlocks(Map map)
    {
        bool merged = false;
        int[,] grid = map.GetGrid();

        do
        {
            merged = false;

            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        // 向上檢查合併
                        if (i > 0 && grid[i, j] == grid[i - 1, j])
                        {
                            grid[i, j] += 1;
                            grid[i - 1, j] = 0;
                            merged = true;
                        }
                        // 向下檢查合併
                        else if (i < map.rows - 1 && grid[i, j] == grid[i + 1, j])
                        {
                            grid[i, j] += 1;
                            grid[i + 1, j] = 0;
                            merged = true;
                        }
                        // 向左檢查合併
                        else if (j > 0 && grid[i, j] == grid[i, j - 1])
                        {
                            grid[i, j] += 1;
                            grid[i, j - 1] = 0;
                            merged = true;
                        }
                        // 向右檢查合併
                        else if (j < map.cols - 1 && grid[i, j] == grid[i, j + 1])
                        {
                            grid[i, j] += 1;
                            grid[i, j + 1] = 0;
                            merged = true;
                        }
                    }
                }
            }

            if (merged)
            {
                mergedMaps.Add(new Map(map.rows, map.cols)
                {
                    grid = grid // 更新地圖狀態
                });
            
                map.grid = grid;
            }

        } while (merged);
    }

}