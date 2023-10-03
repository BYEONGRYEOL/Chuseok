using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Isometric
{
    public abstract class SceneBase : MonoBehaviour
    {
        // 모든 씬 클래스의 부모 현재 씬에 이벤트시스템이 없다면 생성한다.
        public Enums.Scene SceneType { get; protected set; } = Enums.Scene.SceneLoading;
        protected virtual void Init()
        {
            //Debug.Log("SceneBase Init");
            Object obj = GameObject.FindObjectOfType<EventSystem>();
            if (obj == null)
            {
                Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
            }
        }
        public abstract void Clear();
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update() 
        {

        }
    }

}