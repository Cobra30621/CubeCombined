using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    public class EventUI : SerializedMonoBehaviour, IEventUI
    {
        [SerializeField] private bool _isPlayingEvent;
        
        public void ShowEvent(List<CubeEvent> events)
        {
            StartCoroutine(ShowEventCoroutine(events));
        }


        IEnumerator ShowEventCoroutine(List<CubeEvent> events)
        {
            _isPlayingEvent = true;
            foreach (var cubeEvent in events)
            {
                Debug.Log($"{cubeEvent.EventType}, {cubeEvent.Number}");
                yield return new WaitForSeconds(0.5f);
            }

            _isPlayingEvent = false;
        }

        public bool IsPlayingEvent()
        {
            return _isPlayingEvent;
        }
    }
}