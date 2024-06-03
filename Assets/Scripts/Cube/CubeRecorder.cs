using System;
using System.Collections.Generic;
using System.Linq;
using Event;
using Sirenix.OdinInspector;
using UnityEngine;
using EventType = Event.EventType;
using Random = UnityEngine.Random;

namespace Cube
{
    public class CubeRecorder : ICubeRecorder
    {
        [ShowInInspector]
        public int MinShootingNumber { get; private set; }
        [ShowInInspector]
        public int ShootingRange { get; private set; }
        [ShowInInspector]
        public int SetRecordThreshold { get; private set; }
        [ShowInInspector]
        public int HighestCombine { get; private set; }


        private List<CubeEvent> _cubeEvents;
        

        public CubeRecorder(int shootingRange = 5, int setRecordThreshold = 8)
        {
            ShootingRange = shootingRange;
            SetRecordThreshold = setRecordThreshold;
            SetStartNumber(1);
        }


        public void SetStartNumber(int number)
        {
            if (number < 1)
            {
                throw new Exception($"Start Number must be greater or equal than 1, but {number}");
            }
            
            MinShootingNumber = number;
            HighestCombine = number;
        }



        public int GetNumberInRange()
        {
            return Random.Range(MinShootingNumber, MinShootingNumber + ShootingRange);
        }

        public void StartThisTurn()
        {
            _cubeEvents = new List<CubeEvent>();
        }

        public void OnCombined(int number)
        {
            if (number == HighestCombine)
            {
                if (number >= SetRecordThreshold)
                {
                    HandleCombineHistoryCube(number);
                }
            }else if (number > HighestCombine)
            {
                if (number >= SetRecordThreshold)
                {
                    HandleCombineNewCube(number);
                }
                
                HighestCombine = number;
            }
        }

        private void HandleCombineNewCube(int number)
        {
            MinShootingNumber = Math.Max(MinShootingNumber, number - SetRecordThreshold + 2);
            
            _cubeEvents.Add(
            new CubeEvent()
            {
                EventType = EventType.CombineNewCube,
                Number = number,
                MinShootingNumber = MinShootingNumber
            });
        }

        private void HandleCombineHistoryCube(int number)
        {
            _cubeEvents.Add(
                new CubeEvent()
                {
                    EventType = EventType.CombineHistoryCubeAgain,
                    Number = number,
                    MinShootingNumber = MinShootingNumber
                });
        }

        public List<CubeEvent> GetThisTurnEvent()
        {
            return _cubeEvents;
        }

    }
}