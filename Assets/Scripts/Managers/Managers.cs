using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
using Isometric.Data;
using Isometric.Utility;

namespace Isometric
{
    public class Managers : MonoBehaviour
    {

        static Managers instance;
        static Managers Instance { get { return instance; } }

        //MonoBehaviour가 필요없는 다른 매니저 클래스들을 한번에 모두 관리, 전체 매니저 클래스 모두가 싱글톤으로 선언된것과 같은 느낌이다.
        InputManager _input = new InputManager();
        ResourceManager _resource = new ResourceManager();
        UIManager _ui = new UIManager();
        MySceneManager _scene = new MySceneManager();
        DataManager _data = new DataManager();
        PoolManager _pool = new PoolManager();
        TimeManager _time = new TimeManager();
        RandomNumberManager _random = new RandomNumberManager();
        SoundManager _sound = new SoundManager();


        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static MySceneManager Scene { get { return Instance._scene; } }
        public static DataManager Data { get { return Instance._data; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static TimeManager Time { get { return Instance._time; } }
        public static RandomNumberManager Random { get { return Instance._random; } }
        public static SoundManager Sound { get { return Instance._sound; } }

        void Awake()
        {
            Init();
            //Debug.Log("Managers :: Awake function 실행?");
        }

        void Update()
        {
            //매니저들 중 Monobehavior를 상속받지않지만, Update와 같은 기능을 사용하고 싶은 경우
            _input.OnUpdate();
            _time.OnUpdate();
        }

        public static void Init()
        {
            //싱글톤
            if(instance == null)
            {
                // 씬에 @Managers 게임오브젝트가 없다면 생성, 있다면, Dontdetroyonload 등록
                GameObject go = GameObject.Find("@Managers");
                if(go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<Managers>();

                //이 클래스가 초기화될때 들고잇는 다른 매니저클래스들도 초기화
                instance._pool.Init();
                instance._data.Init();
                instance._random.Init();
                instance._sound.Init();

            }
        }
        
        public static void Clear()
        {
            Input.Clear();
            Pool.Clear();
        }

        

    } 
}