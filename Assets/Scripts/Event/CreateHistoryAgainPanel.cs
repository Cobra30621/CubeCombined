using Cube;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Event
{
    public class CreateHistoryAgainPanel : EventPanelBase
    {
        [Required]
        [SerializeField] private CubeData cubeData;
        
        [Required]
        [SerializeField] private Cube.Cube _cube;

        [Required]
        [SerializeField] private TextMeshProUGUI info;

        [Required]
        [SerializeField] private TextMeshProUGUI buttonInfo;


        private int _reward;

        public void ShowPanel(int number, int reward)
        {
            _reward = reward;
            mainPanel.SetActive(true);
            cubeData.SetCubeInfo(_cube, number);
            
            info.text = $"{number} 再次創造\n是否翻倍";
            buttonInfo.text = $"{reward}";
        }

        public void Conform()
        {
            Debug.Log($"Create Again {_reward}");
            Close();
        }
    }
}