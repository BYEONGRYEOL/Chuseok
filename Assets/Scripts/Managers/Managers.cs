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

        //MonoBehaviour�� �ʿ���� �ٸ� �Ŵ��� Ŭ�������� �ѹ��� ��� ����, ��ü �Ŵ��� Ŭ���� ��ΰ� �̱������� ����ȰͰ� ���� �����̴�.
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
            //Debug.Log("Managers :: Awake function ����?");
        }

        void Update()
        {
            //�Ŵ����� �� Monobehavior�� ��ӹ���������, Update�� ���� ����� ����ϰ� ���� ���
            _input.OnUpdate();
            _time.OnUpdate();
        }

        public static void Init()
        {
            //�̱���
            if(instance == null)
            {
                // ���� @Managers ���ӿ�����Ʈ�� ���ٸ� ����, �ִٸ�, Dontdetroyonload ���
                GameObject go = GameObject.Find("@Managers");
                if(go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<Managers>();

                //�� Ŭ������ �ʱ�ȭ�ɶ� ����մ� �ٸ� �Ŵ���Ŭ�����鵵 �ʱ�ȭ
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