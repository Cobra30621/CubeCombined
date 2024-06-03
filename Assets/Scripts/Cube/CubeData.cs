using UnityEngine;

namespace Cube
{
    [CreateAssetMenu(fileName = "Cube Data", menuName = "Cube Data")]
    public class CubeData : ScriptableObject
    {
        [SerializeField] private Color[] _colors;

        public void SetCubeInfo(Cube cube, int number)
        {
            cube.SetInfo($"{number}", _colors[number]);
        }

        public string GetCubeName(int number)
        {
            return $"{number}";
        }
}
}