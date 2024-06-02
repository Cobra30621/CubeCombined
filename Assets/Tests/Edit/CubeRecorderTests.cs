using System;
using System.Collections.Generic;
using System.Linq;
using Cube;
using NUnit.Framework;

namespace Tests.Edit
{
    public class CubeRecorderTests
    {
        [SetUp]
        public void Setup()
        {
            _cubeRecorder = new CubeRecorder();
      
        }

        private CubeRecorder _cubeRecorder;

        [Test]
        public void SetStartNumber_Success()
        {
            for (int i = 1; i < 10; i++)
            {
                _cubeRecorder.SetStartNumber(i);
                
                Assert.AreEqual(i, _cubeRecorder.MinShootingNumber);
            }
        }
        
        [Test]
        public void SetStartNumber_MaxCombineNumberSet()
        {
            for (int i = 1; i < 10; i++)
            {
                _cubeRecorder.SetStartNumber(i);
                
                Assert.AreEqual(i, _cubeRecorder.HighestCombine);
            }
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void SetStartNumber_OutOfRange_ThrowsException(int startNumber)
        {
            Assert.Throws<Exception>(() => _cubeRecorder.SetStartNumber(startNumber));
        }

        [Test]
        public void GetShootingRange_ReturnsShootingRange()
        {
            for (int i = 1; i < 10; i++)
            {
                _cubeRecorder.SetStartNumber(i);

                var expectList = new List<int>();
                for (int j = 0; j < _cubeRecorder.ShootingRange; j++)
                {
                    expectList.Add(i + j);
                }
                
                Assert.AreEqual(expectList, _cubeRecorder.GetShootingRange());
            }
        }

        [Test]
        public void StartThisTurn_ClearRecords()
        {
            _cubeRecorder.StartThisTurn();
            
            Assert.AreEqual(0, _cubeRecorder.GetThisTurnRecords().Count);
        }

        [Test]
        public void OnCombined_SetMaxCombineNumber()
        {
            _cubeRecorder.StartThisTurn();
            for (int i = 1; i < 12; i++)
            {
                _cubeRecorder.OnCombined(i);
                
                Assert.AreEqual(i, _cubeRecorder.HighestCombine);
            }
        }

        [TestCase(8, 8, RecordType.CombineHistoryCubeAgain)]
        [TestCase(8, 9, RecordType.CombineNewCube)]
        [TestCase(20, 21, RecordType.CombineNewCube)]
        public void OnCombined_Reach_AddRecord(int highestCombine, int thisTurnCombine, RecordType recordType)
        {
            _cubeRecorder = new CubeRecorder(setRecordThreshold: 8);
            
            _cubeRecorder.SetStartNumber(highestCombine);
            _cubeRecorder.StartThisTurn();
            _cubeRecorder.OnCombined(thisTurnCombine);

            var records = _cubeRecorder.GetThisTurnRecords();
            
            
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual(recordType, records[0].RecordType);
        }

        [Test]
        public void OnCombined_LotsOfTime_RecordAraSame()
        {
            _cubeRecorder = new CubeRecorder(setRecordThreshold: 8);
            
            _cubeRecorder.SetStartNumber(1);
            _cubeRecorder.StartThisTurn();


            var combines = new int [] { 1, 3 , 5, 7, 8, 9, 9, 10 ,9, 10, 11 };
            foreach (var combine in combines)
            {
                _cubeRecorder.OnCombined(combine);
            }

            var records = _cubeRecorder.GetThisTurnRecords();

            var expectedRecords = new List<Record>()
            {
                new Record() { Number = 8, RecordType = RecordType.CombineNewCube },
                new Record() { Number = 9, RecordType = RecordType.CombineNewCube },
                new Record() { Number = 9, RecordType = RecordType.CombineHistoryCubeAgain },
                new Record() { Number = 10, RecordType = RecordType.CombineNewCube },
                new Record() { Number = 10, RecordType = RecordType.CombineHistoryCubeAgain },
                new Record() { Number = 11, RecordType = RecordType.CombineNewCube },
            };
            
            Assert.AreEqual(expectedRecords.Count, records.Count);
            for (int i = 0; i < expectedRecords.Count(); i++)
            {
                Assert.AreEqual(expectedRecords[i].Number, records[i].Number);
                Assert.AreEqual(expectedRecords[i].RecordType, records[i].RecordType);
            }
        }
    }
}