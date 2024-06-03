using System.Collections.Generic;

namespace Event
{
    public interface IEventController
    {
        void ShowEvent(List<CubeEvent> events);

        bool IsPlayingEvent();

        void OnCloseEventPanel();
    }
}