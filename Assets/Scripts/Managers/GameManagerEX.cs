using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Isometric.UI;
using Isometric.Data;
using UnityEngine.EventSystems;

using Isometric.Utility;
using System.Threading;
using System.Security.Cryptography;
using System;
using UnityEngine.AI;
using UnityEditorInternal;
using Random = System.Random;

namespace Isometric
{
    public class GameManagerEX : SingletonDontDestroyMonobehavior<GameManagerEX>
    {

        /*private static GameManager instance;
        public static GameManager Instance{get{return instance;}}
        private void OnDestroy(){if (instance == this){instance = null;}}
        */
        // -16 ~ 16   0 ~ 16    y >= 1/2x and y < 16 - 1/2 x
        public GameObject bullet;
        [SerializeField] private float minX = -3f;
        [SerializeField] private float maxX = 3f;
        [SerializeField] private float minY = 1f;
        [SerializeField] private float maxY = 4f;
        [SerializeField] private float spawnTime_min = 2f;
        [SerializeField] private float spawnTime_Max = 10f;

        [SerializeField] private List<int> spawnables = new List<int>();
        Dictionary<int, bool> isspawning = new Dictionary<int, bool>();

        private GameObject myPlayer;
        Coroutine _timeCoroutine = null;
        Time timenow;
        float timeStart;
        float ingameTime;
        Random random;

        private enum timeState
        {
            Dawn,
            Afternoon,
            Evening,
            Night
        }

        public Vector3 clickPosition;
        private bool isGameOver;
        private bool isPaused;


        #region ���� ���� ����

        public bool IsGameOver { get { return isGameOver; } }
        public bool IsPaused { get => isPaused; }
        private void LoadData()
        {

            ingameTime = Managers.Data.gameData.IngameTime;

        }
        public void MyResume()
        {
            isPaused = false;
            Debug.Log("timeScale is" + Time.timeScale);

        }



        #endregion

        #region ���� ��� ����
        public void DropItem(int id, Vector2 startPosition, Vector2 endPosition)
        {
            // enemy.id �� �� ������ Ʈ���� ���� id�͵� ��.
            float distance = Vector2.Distance(startPosition, endPosition);
            Vector2 midpoint = new Vector2((startPosition.x + endPosition.x) / 2, 
                (startPosition.y - endPosition.y) / 2 + startPosition.y);


            if (Managers.Data.EnemyStatDict.ContainsKey(id))
            {
                // ���� �׾ �������� ���ž� �ϴ� ���
                // Ȯ�� ���� �����ؼ� �������� �ش� ��ġ�� ���ž߰ڴ�.
            }
        }
        private void ClickTarget()
        {
            //���콺 ��Ŭ���� ���ÿ� UI element�� Ŭ���� ��찡 �ƴѰ�쿡��
            if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero, Mathf.Infinity, 512);
                
