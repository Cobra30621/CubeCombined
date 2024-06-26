using System.Collections.Generic;
using Event;

namespace Cube
{
    public interface ICubeRecorder
    {
        void SetStartNumber(int number);
        
        int GetNumberInRange();

        void StartThisTurn();
        
        void OnCombined(int number);

        List<CubeEvent> GetThisTurnEvent();

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