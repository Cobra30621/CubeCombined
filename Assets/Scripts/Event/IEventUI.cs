using System.Collections.Generic;

namespace Event
{
    public interface IEventUI
    {
        void ShowEvent(List<CubeEvent> events);

        bool IsPlayingEvent();
    }
}