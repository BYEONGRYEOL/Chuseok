using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;

namespace Isometric
{ 
    public class PoolManager
    {
        //pooling 할 오브젝트의 종류만큼의 Pool Class가 존재할거야
        class Pool
        {
            //어떤 종류의 오브젝트를 담을 것인지?
            public GameObject Original { get; private set; }
            //어디에 담을건지
            public Transform Root { get; set; }

            //pooling 기법은 스택으로 관리
            Stack<Poolable> poolStack = new Stack<Poolable>();

            //초기화 시 original 오브젝트를 10개 생성
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

            // 게임오브젝트를 생성!
            Poolable Create()
            {
                GameObject go = Object.Instantiate<GameObject>(Original);
                go.name = Original.name;
                return go.GetOrAddComponent<Poolable>();
            }

            // 게임 씬에 있던 사용중인 게임오브젝트를 다시 Pool에 넣는 과정
            public void Push(Poolable poolable)
            {
                if(poolable == null)
                {
                    Debug.Log(poolable + "is null");
                    return;
                }

                //DontDestroyOnLoad 상태인 Root 산하에 들어가니까 각각의 오브젝트도 DDOL이 된다.
                poolable.transform.parent = Root;
                poolable.gameObject.SetActive(false);
                poolable.isUsing = false;

            }

            // Pool안에서 비활성화 되어있던 오브젝트를 parent 산하에 빼내기
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
                    //현재 씬 담당 오브젝트, 그러니까 @Scene 산하에 임시로 옴겨낸 다음
                    poolable.transform.parent = Managers.Scene.CurrentScene.transform;
                }
                // parent 를 null 로 설정해 DDOL이 아닌 그냥 씬에 부모오브젝트 없이 존재할 수 있음
                poolable.transform.parent = parent;
                poolable.isUsing = true;

                return poolable;
            }
        }

        Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();

        Transform _root;

        public void Init()
        {
            if(_root = null)
            {
                Debug.Log("이거왜실행한돼");
                _root = new GameObject { name = "@Pool" }.transform;
                Object.DontDestroyOnLoad(_root);
            }
        }

        public void CreatePool(GameObject original, int count = 10)
        {
            Pool pool = new Pool();
            pool.Init(original, count);
            pool.Root.parent = _root;

            poolDict.Add(original.name, pool);
        }
        public void Push(Poolable poolable)
        {

            string name = poolable.gameObject.name;

            if(poolDict.ContainsKey(name) == false)
            {
                //관리하고있는 root Pool 게임오브젝트 산하에 넣고싶은데 없을 때는 그냥 삭제
                GameObject.Destroy(poolable.gameObject);
                return;
            }
            poolDict[name].Push(poolable);
        }

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
            Debug.Log(poolDict);
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