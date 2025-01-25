using UnityEngine;

namespace Managers
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning($"More than one instance of {this.GetType().Name}!");
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }

    }
}
