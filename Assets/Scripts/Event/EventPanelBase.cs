using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    public abstract class EventPanelBase : MonoBehaviour
    {
        protected EventController _controller;
        
        [Required]
        [SerializeField] protected GameObject mainPanel;
        
        public void Init(EventController controller)
        {
            _controller = controller;
        }

        public void Close()
        {
            mainPanel.SetActive(false);
            _controller.OnCloseEventPanel();
        }

    }
}