using System.Collections.Generic;

namespace Cube
{
    public interface ICubeRecorder
    {
        void SetStartNumber(int number);
        
        List<int> GetShootingRange();

        void StartThisTurn();
        
        void OnCombined(int number);

        List<Record> GetThisTurnRecords();
    }

    public class Record
    {
        public RecordType RecordType;
        public int Number;
    }

    public enum RecordType
    {
        CombineNewCube,
        CombineHistoryCubeAgain
    }
    
}