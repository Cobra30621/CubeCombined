using System;
using System.Collections.Generic;
using System.Linq;
using Cube;
using Event;
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

                var number = _cubeRecorder.GetNumberInRange();

                bool inRange = number >= _cubeRecorder.MinShootingNumber &&
                               number < _cubeRecorder.MinShootingNumber + _cubeRecorder.ShootingRange;
                
                Assert.AreEqual(true, inRange);
            }
            
            
        }

        [Test]
        public void StartThisTurn_ClearRecords()
        {
            _cubeRecorder.StartThisTurn();
            
            Assert.AreEqual(0, _cubeRecorder.GetThisTurnEvent().Count);
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

        [TestCase(8, 8, EventType.CombineHistoryCubeAgain)]
        [TestCase(8, 9, EventType.CombineNewCube)]
        [TestCase(20, 21, EventType.CombineNewCube)]
        public void OnCombined_Reach_AddRecord(int highestCombine, int thisTurnCombine, EventType recordType)
        {
            _cubeRecorder = new CubeRecorder(setRecordThreshold: 8);
            
            _cubeRecorder.SetStartNumber(highestCombine);
            _cubeRecorder.StartThisTurn();
            _cubeRecorder.OnCombined(thisTurnCombine);

            var records = _cubeRecorder.GetThisTurnEvent();
            
            
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual(recordType, records[0].EventType);
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

            var records = _cubeRecorder.GetThisTurnEvent();

            var expectedRecords = new List<CubeEvent>()
            {
                new () { Number = 8, EventType = EventType.CombineNewCube , MinShootingNumber = 2},
                new (){ Number = 9, EventType = EventType.CombineNewCube  , MinShootingNumber = 3},
                new () { Number = 9, EventType = EventType.CombineHistoryCubeAgain  , MinShootingNumber = 3},
                new () { Number = 10, EventType = EventType.CombineNewCube  , MinShootingNumber = 4},
                new () { Number = 10, EventType = EventType.CombineHistoryCubeAgain  , MinShootingNumber = 4},
                new () { Number = 11, EventType = EventType.CombineNewCube  , MinShootingNumber = 5},
            };
            
            Assert.AreEqual(expectedRecords.Count, records.Count);
            for (int i = 0; i < expectedRecords.Count(); i++)
            {
                Assert.AreEqual(expectedRecords[i].Number, records[i].Number);
                Assert.AreEqual(expectedRecords[i].EventType, records[i].EventType);
                Assert.AreEqual(expectedRecords[i].MinShootingNumber, records[i].MinShootingNumber);
            }
        }
    }
}