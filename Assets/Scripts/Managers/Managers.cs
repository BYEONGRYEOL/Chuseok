using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
using Isometric.Data;

namespace Isometric
{
    public class Managers : MonoBehaviour
    {

        static Managers instance;
        static Managers Instance { get { Init(); return instance; } }


        KeyBindManager _keyBind = new KeyBindManager();
        InputManager _input = new InputManager();
        ResourceManager _resource = new ResourceManager();
        UIManager _ui = new UIManager();
        MySceneManager _scene = new MySceneManager();
        DataManager _data = new DataManager();
        PoolManager _pool = new PoolManager();
        TimeManager _time = new TimeManager();
        ObjectManager _object = new ObjectManager();
        RandomNumberManager _random = new RandomNumberManager();
        public static ObjectManager Object { get { return Instance._object; } }
        public static KeyBindManager KeyBind { get { return Instance._keyBind; } }
        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static MySceneManager Scene { get { return Instance._scene; } }
        public static DataManager Data { get { return Instance._data; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static TimeManager Time { get { return Instance._time; } }
        public static RandomNumberManager Random { get { return Instance._random; } }
        void Awake()
        {
            Init();
            Debug.Log("Managers :: Awake function ����?");
        }

        void Update()
        {
            //�Ŵ����� �� Monobehavior�� ��ӹ���������, Update�� ���� ����� ����ϰ� ���� ���
            _input.OnUpdate();
            _time.OnUpdate();
        }

        static void Init()
        {
            // ���� Manager ���� ������Ʈ�� ���ٸ�
            if(instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if(go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<Managers>();

                //Managers ��ũ��Ʈ�� �ʱ�ȭ�ɶ���
                instance._pool.Init();
                
                instance._data.Init();
                
                instance._keyBind.Init();

                instance._object.Init();


                instance._random.Init();
            }
        }
        
        public static void Clear()
        {
            Input.Clear();
            Pool.Clear();
        }

        

    } 
}