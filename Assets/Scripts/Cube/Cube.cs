using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube
{
    public class Cube : MonoBehaviour
    {

        public TextMeshProUGUI text;
        [SerializeField] private Image image;
        
        
        
        public void SetInfo(string info, Sprite sprite)
        {
            text.text = info;
            image.sprite = sprite;
        }
        
        public void SetInfo(string info, Color color)
        {
            text.text = info;
            image.color = color;
        }

        public void SetSelected(bool selected)
        {
            var alpha = selected ? 0.5f : 1.0f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }
}