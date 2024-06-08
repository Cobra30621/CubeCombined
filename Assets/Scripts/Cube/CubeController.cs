using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cube
{
    public class CubeController : MonoBehaviour, ICubeController
    {
        [Required]
        [SerializeField] private CubeData _cubeData;

        public List<List<Cube>> Cubes => cubes;
        [SerializeField] private List<List<Cube>> cubes;

        [SerializeField] private Cube cubePrefab;

        [SerializeField] private Transform spawnTransform;
        
        [SerializeField] private Cube _selectedCube;

        [Required]
        [SerializeField] private Cube _currentCube;

        private bool isAnimateCompeled;

        private ICubeManager _cubeManager;
        
        public void Init(ICubeManager cubeManager)
        {
            _cubeManager = cubeManager;
        }
        
        public void InitCubes(int row, int column)
        {
            DestroyAllCube();
            
            cubes = new List<List<Cube>>();
            for (int i = 0; i < row; i++)
            {
                var columnCube = new List<Cube>();
                for (int j = 0; j <column; j++)
                {
                    var cube = GameObject.Instantiate(cubePrefab, spawnTransform);
                    cube.name = "Cube " + i + "_" + j;
                    Debug.Log(cube.name);
                    columnCube.Add(cube);
                }
                cubes.Add(columnCube);
                
            }
        }

        private void DestroyAllCube()
        {
            // 找到所有具有 Cube 組件的遊戲物件
            Cube[] cubes = spawnTransform.gameObject.GetComponentsInChildren<Cube>();

            // 遍歷所有找到的 Cube 組件並銷毀它們的遊戲物件
            foreach (Cube cube in cubes)
            {
                Destroy(cube.gameObject);
            }
        }
        
        public void UpdateCubeDisplay(Map map)
        {
            var grid = map.GetGrid();
            for (int i = 0; i < map.rows; i++)
            {
                for (int j = 0; j < map.cols; j++)
                {
                    var cube = cubes[i][j];
                    var number = grid[i, j];
                    _cubeData.SetCubeInfo(cube, number);
                }
            }
        }

        public void UpdateCurrentCube(int number)
        {
            _cubeData.SetCubeInfo(_currentCube, number);
        }

        [Button]
        public void ShowPreviewAt(int column)
        {
            var row = _cubeManager.GetFirstZeroRowAt(column);
            if (row == -1)
            {
                return;
            }
            
            var index = _cubeManager.CurrentCube;
            _selectedCube = cubes[row][column];
            _cubeData.SetCubeInfo(_selectedCube, index);
            _selectedCube.SetSelected(true);
        }

        public void ClosePreview()
        {
            if (_selectedCube != null)
            {
                _cubeData.SetCubeInfo(_selectedCube, 0);
                _selectedCube.SetSelected(false);
            }
            
            _selectedCube = null;
            
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