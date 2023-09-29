using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using System;
namespace Isometric
{

    public class MultipleObjectPool : SingletonDontDestroyMonobehavior<MultipleObjectPool>
    {
        public GameObject[] poolingObjectPrefabs;
        public int initCount = 10;
        private Dictionary<object, Queue<GameObject>> poolingObjectsDict = new Dictionary<object, Queue<GameObject>>();
        protected override void Awake()
        {
            base.Awake();
            Init(10);
        }

        public enum keyList
        {
            Enemy_HP,
            Enemy_DamageText
        }
        private void Init(int initCount)
        {
            for (int i = 0; i < poolingObjectPrefabs.Length; i++)
            {
                for (int j = 0; j < initCount; j++)
                {
                    if (!poolingObjectsDict.ContainsKey(poolingObjectPrefabs[i].name))
                    {
                        Queue<GameObject> newQueue = new Queue<GameObject>();
                        poolingObjectsDict.Add(poolingObjectPrefabs[i].name, newQueue);
                    }
                    poolingObjectsDict[poolingObjectPrefabs[i].name].Enqueue(CreateNewObject(i));   
                }
            }

        }
        private GameObject CreateNewObject(int i)
        {
            var newObj = Instantiate(poolingObjectPrefabs[i]);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }
        private GameObject CreateNewObject(string name)
        {
            var newObj = Instantiate(poolingObjectPrefabs[Array.IndexOf(poolingObjectPrefabs, name)]);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);
            return newObj;
        }
        public static GameObject GetObject(string name)
        {

            if (Instance.poolingObjectsDict.ContainsKey(name))
            {
                if (Instance.poolingObjectsDict[name].Count > 0)
                {
                    var obj = Instance.poolingObjectsDict[name].Dequeue();
                    obj.transform.SetParent(null);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
                else
                {
                    var newObj = Instance.CreateNewObject(name);
                    newObj.gameObject.SetActive(true);
                    newObj.transform.SetParent(null);
                    return newObj;
                }
            }
            else
            {
                Debug.Log("오브젝트풀 호출 이름 오류");
                return null;
            }
        }
        public static void ReturnObject(GameObject obj, string name)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(Instance.transform);
            Instance.poolingObjectsDict[name].Enqueue(obj);
        }
    }

}