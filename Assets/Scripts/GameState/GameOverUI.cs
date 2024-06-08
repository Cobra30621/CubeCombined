using Sirenix.OdinInspector;
using UnityEngine;

namespace GameState
{
    public class GameOverUI : MonoBehaviour, IGameOverController
    {
        [Required]
        [SerializeField] private GameObject mainPanel;
        

        public void GameOver()
        {
            mainPanel.SetActive(true);
        }
    }
}