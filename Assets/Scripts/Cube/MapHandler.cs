using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public class MapHandler
    {
        private List<Map> mergedMaps;
        private Map gameMap;

        public Map GameMap => gameMap;
    
        public MapHandler(int numRows, int numCols)
        {
            mergedMaps = new List<Map>();
            gameMap = new Map(numRows, numCols);
        }

        public void SetMap(int[,] grid)
        {
            gameMap = new Map(grid);
        }

        public void PrintMergedMaps()
        {
            Debug.Log(mergedMaps.Count);
            foreach (var map in mergedMaps)
            {
                map.PrintMap();
            }
        }

        public bool CanRelease(int column)
        {
            return gameMap.GetGrid()[column, gameMap.rows - 1] == 0;
        }
        

        public void AddCube(int column, int value, bool printMerge = false)
        {
            gameMap.AddCube(column, value);
            mergedMaps.Clear();
            mergedMaps.Add(new Map(gameMap)); // 新增一個 Map 的複本到 mergedMaps
            CheckAndMergeBlocks(gameMap);

            if (printMerge)
            {
                PrintMergedMaps();
            }
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
                        if (grid[i, j] != 0 && !merged)
                        {
                            merged = CheckAndMerge(grid, i, j);
                        }
                    }
                }

                if (merged)
                {
                    map.grid = grid;
                    mergedMaps.Add(new Map(map));

                    var shiftGridUp = ShiftGridUp(grid);
                    if (shiftGridUp)
                    {
                        mergedMaps.Add(new Map(map));
                    }
                }

            } while (merged);
        }

        private bool CheckAndMerge(int[,] grid, int row, int col)
        {
            bool merged = false;

            if (row > 0 && grid[row, col] == grid[row - 1, col])
            {
                grid[row, col] += 1;
                grid[row - 1, col] = 0;
                merged = true;
            }
            else if (row < grid.GetLength(0) - 1 && grid[row, col] == grid[row + 1, col])
            {
                grid[row, col] += 1;
                grid[row + 1, col] = 0;
                merged = true;
            }
            else if (col > 0 && grid[row, col] == grid[row, col - 1])
            {
                grid[row, col] += 1;
                grid[row, col - 1] = 0;
                merged = true;
            }
            else if (col < grid.GetLength(1) - 1 && grid[row, col] == grid[row, col + 1])
            {
                grid[row, col] += 1;
                grid[row, col + 1] = 0;
                merged = true;
            }

            return merged;
        }
    
        private bool ShiftGridUp(int[,] grid)
        {
            var none0 = new bool [gameMap.cols];
            for (var i = 0; i < none0.Length; i++)
            {
                none0[i] = false;
            }

            bool shifted = false;

            for (int col = 0; col < grid.GetLength(1); col++)
            {
                for (int row = grid.GetLength(0) - 1; row >= 0 ; row--)
                {
                    if (grid[row, col] == 0)
                    {
                        if (none0[col])
                        {
                            for (int i = row; i < grid.GetLength(0) - 1; i++)
                            {
                                grid[row + i, col] = grid[row + i + 1, col];
                            }

                            grid[grid.GetLength(0) - 1, col] = 0;
                            shifted = true;
                        }
                    }
                    else
                    {
                        none0[col] = true;
                    }
                }
            }

            return shifted;
        }



    }
}