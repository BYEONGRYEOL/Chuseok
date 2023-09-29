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

        //Instantiate ����, ������ ���ӿ�����Ʈ�� �����Ѵٴ� �ǹ̷� Instantiate ��� ����
        //�׷��ϱ� �ٸ� ��ũ��Ʈ���� ��� �� ��ο� Prefab/ �� �߰��� �ʿ䰡 ����
        public GameObject Instantiate(string filepath, Transform parent = null)
        {
            //�޸𸮿��� ������Ʈ�� ����ִ� ����
            GameObject original = Load<GameObject>($"Prefabs/{filepath}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {filepath}");
                return null;
            }

            
            // ���� ���Ӿ��� �����ϴ� �κ�

            if(original.GetComponent<Poolable>() != null)
            {
                return Managers.Pool.Pop(original, parent).gameObject;
            }


            GameObject go =  Object.Instantiate(original, parent);
            // �̸��� (Clone) �ٴ°� �Ⱦ
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