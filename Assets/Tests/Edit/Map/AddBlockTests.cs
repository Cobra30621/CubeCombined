using System.Collections.Generic;
using Cube;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Edit
{
    public class AddBlockTests
    {
        private readonly MapTests _mapTests = new MapTests();

        
        #region MapHandler


        [Test]
        public void Empty_Init()
        {
            // Arrange
            var handler = new MapHandler(3, 3);

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
        public void SetMap_AreEqual()
        {
            // Arrange
            var handler = new MapHandler(3,3);
            int[,] grid = new int[,]
            {
                { 1, 2, 3 },
                { 0, 4, 1 },
                { 0, 0, 2 }
            };

            handler.SetMap(grid);
            
            // Assert
            AssertMap(grid, handler.GameMap);
        }

        #region CanRelease


        [Test]
        public void CheckCanRelease()
        {
            // Arrange
            var handler = new MapHandler(3,3);
            int[,] grid = new int[,]
            {
                { 1, 2, 3 },
                { 0, 4, 1 },
                { 0, 0, 2 }
            };

            handler.SetMap(grid);
            
            // Assert
            Assert.IsTrue(handler.CanRelease(0));
            Assert.IsTrue(handler.CanRelease(1));
            Assert.IsFalse(handler.CanRelease(2));
            
        }
        

        #endregion


        #region AddBlock

        [Test]
        public void AddBlock()
        {
            // Arrange
            var handler = new MapHandler(3, 3);

            // Act
            handler.AddCube(0,1,true);
            handler.AddCube(1,2,true);
            handler.AddCube(2,3,true);
            handler.AddCube(2,1,true);
            handler.AddCube(2,2,true);
            handler.AddCube(1,4,true);
            
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
            var handler = new MapHandler(3, 3);

            // Act
            handler.AddCube(0, 1, true);
            handler.AddCube(1, 1, true);
            
            handler.AddCube(1, 1, true);
            handler.AddCube(2, 1, true); // 合併方塊
            handler.AddCube(2, 1, true); // 合併方塊
            handler.AddCube(2, 1, true); // 合併方塊

            // Assert
            int[,] expectedMap = new int[,]
            {
                { 3, 0, 2 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            
            AssertMap(expectedMap, handler.GameMap);
        }
        
        private string folder_path = "Assets/Tests/Edit/Map/TestCases/";

        [Test]
        public void ShiftBlock()
        {
            var blockHandler = new MapHandler(3, 3);
            
            var mapClip = MapLoader.LoadMapsWithBool(folder_path + "ShiftBlockTestCase.txt");
            
            foreach (var (initMap, expect, expectHaveShift) in mapClip)
            {
                bool haveShift = blockHandler.ShiftGridUp(initMap.grid);
                AssertMap(expect.grid, initMap);
                Assert.AreEqual(expectHaveShift, haveShift);
            }
        }

        [Test]
        public void MergeMap()
        {
            var blockHandler = new MapHandler(3, 3);
            
            var mapClip = MapLoader.LoadMapsWithBool(folder_path + "MergeMapTestCase.txt");
            
            foreach (var (initMap, expect, expectHaveShift) in mapClip)
            {
                bool haveShift = blockHandler.MergeMap(initMap);
                AssertMap(expect.grid, initMap);
                Assert.AreEqual(expectHaveShift, haveShift);
            }
        }

        [Test]
        public void CheckAndMerge_BasicCase()
        {
            CheckAndMergeTest("CheckAndMerge_BasicCase.txt");
        }
        
        [Test]
        public void CheckAndMerge_CompleteCase()
        {
            CheckAndMergeTest("CheckAndMerge_CompleteCase.txt");
        }

        private void CheckAndMergeTest(string filename)
        {
            var blockHandler = new MapHandler(3, 3);
            
            var mapClip = MapLoader.LoadMapsWithInt(folder_path + filename);

            for (var index = 0; index < mapClip.Count; index++)
            {
                // Act
                Debug.Log($"[Test Case {index} ]");
                blockHandler.GetMergedMaps().Clear();
                var (initMap, expect, expectedMergeMapCount) = mapClip[index];

                var testMap = new Map(initMap);
                blockHandler.CheckAndMergeBlocks(testMap);
                int mergeMapCount = blockHandler.GetMergedMaps().Count;
                
                // Assert
                AssertMap(expect.grid, testMap);
                Debug.Log("Progress: " + blockHandler.GetMergedMaps().Count);
                initMap.PrintMap();
                blockHandler.PrintMergedMaps();
                Assert.AreEqual(expectedMergeMapCount, mergeMapCount);
            }
        }

        #endregion
        

        #endregion
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