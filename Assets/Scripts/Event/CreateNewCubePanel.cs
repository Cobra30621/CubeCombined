using Cube;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Event
{
    public class CreateNewCubePanel : EventPanelBase
    {
        [Required]
        [SerializeField] private CubeData cubeData;
        [SerializeField] private Cube.Cube _cube;

        [Required]
        [SerializeField] private TextMeshProUGUI info;

        private int _reward;

        public void ShowPanel(int number, int reward)
        {
            _reward = reward;
            mainPanel.SetActive(true);
            cubeData.SetCubeInfo(_cube, number);
            
            info.text = $"+{reward}";
            
        }

        public void Conform()
        {
            Debug.Log($"Get reward {_reward}");
            Close();
        }
        
        
    }
}