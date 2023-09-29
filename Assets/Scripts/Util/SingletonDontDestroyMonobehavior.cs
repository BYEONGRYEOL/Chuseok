using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Utility 
{
    public abstract class SingletonDontDestroyMonobehavior<T> : SingletonDontDestroyMonobehavior where T : SingletonDontDestroyMonobehavior<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }
        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = (T)this;
            }
            if(Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        protected virtual void Ondestroy()
        {
            if(instance == this)
                instance = null;
        }
    }

    public abstract class SingletonDontDestroyMonobehavior : MonoBehaviour
    {

    }
}


    

