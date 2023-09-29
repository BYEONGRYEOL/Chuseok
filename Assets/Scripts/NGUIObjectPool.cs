using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
namespace Isometric
{

    public class NGUIObjectPool : SingletonDontDestroyMonobehavior<NGUIObjectPool>
    {
        
        [SerializeField] private GameObject poolingObjectPrefab;
        Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();
        protected override void Awake()
        {
            base.Awake();
            Init(10);
        }
        private void Init(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                poolingObjectQueue.Enqueue(CreateNewObject());
            }
        }
        private GameObject CreateNewObject()
        {
            var newObj = Instantiate(poolingObjectPrefab);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }
        public static GameObject GetObject()
        {
            if (Instance.poolingObjectQueue.Count > 0)
            {
                var obj = Instance.poolingObjectQueue.Dequeue();
                
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewObject();
                newObj.gameObject.SetActive(true);
                
                return newObj;
            }
        }
        public static void ReturnObject(GameObject obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            Instance.poolingObjectQueue.Enqueue(obj);
        }

        
    }

}