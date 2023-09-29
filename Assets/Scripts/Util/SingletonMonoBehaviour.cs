using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.Utility
{
    public abstract class SingletonMonoBehaviour<T> : SingletonMonoBehaviour where T : SingletonMonoBehaviour<T>
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
        }
        protected virtual void Ondestroy()
        {
            instance = null;
        }
    }

    public abstract class SingletonMonoBehaviour : MonoBehaviour
    {

    }

}