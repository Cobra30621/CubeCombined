using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    public abstract class EventPanelBase : MonoBehaviour
    {
        protected IEventController _controller;
        
        [Required]
        [SerializeField] protected GameObject mainPanel;
        
        public void Init(IEventController controller)
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