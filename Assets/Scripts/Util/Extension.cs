using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Isometric.UI;
using Isometric.Utility;
using Isometric;

//���� �����ϴ� �޼���ó�� ex) List.Count �� Count �޼���ó��) ��� �����ϵ��� ����� Ȯ��޼���
// static Ŭ���� ���� static �޼���� �����Ѵ�.


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

