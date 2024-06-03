using Cube;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Event
{
    public class ShootingChangePanel : EventPanelBase
    {
        [Required]
        [SerializeField] private CubeData cubeData;
        [SerializeField] private Cube.Cube _cube;


        public void ShowPanel(int number)
        {
            mainPanel.SetActive(true);
            
            cubeData.SetCubeInfo(_cube, number);
        }
    }
}