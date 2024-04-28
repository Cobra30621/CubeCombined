using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Cube
{
    public class CubeManager : ICubeManager
    {
        private List<List<int>> grid = new List<List<int>>();

        public override void Init()
        {
            InitMap();
        }

        public void InitMap()
        {
            for (int i = 0; i < 5; i++)
            {
                grid.Add(new List<int>());
                for (int j = 0; j < 10; j++)
                {
                    grid[i].Add(0); // Initialize all cells with 0
                }
            }
        }

        private bool isPlayingAnimation;


        public override int GetCurrentCube()
        {
            throw new System.NotImplementedException();
        }

        public override void AddCube(int row, int index)
        {
            if (row < 5 && row >= 0)
            {
                for (int i = 9; i >= 0; i--)
                {
                    if (grid[row][i] == 0)
                    {
                        grid[row][i] = index;
                        return;
                    }
                }
            }
        }

        private IEnumerator CombinedCoroutine()
        {


            yield return null;
        }
        

        public override bool CanRelease(int row)
        {
            return false;
        }

        public override bool IsPlayingAnimation()
        {
            return isPlayingAnimation;
        }
    }
}