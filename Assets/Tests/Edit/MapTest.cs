using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Edit
{
    public class MapTest
    {
        [Test]
        public void TestBlockHandler()
        {
            // Arrange
            BlockHandler blockHandler = new BlockHandler(3, 3);

            // Act
            blockHandler.AddBlock(0, 1, true);
            blockHandler.AddBlock(1, 2, true);
            blockHandler.AddBlock(1, 3, true); // 合併方塊
            blockHandler.AddBlock(1, 2, true); // 合併方塊
            blockHandler.AddBlock(1, 2, true); // 合併方塊
            blockHandler.AddBlock(1, 2, true); // 合併方塊
            blockHandler.PrintMergedMaps();

            
        }
        
        [Test]
        public void TestBlockHandler2()
        {
            // Arrange
            BlockHandler blockHandler = new BlockHandler(3, 3);

            // Act
            blockHandler.AddBlock(0, 1, true);
            blockHandler.AddBlock(2, 1, true);
            blockHandler.AddBlock(1, 1, true);
            blockHandler.AddBlock(1, 2, true); // 合併方塊

            // Assert
            int[,] expectedMap = new int[,]
            {
                { 3, 0, 1 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            CollectionAssert.AreEqual(expectedMap, blockHandler.GameMap.grid);
        }

        [Test]
        public void Test3Merge()
        {
            var blockHandler = new BlockHandler(3, 3);
            blockHandler.AddBlock(2, 2, true);
            blockHandler.AddBlock(1, 1, true);
            blockHandler.AddBlock(2, 1, true);
            blockHandler.AddBlock(1, 1, true);
            
            // Assert
            int[,] expectedMap = new int[,]
            {
                { 0, 3, 1 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            CollectionAssert.AreEqual(expectedMap, blockHandler.GameMap.grid);
        }
    }
}