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
                // UI_Root ������Ʈ �� �ڽ����� Popup���� ���� ���ؼ�
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
            // overlay�� cameramode �ε� UI�� �� 3D �ű��ϰ��Ұžƹ̳� ������ overlay�ϱ� 
            //canvas�� �ٷ� �� overlay�� ������ִ� ���� ������ �ȵȴ�.
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //��ø�Ͽ� canvas�� ����ǵ�, �켱 �ڽ�ĵ������ sortingOrder ���� �θ�ĵ������ sortingOrder�� �����ϴ� 
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

        //��ɱ����� �� Script�� �����ִ� UI�� Item���� ������ �� ����
        //Script�� �޸��� ���� UI�� Item�� �������� �͵� �ѹ� �غ���???

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
            Debug.Log(name + "UI ����");
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
                //�ߺ��Ұ����ٰ� �˾�ui�� �̹� �����ϸ� �ȸ����
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