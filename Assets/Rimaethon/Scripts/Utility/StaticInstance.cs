using System;
using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
 
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance =FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.LogError($"Instance of type {typeof(T)} could not be found.");
                }
                return _instance;
            }
            protected set => _instance = value;
        }

        protected virtual void Awake()
        {
            InitializeInstance();
        }

        protected virtual void OnApplicationQuit()
        {
            _instance = null;
            Destroy(gameObject);
        }

        private void InitializeInstance()
        {
            if (this is T instance)
            {
                Instance = instance;
                Debug.Log($"Instance of type {typeof(T)} created.");
            }
            else
            {
                Debug.LogError($"Instance of type {typeof(T)} could not be created.");
                throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
            }
        }
    }

  public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
  {
      protected override void Awake()
      {
          base.Awake();
          if (this is T instance)
          {
              if (Instance != null && Instance != this)
              {
                  Destroy(gameObject);
              }
              else
              {
                  Instance = instance;
              }
          }
          else
          {
              Debug.LogError($"Instance of type {typeof(T)} could not be created.");
              throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
          }
      }
  }


    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}