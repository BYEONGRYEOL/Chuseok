using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
namespace Isometric.UI
{

    public class UIManager
    {
        int _sortOrder = 10;

        Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
        public UI_Scene SceneUI { get; private set; }

                
        public GameObject Root
        {
            get
            {
                // UI_Root 오브젝트 의 자식으로 Popup들을 띄우기 위해서
                GameObject root = GameObject.Find("@UI_Root");
                if (root == null)
                {
                    root = new GameObject { name = "@UI_Root" };
                }
                return root;
            }

        }

        public void SetCanvas(GameObject go, bool sort = false)
        {
            Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
            // overlay나 cameramode 인데 UI를 막 3D 신기하게할거아미녀 무조건 overlay니까 
            //canvas를 다룰 때 overlay로 만들어주는 것을 잊으면 안된다.
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //중첩하여 canvas를 만들건데, 우선 자식캔버스의 sortingOrder 값을 부모캔버스의 sortingOrder로 설정하는 
            canvas.overrideSorting = true; 

            if (sort)
            {
                canvas.sortingOrder = _sortOrder;
                _sortOrder++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        //기능구현이 된 Script를 갖고있는 UI내 Item만을 가져올 수 있음
        //Script가 달리지 않은 UI내 Item을 가져오는 것도 한번 해볼까???

        public T GetUIItem<T>(Transform parent, string name = null) where T : UI_Base
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }
            GameObject go = Managers.Resource.Instantiate($"UI/Items/{name}");
            if(parent != null)
            {
                go.transform.SetParent(parent);
            }
            T item = Util.GetOrAddComponent<T>(go);

            return item;

        }
        public void CloseSceneUI(string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = SceneUI.name;
            }
            if(SceneUI.name != name)
            {
                return;
            }
            GameObject go = Root.transform.Find(name).gameObject;
            GameObject.Destroy(go);
        }
        public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        {
            if(SceneUI != null)
            {
                CloseSceneUI(SceneUI.name);
            }
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
            Debug.Log(name + "UI 생성");
            T scene = Util.GetOrAddComponent<T>(go);
            SceneUI = scene;

            
            go.transform.SetParent(Root.transform);

            
            return scene;
        }
        public T ShowPopupUI<T>(bool duplicatable = true, string name = null) where T : UI_Popup
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
            T popup = Util.GetOrAddComponent<T>(go);
            if(!duplicatable && _popupStack.Contains(popup))
            {
                //중복불가에다가 팝업ui가 이미 존재하면 안만들어
                GameObject.Destroy(go);
                return null;
            }
            _popupStack.Push(popup);

            
            go.transform.SetParent(Root.transform);


            return popup;
        }
        
        
        public void ClosePopupUI(UI_Popup popup)
        {
            if(_popupStack.Count == 0)
            {
                return;
            }

            if(_popupStack.Peek() != popup)
            {
                Debug.Log("Close late Popup Failed!"); 
            }

            ClosePopupUI();
        }
        

        public void ClosePopupUI()
        {
            if(_popupStack.Count == 0)
            {
                return;
            }

            UI_Popup popup = _popupStack.Pop();
            Managers.Resource.Destroy(popup.gameObject);
            popup = null;

            _sortOrder--;
        }
        
        public void CloseAllPopupUI()
        {
            while(_popupStack.Count > 0)
            {
                ClosePopupUI();
            }
        }
    }

}