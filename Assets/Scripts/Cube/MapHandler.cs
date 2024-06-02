using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public class MapHandler
    {
        [SerializeField] private List<Map> mergedMaps;
        [SerializeField] private Map gameMap;

        private ICubeRecorder _cubeRecorder;

        public Map GameMap => gameMap;
    
        public MapHandler(int numRows, int numCols)
        {
            mergedMaps = new List<Map>();
            gameMap = new Map(numRows, numCols);
        }
        
        public void SetCubeRecorder(ICubeRecorder cubeRecorder)
        {
            _cubeRecorder = cubeRecorder;
        }

        public void SetMap(int[,] grid)
        {
            gameMap = new Map(grid);
        }
        
        /// <summary>
        /// 在某一列新增 Cube
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="printMerge"></param>
        public bool AddCube(int column, int value, bool printMerge = false)
        {
            var canRelease = CanRelease(column);
            if (!canRelease)
            {
                return false;
            }
            
            _cubeRecorder.StartThisTurn();
            
            mergedMaps.Clear();
            gameMap.AddCube(column, value);
            mergedMaps.Add(new Map(gameMap)); // 新增一個 Map 的複本到 mergedMaps
            CheckAndMergeBlocks(gameMap);

            if (printMerge)
            {
                PrintMergedMaps();
            }

            return true;
        }

        
        /// <summary>
        /// 檢測該列是否可以方置方塊
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool CanRelease(int column)
        {
            return gameMap.GetGrid()[gameMap.rows - 1, column] == 0;
        }

        public int GetFirstZeroRowAt(int column)
        {
            var grid = gameMap.GetGrid();

            int firstRow = -1;

            for (int i = 0; i < gameMap.rows; i++)
            {
                if (grid[i, column] == 0)
                {
                    firstRow = i;
                    break;
                }
            }

            return firstRow;
        }
        

        
        public void CheckAndMergeBlocks(Map map)
        {
            bool shiftGridUp = false;
            bool merged = false;
            do
            {
                shiftGridUp = ShiftGridUp(map.GetGrid());
                if (shiftGridUp)
                {
                    mergedMaps.Add(new Map(map));
                }
                
                merged = MergeMap(map);
                
                if (merged)
                {
                    mergedMaps.Add(new Map(map));
                }
                
                
            } while (merged || shiftGridUp);
        }

        public bool MergeMap(Map map)
        {
            var merged = false;
            int[,] grid = map.GetGrid();

            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    if (grid[i, j] != 0 && !merged)
                    {
                        merged = MergeSingleBlock(grid, i, j);
                    }
                }
            }

            return merged;
        }

        private bool MergeSingleBlock(int[,] grid, int row, int col)
        {
            bool merged = false;
            int mergeNumber = -1;
            
            if (row > 0 && grid[row, col] == grid[row - 1, col])
            {
                mergeNumber = grid[row, col] + 1;
                grid[row, col] = mergeNumber;
                grid[row - 1, col] = 0;
                merged = true;
            }
            else if (row < grid.GetLength(0) - 1 && grid[row, col] == grid[row + 1, col])
            {
                mergeNumber = grid[row, col] + 1;
                grid[row, col] = mergeNumber;
                grid[row + 1, col] = 0;
                merged = true;
            }
            else if (col > 0 && grid[row, col] == grid[row, col - 1])
            {
                mergeNumber = grid[row, col] + 1;
                grid[row, col] = mergeNumber;
                grid[row, col - 1] = 0;
                merged = true;
            }
            else if (col < grid.GetLength(1) - 1 && grid[row, col] == grid[row, col + 1])
            {
                mergeNumber = grid[row, col] + 1;
                grid[row, col] = mergeNumber;
                grid[row, col + 1] = 0;
                merged = true;
            }
            
            if (merged)
            {
                _cubeRecorder?.OnCombined(mergeNumber);
            }

            return merged;
        }

        public bool ShiftGridUp(int[,] grid)
        {
            bool shifted = false;
            
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                for (int row = grid.GetLength(0)  -1; row >= 1; row--)
                {
                    if (grid[row, col] != 0)
                    {
                        if (grid[row - 1, col] == 0)
                        {
                            grid[row  - 1, col] = grid[row, col];
                            grid[row, col] = 0;
                            shifted = true;
                        }
                    }
                }
            }

            return shifted;
        }
        
        public List<Map> GetMergedMaps()
        {
            return mergedMaps;
        }


        public void PrintMergedMaps()
        {
            
            foreach (var map in mergedMaps)
            {
                map.PrintMap();
            }
        }

    }
}