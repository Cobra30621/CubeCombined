using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cube
{
    public class CubeUI : SerializedMonoBehaviour, ICubeUI
    {
        [SerializeField] private Color[] _colors;

        [SerializeField] private List<List<Cube>> cubes;

        [SerializeField] private Cube cubePrefab;

        [SerializeField] private Transform spawnTransform;
        
        
        public void UpdateCubeDisplay(Map map)
        {
            var grid = map.GetGrid();
            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    var cube = cubes[i][j];
                    var number = grid[i, j];
                    cube.SetInfo($"{number}", _colors[number] );
                }
            }
        }

        public void InitCubes(int column, int row)
        {
            cubes = new List<List<Cube>>();
            for (int i = 0; i < row; i++)
            {
                var columnCube = new List<Cube>();
                for (int j = 0; j <column; j++)
                {
                    var cube = GameObject.Instantiate(cubePrefab, spawnTransform);
                    columnCube.Add(cube);
                }
                cubes.Add(columnCube);
                
            }
        }
        
        public void PlayAddCubeAnimation(List<Map> mergeMap)
        {
           
        }

        public bool IsAnimationComplete()
        {
            return true;
        }
    }
}