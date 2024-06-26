using System;
using System.Collections.Generic;
using System.IO;

namespace Cube
{
    public class MapLoader
    {
        
        public static List<(Map,int[])> LoadGetFirstZeroRowAtMap(string filePath)
        {
            var mapPairs = new List<(Map, int[])>();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length % 4 != 0)
            {
                throw new Exception($"檔案格式錯誤：每四行應該對應一組 <Map, int[]> \n輸入為 {lines.Length}行");
            }

            for (int i = 0; i < lines.Length; i += 4)
            {
                Map map1 = ParseMap(lines[i + 0], lines[i + 1], lines[i + 2]);

                var firstZeroRow = ParseIntArray(lines[i + 3]);
                
                mapPairs.Add((map1, firstZeroRow));
            }

            return mapPairs;
            
        }
        public static List<(Map, bool[])> LoadCanReleaseMap(string filePath)
        {
            var mapPairs = new List<(Map, bool[])>();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length % 4 != 0)
            {
                throw new Exception($"檔案格式錯誤：每四行應該對應一組 <Map, bool[]> \n輸入為 {lines.Length}行");
            }

            for (int i = 0; i < lines.Length; i += 4)
            {
                Map map1 = ParseMap(lines[i + 0], lines[i + 1], lines[i + 2]);

                var canReleaseArray = ParseBoolArray(lines[i + 3]);
                
                mapPairs.Add((map1, canReleaseArray));
            }

            return mapPairs;
            
        }
        
        public static List<(Map, Map, bool)> LoadMapsWithBool(string filePath)
        {
            var intPairs = LoadMapsWithInt(filePath);
            var mapPairs = new List<(Map, Map, bool)>();
            foreach ((Map, Map, int) pair in intPairs)
            {
                bool haveChange = pair.Item3 == 1;
                mapPairs.Add((pair.Item1, pair.Item2, haveChange));
            }
        
            return mapPairs;
        }
        
        public static List<(Map, Map, int)> LoadMapsWithInt(string filePath)
        {
            var mapPairs = new List<(Map, Map, int)>();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length % 7 != 0)
            {
                throw new Exception($"檔案格式錯誤：每六行應該對應一組 <Map, Map>，並且每組 <Map, Map> 間應該有一個 int。\n輸入為 {lines.Length}行");
            }

            for (int i = 0; i < lines.Length; i += 7)
            {
                Map map1 = ParseMap(lines[i + 0], lines[i + 1], lines[i + 2]);
                Map map2 = ParseMap(lines[i + 3], lines[i + 4], lines[i + 5]);

                int value = 0;
                var parseSuccess = int.TryParse(lines[i + 6], out value);

                
                if (!parseSuccess)
                {
                    throw new Exception($"每組 <Map, Map> 間應該一個 int \n該行為:{lines[i + 6]}");
                }
                mapPairs.Add((map1, map2, value));
            }

            return mapPairs;
        }

        

        private static (bool, bool) ParseBool(string text)
        {
            if (text == "0" || text == "1")
            {
                return (text == "1", true);
            }

            return (false, false);
        }

        public static bool[] ParseBoolArray(string row)
        {
            string[] values1 = row.Split(' ');
            if (values1.Length != 3)
            {
                throw new Exception("檔案格式錯誤：每行應該包含三個數字");
            } 

            var bools = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                bools[i] = int.Parse(values1[i]) == 1;
            }

            return bools;
        }
        
        public static int[] ParseIntArray(string row)
        {
            string[] values1 = row.Split(' ');
            if (values1.Length != 3)
            {
                throw new Exception("檔案格式錯誤：每行應該包含三個數字");
            } 

            var intArray = new int[3];
            for (int i = 0; i < 3; i++)
            {
                intArray[i] = int.Parse(values1[i]);
            }

            return intArray;
        }

        public static Map ParseMap(string row1, string row2, string row3)
        {
            string[] values1 = row1.Split(' ');
            string[] values2 = row2.Split(' ');
            string[] values3 = row3.Split(' ');

            if (values1.Length != 3 || values2.Length != 3 || values3.Length != 3)
            {
                throw new Exception("檔案格式錯誤：每行應該包含三個數字");
            }

            int[,] grid = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                grid[0, i] = int.Parse(values1[i]);
                grid[1, i] = int.Parse(values2[i]);
                grid[2, i] = int.Parse(values3[i]);
            }

            return new Map(grid);
        }
    }
}