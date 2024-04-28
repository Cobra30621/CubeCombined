using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Edit
{
    public class MapTest
    {
        [Test]
        public void Empty_Init()
        {
            // Arrange
            var handler = new BlockHandler(3, 3);

            // Act
            
            // Assert
            int[,] expectedMap = new int[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            AssertMap(expectedMap, handler.GameMap);
        }

        [Test]
        public void AddBlock()
        {
            // Arrange
            var handler = new BlockHandler(3, 3);

            // Act
            handler.AddBlock(0,1,true);
            handler.AddBlock(1,2,true);
            handler.AddBlock(2,3,true);
            handler.AddBlock(2,1,true);
            handler.AddBlock(2,2,true);
            handler.AddBlock(1,4,true);
            
            // Assert
            int[,] expectedMap = new int[,]
            {
                { 1, 2, 3 },
                { 0, 4, 1 },
                { 0, 0, 2 }
            };
            AssertMap(expectedMap, handler.GameMap);
        }

        
        [Test]
        public void MergeBlock()
        {
            // Arrange
            var handler = new BlockHandler(3, 3);

            // Act
            handler.AddBlock(0, 1, true);
            handler.AddBlock(1, 1, true);
            
            handler.AddBlock(1, 1, true);
            handler.AddBlock(2, 1, true); // 合併方塊
            handler.AddBlock(2, 1, true); // 合併方塊
            handler.AddBlock(2, 1, true); // 合併方塊

            // Assert
            int[,] expectedMap = new int[,]
            {
                { 3, 0, 2 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            
            AssertMap(expectedMap, handler.GameMap);
        }

        [Test]
        public void ShiftBlock()
        {
            var blockHandler = new BlockHandler(3, 3);
            blockHandler.AddBlock(2, 2, true);
            blockHandler.AddBlock(1, 1, true);
            blockHandler.AddBlock(2, 1, true);
            blockHandler.AddBlock(2, 4, true);
            blockHandler.AddBlock(1, 1, true);
            
            // Assert
            int[,] expectedMap = new int[,]
            {
                { 0, 3, 1 },
                { 0, 0, 4 },
                { 0, 0, 0 }
            };
            
            AssertMap(expectedMap, blockHandler.GameMap);
        }


        #region Tools

        private void AssertMap(int[,] expected, Map map)
        {
            Debug.Log("Actual Map :");
            map.PrintMap();
            PrintExpectMap(expected);

            CollectionAssert.AreEqual(expected, map.grid);
        }

        private void PrintExpectMap(int[,] expectedMap)
        {
            Debug.Log("Expect Map: ");
            string mapString = "";
            for (int i = 0; i < expectedMap.GetLength(0); i++)
            {
                for (int j = 0; j < expectedMap.GetLength(1); j++)
                {
                    mapString += expectedMap[i, j] + " ";
                }
                mapString += "\n";
            }
            Debug.Log(mapString);
        }

        #endregion
    }
}