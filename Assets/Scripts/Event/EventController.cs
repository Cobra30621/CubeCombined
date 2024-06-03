using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    public class EventController : SerializedMonoBehaviour, IEventController
    {
        
        [Required] [SerializeField] private CreateNewCubePanel createNewCubePanel;
        [Required] [SerializeField] private CreateHistoryAgainPanel createHistoryAgainPanel;
        [Required] [SerializeField] private ShootingChangePanel addShootingPanel, removeShootingPanel;
        
        [SerializeField] private bool _isPlayingEvent;

        private void Awake()
        {
            createNewCubePanel.Init(this);
            createHistoryAgainPanel.Init(this);
            addShootingPanel.Init(this);
            removeShootingPanel.Init(this);
        }


        public void ShowEvent(List<CubeEvent> events)
        {
            StartCoroutine(ShowEventCoroutine(events));
        }


        IEnumerator ShowEventCoroutine(List<CubeEvent> events)
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
                        yield return new WaitUntil(()=>!_isPlayingEvent);
                        break;
                }
                
                
            }
        }

        IEnumerator PlayCreateNewCube(CubeEvent cubeEvent)
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

        public bool IsPlayingEvent()
        {
            return _isPlayingEvent;
        }

        public void OnCloseEventPanel()
        {
            _isPlayingEvent = false;
        }
    }
}