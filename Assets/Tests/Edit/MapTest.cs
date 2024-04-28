using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Edit
{
    public class MapTest
    {
        [Test]
        public void Test()
        {
            var gameMap = new Map(3, 3);
            gameMap.PrintMap();

            gameMap.AddBlock(0, 1);
            gameMap.PrintMap();

            gameMap.AddBlock(0, 1);
            gameMap.PrintMap();

            gameMap.AddBlock(0, 1);
            gameMap.PrintMap();
        }

        [Test]
        public void TestBlockHandler()
        {
            // Arrange
            BlockHandler blockHandler = new BlockHandler(3, 3);

            // Act
            blockHandler.AddBlock(0, 1);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(1, 2);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(2, 3);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(1, 2); // 合併方塊
            blockHandler.PrintMergedMaps();

            List<Map> mergedMaps = blockHandler.GetMergedMaps();

        }
        
        [Test]
        public void TestBlockHandler2()
        {
            // Arrange
            BlockHandler blockHandler = new BlockHandler(3, 3);

            // Act
            blockHandler.AddBlock(0, 1);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(2, 1);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(1, 1);
            blockHandler.PrintMergedMaps();
            blockHandler.AddBlock(1, 2); // 合併方塊
            blockHandler.PrintMergedMaps();

            List<Map> mergedMaps = blockHandler.GetMergedMaps();

        }
    }
}