using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Isometric.Utility;
using TMPro;

namespace Isometric.UI
{
    public class UI_Base : MonoBehaviour
    {

        // UnityEngine.Object는 유니티 엔진에서 모든 오브젝트와 컴포넌트들의 부모클래스, 그니까 조상이다.
        protected Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            // Enum형식으로 받아온 Bind할 객체의 타입을 이용해 객체 이름배열로 
            string[] names = Enum.GetNames(type);
            UnityEngine.Object[] temp_objects = new UnityEngine.Object[names.Length];
            // objects 딕셔너리에 key는 타입, value는 타입별 오브젝트 빈 배열
            objects.Add(typeof(T), temp_objects);

            for (int i = 0; i < names.Length; i++)
            {
                // 해당하는 type에 맞게 (Ex: 게임오브젝트는 그냥 게임오브젝트, 버튼은 버튼 컴포넌트가 추가된채로,
                // 아까 추가한 타입별 오브젝트 빈배열에 추가한다.
                if (typeof(T) == typeof(GameObject))
                {
                    temp_objects[i] = Util.FindChild(gameObject, names[i], true);
                }
                else
                {
                    temp_objects[i] = Util.FindChild<T>(gameObject, names[i], true);
                }
                if (temp_objects[i] == null)
                {
                    //Debug.Log($"Failed to bind :: {names[i]}");
                }
            }
        }

        //지정해주는 UIEvent에 맞게 동적으로 Event를 등록해주는 함수
        public static void BindEvent(GameObject go, Action<PointerEventData> action, Enums.UIEvent type = Enums.UIEvent.Click)
        {
            UI_EventHandler myEvent = Util.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case Enums.UIEvent.Click:
                    myEvent.OnClickHandler -= action;
                    myEvent.OnClickHandler += action;
                    break;
                case Enums.UIEvent.Drag:
                    myEvent.OnDragHandler -= action;
                    myEvent.OnDragHandler += action;
                    break;
                case Enums.UIEvent.BeginDrag:
                    myEvent.OnBeginDragHandler -= action;
                    myEvent.OnBeginDragHandler += action;
                    break;
                case Enums.UIEvent.EndDrag:
                    myEvent.OnEndDragHandler -= action;
                    myEvent.OnEndDragHandler += action;
                    break;
            }
        }
        public virtual void Init()
        {

        }
        protected T Get<T>(int index) where T : UnityEngine.Object
        {
            UnityEngine.Object[] temp_objects = null;
            if (objects.TryGetValue(typeof(T), out temp_objects) == false)
            {
                return null;
            }
            return temp_objects[index] as T;
        }
        protected GameObject GetObject(int index) { return Get<GameObject>(index); }
        protected TextMeshProUGUI GetText(int index) { return Get<TextMeshProUGUI>(index); }
        protected Button GetButton(int index) { return Get<Button>(index); }
        protected Image GetImage(int index) { return Get<Image>(index); }
    }

}