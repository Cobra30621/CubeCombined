using UnityEngine;

namespace Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        // Singleton instance property.
        public static T Instance
        {
            get
            {
                // If instance is not assigned, try to find it in the scene.
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    // If instance is still not found, log an error.
                    if (instance == null)
                    {
                        Debug.LogWarning($"The GameObject of type {typeof(T)} is not present in the scene, " +
                                       $"yet its method is being called. Please add {typeof(T)} to the scene.");
                        instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                    else
                    {
                        // If instance is found, ensure it persists across scene changes.
                        DontDestroyOnLoad(instance);
                    }
                }
                return instance;
            }
        }

        // Called when the instance is first created.
        protected virtual void Awake()
        {
            // If instance is not assigned, assign this instance to it and make it persistent.
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                Initialize(); // Customizable method called at Awake.
            }
            else
            {
                // If instance is already assigned, destroy this instance.
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        // Customizable method that can be overridden in derived classes.
        protected virtual void Initialize()
        {
            // Implement any specific behavior at Awake if needed.
        }

        public static bool HasInstance()
        {
            return instance != null;
        }
    }
}