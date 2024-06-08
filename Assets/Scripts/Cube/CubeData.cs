using UnityEngine;

namespace Cube
{
    public enum InfoType
    {
        Simple,
        G2048
    }
    
    [CreateAssetMenu(fileName = "Cube Data", menuName = "Cube Data")]
    public class CubeData : ScriptableObject
    {
        [SerializeField] private Color[] _colors;

        public void SetCubeInfo(Cube cube, int number, InfoType type = InfoType.Simple)
        {
            cube.SetInfo($"{number}", _colors[number]);
        }

        public string GetCubeName(int number)
        {
            return $"{number}";
        }
}
}