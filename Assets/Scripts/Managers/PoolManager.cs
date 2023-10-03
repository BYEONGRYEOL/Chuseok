using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;

namespace Isometric
{ 
    public class PoolManager
    {
        class Pool
        {
            // � ������Ʈ�� 
            public GameObject Original { get; private set; }
            //��� ��������
            public Transform Root { get; set; }

            //pooling ����� �������� ����
            Stack<Poolable> poolStack = new Stack<Poolable>();

            //�ʱ�ȭ �� original ������Ʈ�� 10�� ����
            public void Init(GameObject original, int count = 10)
            {
                Original = original;
                Root = new GameObject().transform;
                Root.name = $"{original.name}_Pool";

                for (int i=0; i<count; i++)
                {
                     Push(Create());
                }
            }

            // ���ӿ�����Ʈ�� ������ ����
            Poolable Create()
            {
                GameObject go = Object.Instantiate<GameObject>(Original);
                go.name = Original.name;
                return go.GetOrAddComponent<Poolable>();
            }

            // ���� ���� �ִ� ������� ���ӿ�����Ʈ�� �ٽ� Pool�� �ִ� ����
            public void Push(Poolable poolable)
            {
                if(poolable == null)
                {
                    //Debug.Log(poolable + "is null");
                    return;
                }

                //DontDestroyOnLoad ������ Root ���Ͽ� ���ϱ� ������ ������Ʈ�� DDOL�� �ȴ�.
                poolable.transform.parent = Root;
                poolable.gameObject.SetActive(false);
                poolable.isUsing = false;

            }

            // Pool�ȿ��� ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� parent ���Ͽ� ������
            public Poolable Pop(Transform parent = null)
            {
                Poolable poolable;
                if (poolStack.Count > 0)
                {
                    poolable = poolStack.Pop();
                }
                else
                {
                    poolable = Create();
                }

                poolable.gameObject.SetActive(true);
                if(parent == null)
                {
                    //���� �� ��� ������Ʈ, �׷��ϱ� @Scene ���Ͽ� �ӽ÷� �Ȱܳ� ����
                    poolable.transform.parent = Managers.Scene.CurrentScene.transform;
                }
                // parent �� null �� ������ DDOL�� �ƴ� �׳� ���� �θ������Ʈ ���� ������ �� ����
                // DontdestroyOnLoad�� �־��ٸ�, ���� DontdestroyOnLoad�� �ƴ� ���� ��� ������Ʈ�� �ڽ����� �ٿ��ٰ� ���� dontdestroyonload�� �����ȴ�.
                poolable.transform.parent = parent;
                poolable.isUsing = true;

                return poolable;
            }
        }

        // Ǯ ������ ����
        Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

        Transform _root;

        public void Init()
        {
            if(_root == null)
            {
                _root = new GameObject { name = "@Pool" }.transform;
                Object.DontDestroyOnLoad(_root);
            }
        }

        // ���� �����ڰ� �����ϱ�� �� object�� pooling �����ϸ�, ���� pool�� ��������� ������� Pool ����

        public void CreatePool(GameObject original, int count = 10)
        {
            Pool pool = new Pool();
            pool.Init(original, count);
            pool.Root.parent = _root;

            poolDict.Add(original.name, pool);
        }

        // ���� ���� �ִٰ� ��������� poolable ��ü �ٽ� pool�� push
        public void Push(Poolable poolable)
        {

            string name = poolable.gameObject.name;

            if(poolDict.ContainsKey(name) == false)
            {
                //�����ϰ��ִ� root Pool ���ӿ�����Ʈ ���Ͽ� �ְ������ ���� ���� �׳� ����
                GameObject.Destroy(poolable.gameObject);
                return;
            }
            poolDict[name].Push(poolable);
        }

        //���Ӿ����� ����ϱ����� �ʿ��� object�� pop, ���� �׷��� pool�� �������� �����Ҵٸ� pool�� �����Ѵ�.
        public Poolable Pop(GameObject original, Transform parent = null)
        {
            if(poolDict.ContainsKey(original.name) == false)
            {
                CreatePool(original);
            }
            return poolDict[original.name].Pop(parent);
        }

        public GameObject GetOriginal(string name)
        {
            //Debug.Log(poolDict);
            if (poolDict.ContainsKey(name))
            {
                if (poolDict[name].Original == null)
                    return null;
                return poolDict[name].Original;
            }
            return null;
        }

        public void Clear()
        {
            foreach(Transform childPool in _root)
            {
                GameObject.Destroy(childPool.gameObject);
                poolDict.Clear();
            }
        }
    }

}