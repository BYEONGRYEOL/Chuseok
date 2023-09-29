using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Isometric.UI;
using Isometric.Utility;
using Isometric;

//본래 존재하는 메서드처럼 ex) List.Count 의 Count 메서드처럼) 사용 가능하도록 만드는 확장메서드
// static 클래스 내의 static 메서드로 정의한다.


    public static class Extension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
        {
            return Util.GetOrAddComponent<T>(go);
        }

        public static void BindEvent(this GameObject go, Action<PointerEventData> action, Enums.UIEvent type = Enums.UIEvent.Click)
        {
            UI_Base.BindEvent(go, action, type);
        }
    }

