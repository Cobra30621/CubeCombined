using System;
using NUnit.Framework;

namespace Tests.Edit
{
    public class MapTests
    {
        [Test]
        public void InitMap_ZeroElement()
        {
            // Arrange
            var map = new Map(3, 3);
            
            // Act
            
            // Assert
            int[,] expected = new int[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };
            
            CollectionAssert.AreEqual(expected, map.grid);
        }

        [Test]
        public void MapSetMap_AreEqual()
        {
            // Arrange
            int[,] expectedMap = new int[,]
            {
                { 1, 2, 3 },
                { 0, 4, 1 },
                { 0, 0, 2 }
            };

            var map = new Map(expectedMap);
            
            // Assert
            CollectionAssert.AreEqual(expectedMap, map.grid);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void AddCube_ValidColumnInEmpty_AddCube(int column)
        {
            // Arrange
            int[,] expectedMap = new int[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            expectedMap[0, column] = 1;
            var map = new Map(3,3);

            // Act
            map.AddCube(column, 1);
            
            // Assert
            CollectionAssert.AreEqual(expectedMap, map.grid);
        }

        [Test]
        public void AddCube_ValidColumnNoneEmptyColumn_AddCube()
        {
            // Arrange
            int[,] expectedMap = new int[,]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            var map = new Map(3,3);
            
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    map.AddCube(col, 1);
                    expectedMap[row, col] = 1;
                    
                    // Assert
                    CollectionAssert.AreEqual(expectedMap, map.grid);
                }
            }
        }


        [Test]
        public void AddCube_InvalidColumn_ThrowException()
        {
            var map = new Map(3,3);
            Assert.Throws<Exception>(() => map.AddCube(-1, 1));
            
            Assert.Throws<Exception>(() => map.AddCube(4, 1));
        }

        [Test]
        public void AddCube_FullColumn_ThrowException()
        {
            var map = new Map(3,3);
            
            for (int i = 0; i < 3; i++)
            {
                map.AddCube(0, 1);
            }
            Assert.Throws<Exception>(() => map.AddCube(0, 1));
        }
        
        
        
        
    }
}