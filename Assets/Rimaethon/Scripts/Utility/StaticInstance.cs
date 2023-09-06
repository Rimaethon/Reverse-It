using System;
using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
 
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (this is T instance)
            {
                Instance = instance;
            }
            else
            {
                Debug.LogError($"Instance of type {typeof(T)} could not be created.");
                throw new InvalidOperationException($"Instance of type {typeof(T)} could not be created.");
            }
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
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