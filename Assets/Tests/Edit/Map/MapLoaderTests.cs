using System;
using System.IO;
using Cube;
using NUnit.Framework;

namespace Tests.Edit
{
    public class MapLoaderTests
    {
        #region ParseMap

        [Test]
        public void ParseMap_EmptyLine_ThrowException()
        {
            Assert.Throws<Exception>(()=>MapLoader.ParseMap("", "", ""));
        }

        
        [Test]
        public void ParseMap_ValidInput_ReturnsMap()
        {
            Assert.IsNotNull(MapLoader.ParseMap("1 2 3", "4 5 6", "7 8 9"));
        }

        [Test]
        public void ParseMap_MissingValues_ThrowException()
        {
            Assert.Throws<Exception>(()=>MapLoader.ParseMap("1 2", "4 5 6", "7 8 9"));
        }

        [Test]
        public void ParseMap_InvalidValues_ThrowException()
        {
            Assert.Throws<FormatException>(()=>MapLoader.ParseMap("1 a 3", "4 5 6", "7 8 9"));
        }
        

        #endregion

        #region LoadMapsFromFile

        private string folder_path = "Assets/Tests/Edit/Map/TestCases/";
        
        [Test]
        public void LoadMapsFromFile_ValidFile_ReturnsCorrectMapPairs()
        {
            // Arrange
            string filePath = folder_path + "test_data.txt";
            string[] expectedMaps = new string[]
            {
                "1 2 3",
                "4 5 6",
                "7 8 9",
                "10 11 12",
                "13 14 15",
                "16 17 18",
                "0",
            };
            File.WriteAllLines(filePath, expectedMaps);

            // Act
            var result = MapLoader.LoadMapsFromFile(filePath);

            // Assert
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEqual(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }, result[0].Item1.grid);
            CollectionAssert.AreEqual(new int[,] { { 10, 11, 12 }, { 13, 14, 15 }, { 16, 17, 18 } }, result[0].Item2.grid);
            Assert.AreEqual(false, result[0].Item3);
        }
        
        
        [Test]
        public void LoadMapsFromFile_ValidFile_Check21stLine()
        {
            // Arrange
            string filePath =  folder_path + "test_data_21.txt";
            string[] expectedMaps = new string[]
            {
                "1 2 3",
                "4 5 6",
                "7 8 9",
                "10 11 12",
                "13 14 15",
                "16 17 18",
                "0",
                "19 20 21",
                "22 23 24",
                "25 26 27",
                "28 29 30",
                "31 32 33",
                "34 35 36",
                "1",
            };
            File.WriteAllLines(filePath, expectedMaps);

            // Act
            var result = MapLoader.LoadMapsFromFile(filePath);

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.AreEqual(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }, result[0].Item1.grid);
            CollectionAssert.AreEqual(new int[,] { { 10, 11, 12 }, { 13, 14, 15 }, { 16, 17, 18 } }, result[0].Item2.grid);
            Assert.AreEqual(false, result[0].Item3);
            CollectionAssert.AreEqual(new int[,] { { 19, 20, 21 }, { 22, 23, 24 }, { 25, 26, 27 } }, result[1].Item1.grid);
            CollectionAssert.AreEqual(new int[,] { { 28, 29, 30 }, { 31, 32, 33 }, { 34, 35, 36 } }, result[1].Item2.grid);
            Assert.AreEqual(true, result[1].Item3);
        }

        [Test]
        public void LoadMapsFromFile_InvalidFile_ThrowsException()
        {
            // Arrange
            string filePath =  folder_path + "invalid_data.txt";
            string[] invalidData = new string[]
            {
                "1 2 3",
                "4 5 6",
                "7 8 9",
                "10 11 12",
                "13 14 15",
                "16 17 18",
                "19 20 21"
            };
            File.WriteAllLines(filePath, invalidData);

            // Act & Assert
            Assert.Throws<Exception>(() => MapLoader.LoadMapsFromFile(filePath));
        }

        [Test]
        public void LoadMapsFromFile_EmptyFile_ReturnsEmptyList()
        {
            // Arrange
            string filePath =  folder_path + "empty_data.txt";
            File.WriteAllLines(filePath, new string[] { });

            // Act
            var result = MapLoader.LoadMapsFromFile(filePath);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void LoadMapsFromFile_OddNumberOfLines_ThrowsException()
        {
            // Arrange
            string filePath =  folder_path + "odd_data.txt";
            string[] oddData = new string[]
            {
                "1 2 3",
                "4 5 6",
                "7 8 9",
                "10 11 12"
            };
            File.WriteAllLines(filePath, oddData);

            // Act & Assert
            Assert.Throws<Exception>(() => MapLoader.LoadMapsFromFile(filePath));
        }

        [Test]
        public void LoadMapsFromFile_InvalidMapFormat_ThrowsException()
        {
            // Arrange
            string filePath =  folder_path + "invalid_map_format.txt";
            string[] invalidMapData = new string[]
            {
                "1 2 3",
                "4 5 6",
                "7 8 9",
                "10 11 12",
                "13 14 15",
                "16 17 18",
                "19 20 21",
                "40 41 42"
            };
            File.WriteAllLines(filePath, invalidMapData);

            // Act & Assert
            Assert.Throws<Exception>(() => MapLoader.LoadMapsFromFile(filePath));
        }

        [Test]
        public void LoadRealFile()
        {
            var result = MapLoader.LoadMapsFromFile( folder_path + "ShiftBlockTestCase.txt");

            foreach (var (item1, item2, item3) in result)
            {
                item1.PrintMap();
                item2.PrintMap();
            }
        }

        #endregion
    }
}