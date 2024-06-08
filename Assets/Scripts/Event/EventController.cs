using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    /// <summary>
    /// EventController class manages the event flow and interacts with panels to display events.
    /// </summary>
    public class EventController : SerializedMonoBehaviour, IEventController
    {
        [Required] [SerializeField] private CreateNewCubePanel createNewCubePanel;
        [Required] [SerializeField] private CreateHistoryAgainPanel createHistoryAgainPanel;
        [Required] [SerializeField] private ShootingChangePanel addShootingPanel, removeShootingPanel;

        [SerializeField] private bool _isPlayingEvent;

        /// <summary>
        /// Initializes the event controller and sets up the panels.
        /// </summary>
        private void Awake()
        {
            createNewCubePanel.Init(this);
            createHistoryAgainPanel.Init(this);
            addShootingPanel.Init(this);
            removeShootingPanel.Init(this);
        }

        /// <summary>
        /// Shows the events in the specified order.
        /// </summary>
        /// <param name="events">A list of events to be displayed.</param>
        public void ShowEvent(List<CubeEvent> events)
        {
            StartCoroutine(ShowEventCoroutine(events));
        }

        /// <summary>
        /// Coroutine to display the events in the specified order.
        /// </summary>
        /// <param name="events">A list of events to be displayed.</param>
        /// <returns>An IEnumerator object that can be used to control the flow of the coroutine.</returns>
        private IEnumerator ShowEventCoroutine(List<CubeEvent> events)
        {
            foreach (var cubeEvent in events)
            {
                Debug.Log($"{cubeEvent.EventType}, {cubeEvent.Number}");

                switch (cubeEvent.EventType)
                {
                    case EventType.CombineNewCube:
                        yield return PlayCreateNewCube(cubeEvent);
                        break;
                    case EventType.CombineHistoryCubeAgain:
                        _isPlayingEvent = true;
                        createHistoryAgainPanel.ShowPanel(cubeEvent.Number, 100);
                        yield return new WaitUntil(() => !_isPlayingEvent);
                        break;
                }
            }
        }

        /// <summary>
        /// Coroutine to display the event of creating a new cube.
        /// </summary>
        /// <param name="cubeEvent">The event data for creating a new cube.</param>
        /// <returns>An IEnumerator object that can be used to control the flow of the coroutine.</returns>
        private IEnumerator PlayCreateNewCube(CubeEvent cubeEvent)
        {
            _isPlayingEvent = true;
            createNewCubePanel.ShowPanel(cubeEvent.Number, 100);
            yield return new WaitUntil(() => !_isPlayingEvent);

            _isPlayingEvent = true;
            addShootingPanel.ShowPanel(cubeEvent.Number);
            yield return new WaitUntil(() => !_isPlayingEvent);

            _isPlayingEvent = true;
            removeShootingPanel.ShowPanel(cubeEvent.Number);
            yield return new WaitUntil(() => !_isPlayingEvent);
        }

        /// <summary>
        /// Checks if the event is currently being played.
        /// </summary>
        /// <returns>True if the event is being played, false otherwise.</returns>
        public bool IsPlayingEvent()
        {
            return _isPlayingEvent;
        }

        /// <summary>
        /// Sets the flag indicating that the event panel is closed.
        /// </summary>
        public void OnCloseEventPanel()
        {
            _isPlayingEvent = false;
        }
    }
}