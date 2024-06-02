namespace Event
{
    public class CubeEvent
    {
        public EventType EventType;
        public int Number;

        public int MinShootingNumber;
    }

    public enum EventType
    {
        CombineNewCube,
        CombineHistoryCubeAgain,
        AddNewShootingCube,
        CancelShootingCube,
        NextGoal
    }
}