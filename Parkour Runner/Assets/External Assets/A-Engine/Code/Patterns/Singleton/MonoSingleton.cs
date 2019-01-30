// A-Engine, Code version: 1

using UnityEngine;

namespace AEngine
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        //instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
						instance = new GameObject(typeof(T).ToString()).AddComponent<T> ();

                        if (instance == null)
                            Debug.LogError("[Class: MonoSingleton] Problem during the creation of " + typeof(T).ToString());
                        else
                            instance.Init();
                    }
                    else
                        instance.Init();
                }

                return instance;
            }
        }

		protected virtual void Init() { }
                
        private void Awake()
        {
            if (instance == null)
                instance = this as T;
        }        

        private void OnApplicationQuit()
        {
            instance = null;
        }
    }
}