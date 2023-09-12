using UnityEngine;

namespace _GameLogic.Extension
{
    [DisallowMultipleComponent]
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }
        
        public bool DontDestroyOnLoad = true;

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            if (Instance == null)
            {
                Instance = this as T;

                if (transform.parent == null && DontDestroyOnLoad) 
                {
                    DontDestroyOnLoad (gameObject);
                }
            }
            else if (Instance != this)
            {
                DestroyImmediate(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}