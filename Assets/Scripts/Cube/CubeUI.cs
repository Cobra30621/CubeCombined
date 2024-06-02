using System.Collections;
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
        
        [SerializeField] private Cube _selectedCube;

        [Required]
        [SerializeField] private Cube _currentCube;

        private bool isAnimateCompeled;
        
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

        public void UpdateCurrentCube(int number)
        {
            _currentCube.SetInfo($"{number}", _colors[number] );
        }

        [Button]
        public void ShowCubePreviewAt(int column, int row, int index)
        {
            _selectedCube = cubes[row][column];
            _selectedCube.SetInfo($"{index}", _colors[index] );
            _selectedCube.SetSelected(true);
        }

        public void ClosePreview()
        {
            if (_selectedCube != null)
            {
                _selectedCube.SetInfo($"", _colors[0] );
                _selectedCube.SetSelected(false);
            }
            
            _selectedCube = null;
            
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
            StartCoroutine(AddCubeAnimation(mergeMap));
        }

        
        IEnumerator AddCubeAnimation(List<Map> mergeMap)
        {
            isAnimateCompeled = false;
            foreach (var map in mergeMap)
            {
                UpdateCubeDisplay(map);
                yield return new WaitForSeconds(0.3f);
            }

            isAnimateCompeled = true;
        }
        
        

        public bool IsAnimationComplete()
        {
            return isAnimateCompeled;
        }
    }
}