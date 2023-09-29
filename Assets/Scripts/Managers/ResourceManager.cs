using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class ResourceManager
    {
        public T Load<T>(string filepath) where T: Object
        {
            if(typeof(T) == typeof(GameObject))
            {
                string name = filepath;
                int index = name.LastIndexOf("/");
                Debug.Log(name);
                if (index > 0)
                    name = name.Substring(index + 1);

                GameObject go = Managers.Pool.GetOriginal(name);
                if (go != null)
                    return go as T;
            }
            return Resources.Load<T>(filepath);
        }

        //Instantiate 사용시, 어차피 게임오브젝트라서 생성한다는 의미로 Instantiate 라고 했음
        //그러니까 다른 스크립트에서 사용 시 경로에 Prefab/ 를 추가할 필요가 없음
        public GameObject Instantiate(string filepath, Transform parent = null)
        {
            //메모리에만 오브젝트를 들고있는 상태
            GameObject original = Load<GameObject>($"Prefabs/{filepath}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {filepath}");
                return null;
            }

            
            // 실제 게임씬에 생성하는 부분

            if(original.GetComponent<Poolable>() != null)
            {
                return Managers.Pool.Pop(original, parent).gameObject;
            }


            GameObject go =  Object.Instantiate(original, parent);
            // 이름에 (Clone) 붙는거 싫어서
            go.name = original.name;

            return go;
            
        }

        public void Destroy(GameObject go)
        {
            if(go == null)
            {
                Debug.Log($"Failed to Destroy GameObject : {go.name}");
                return;
            }

            Poolable poolable = go.GetComponent<Poolable>();
            if(poolable != null)
            {
                Managers.Pool.Push(poolable);
                return;
            }

            Object.Destroy(go);
        }
    }

}