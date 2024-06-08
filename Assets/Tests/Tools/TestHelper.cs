using UnityEngine;

namespace Tests.Tools
{
    public static class TestHelper
    {
        public static void ClearScene()
        {
            GameObject[] objects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objects)
            {
                if (obj == null) continue;
                if (obj.GetComponent<Camera>() == null)
                {
                    Object.DestroyImmediate(obj.gameObject);
                }
            }
        }

        public static TestReference GetTestReference()
        {
            return ((GameObject)Resources.Load("TestReference", typeof(GameObject))).GetComponent<TestReference>();
        }
    }
}