                if(hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        clickPosition = hit.transform.position;
                    }
                }
            }
        }
        
        public void InitTimeState()
        {
            {
                // 720�ʸ� �������� ����
                // 22~06 night 06 ~ 12 dawn 12 ~ 18 afternoon 18~22 evening
                if (ingameTime < 360 && ingameTime > 180)
                {
                    // dawn
                    _timeCoroutine = StartCoroutine(Dawn());
                }
                else if (ingameTime < 540 && ingameTime > 360)
                {
                    // afternoon
                    _timeCoroutine = StartCoroutine(Afternoon());
                }
                else if (ingameTime < 660 && ingameTime > 540)
                {
                    // evening
                    _timeCoroutine = StartCoroutine(Evening());
                }
                else
                {
                    //night
                    _timeCoroutine = StartCoroutine(Night());
                }
            }
        }

        #endregion

        #region �ʱ�ȭ
        private void Start()
        {
            //������ �ε�
            myPlayer = GameObject.FindGameObjectWithTag("Player");
            LoadData();
            Init();
        }

        private void Init()
        {
            random = new Random();
            InitTimeState();
            CheckClock();

            spawnables.Add(2);
            spawnables.Add(107);
            foreach(int i in spawnables)
            {
                isspawning.Add(i, false);
            }

            bullet = Resources.Load<GameObject>("Prefabs/InGame/Bullet");
            Managers.Pool.CreatePool(bullet, 100);
            

        }
        public void SetUI()
        {

            UI_Game gameUI = Managers.UI.SceneUI as UI_Game;
            if (gameUI == null)
            {
                //ĳ���� ���� -> ���� ui�� �ΰ���ui�� �ƴ��� �̾߱��ϴ°�
                return;
            }

            UI_Inventory inventory = gameUI.inventory;
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isActiveAndEnabled)
                {
                    inventory.gameObject.SetActive(false);
                }
                else
                {
                    inventory.gameObject.SetActive(true);
                    inventory.RefreshSlot();
                }
            }
        }

        #endregion
        // Update is called once per frame
        private void Update()
        {
            ClickTarget();
            SetUI();
            SpawnCheck();
            BulletPooling();
        }

        void BulletPooling()
        {
            Managers.Pool.Pop(bullet);
            
            
        }

        #region �� ����

        public void SpawnCheck()
        {
            foreach(int i in spawnables)
            {
                if(!IsSpawning(i) && Managers.Object.ObjectSpawnLimit[i] > Managers.Object.Count(i))
                {
                    float spawntime = UnityEngine.Random.Range(spawnTime_min, spawnTime_Max);
                    StartCoroutine(SpawnEnemy(i, spawntime));
                }
            }
        }
        private bool IsSpawning(int key, bool? spawning = null)
        {
            if(spawning == null)
            {
                return isspawning[key];
            }
            
            isspawning[key] = (bool)spawning;

            return (bool)spawning;
        }

        private IEnumerator SpawnEnemy(int key, float spawntime)
        {
            IsSpawning(key, spawning: true);

            yield return new WaitForSeconds(spawntime);
            
            string name = Managers.Data.EnemyStatDict[key].name;
            GameObject go = Managers.Resource.Instantiate("Ingame/" + name);

            Vector3 randPos = new Vector3(0,0,0);
            int limit = 20;
            while (limit > 0)
            {
                float randomX = UnityEngine.Random.Range(minX, maxX);
                float randomY = UnityEngine.Random.Range(minY, maxY);

                randPos = new Vector3(randomX, randomY, 0);
                
                limit -= 1;
            }
            go.transform.position = randPos;
            Managers.Object.Add(key, go);
            IsSpawning(key, spawning: false);

        }
        #endregion
        #region �ð�����

        public void CheckClock()
        {
            // ���� ���� 8�и��� �ٲ������ ��
            // Managers.Time.SetTimer()

            // �ð� �޾ƿ���
            ingameTime = Managers.Time.IngameTime;

            if (!isPaused)
            {
                Invoke("CheckClock", 3);
            }
        }
        
        public IEnumerator Evening()
        {
            Debug.Log("Evening");
            //9��������
            yield return new WaitUntil(() => ingameTime > 600);

            //12��������
            yield return new WaitUntil(() => ingameTime > 660);
            _timeCoroutine = StartCoroutine(Night());
        }
        public IEnumerator Night()
        {
            Debug.Log("Night");

            //2��������
            yield return new WaitUntil(() => ingameTime > 60 && ingameTime < 200);

            //6��������
            yield return new WaitUntil(() => ingameTime > 180);
            _timeCoroutine = StartCoroutine(Dawn());
        }
        public IEnumerator Dawn()
        {
            Debug.Log("Dawn");

            //9��������
            yield return new WaitUntil(() =>ingameTime > 270);

            //12��������
            yield return new WaitUntil(() => ingameTime > 360);
            _timeCoroutine = StartCoroutine(Afternoon());
        }

        public IEnumerator Afternoon()
        {
            Debug.Log("Afternoon");

            //15��������
            yield return new WaitUntil(() => ingameTime > 450);

            //18��������
            yield return new WaitUntil(() => ingameTime > 540);
            _timeCoroutine = StartCoroutine(Evening());
        }
        #endregion
    }
}