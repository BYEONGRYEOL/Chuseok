using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Utility
{

    public class Util
    {
        // 자식 게임 오브젝트를 찾는 메서드
        public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
        {
            Transform transform = FindChild<Transform>(go, name, recursive); 
            if(transform == null)
            {
                return null;
            }
            return transform.gameObject;
        }


        // go 게임오브젝트의 자식 오브젝트에 컴포넌트까지 부착하여 리턴
        public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if(go== null)
            {
                return null;
            }

            if(recursive == false)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform transform = go.transform.GetChild(i);
                    if(string.IsNullOrEmpty(name) || transform.name == name)
                    {
                        T component = transform.GetComponent<T>();
                        if (component != null)
                        {
                            return component;
                        }
                    }
                }
            }
            else
            {
                foreach(T component in go.GetComponentsInChildren<T>())
                {
                    if ( string.IsNullOrEmpty(name) || component.name == name)
                    {
                        return component;
                    }
                }
            }
            return null;
        }

        public static T GetOrAddComponent<T>(GameObject go) where T: UnityEngine.Component
        {
            T component = go.GetComponent<T>();
            if(component == null)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }

        /*public static bool CustomEndsWith(string a, string b)
        {
            int ap = a.Length - 1;
            int bp = b.Length - 1;

            while (ap >= 0 && bp >= 0 && a[ap] == b[bp])
            {
                ap--;
                bp--;
            }
            return (bp < 0 && a.Length >= b.Length) ||

                    (ap < 0 && b.Length >= a.Length);
        }

        public static bool CustomStartsWith(string a, string b)
        {
            int aLen = a.Length;
            int bLen = b.Length;
            int ap = 0; int bp = 0;

            while (ap < aLen && bp < bLen && a[ap] == b[bp])
            {
                ap++;
                bp++;
            }

            return (bp == bLen && aLen >= bLen) ||

                    (ap == aLen && bLen >= aLen);
        }*/
    }

}