using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public class CubeRecorder : ICubeRecorder
    {
        public int MinShootingNumber { get; private set; }
        
        public int ShootingRange { get; private set; }
        
        public int SetRecordThreshold { get; private set; }

        public int HighestCombine { get; private set; }

        private List<Record> _records;
        

        public CubeRecorder(int shootingRange = 5, int setRecordThreshold = 8)
        {
            ShootingRange = shootingRange;
            SetRecordThreshold = setRecordThreshold;
        }


        public void SetStartNumber(int number)
        {
            if (number < 1)
            {
                throw new Exception($"Start Number must be greater than 1, but {number}");
            }
            
            MinShootingNumber = number;
            HighestCombine = number;
        }

        public List<int> GetShootingRange()
        {
            var range = new List<int>();
            for (int i = 0; i < ShootingRange; i++)
            {
                range.Add(MinShootingNumber + i);
            }

            return range;
        }

        public void StartThisTurn()
        {
            _records = new List<Record>();
        }

        public void OnCombined(int number)
        {
            if (number == HighestCombine)
            {
                if (number >= SetRecordThreshold)
                {
                    _records.Add(new Record()
                    {
                        RecordType = RecordType.CombineHistoryCubeAgain,
                        Number = number
                    });
                }
            }else if (number > HighestCombine)
            {
                HighestCombine = number;

                if (number >= SetRecordThreshold)
                {
                    _records.Add(new Record()
                    {
                        RecordType = RecordType.CombineNewCube,
                        Number = number
                    });
                }
            }
        }

        public List<Record> GetThisTurnRecords()
        {
            return _records;
        }
    }
}