using Sirenix.OdinInspector;
using UnityEngine;

namespace GameState
{
    public class GameOverUI : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject mainPanel;

        public void ShowPanel()
        {
            mainPanel.SetActive(true);
        }
    }
}