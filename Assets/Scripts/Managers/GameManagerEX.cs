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


        #region 게임 설정 진행

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

        #region 게임 기능 진행
        public void DropItem(int id, Vector2 startPosition, Vector2 endPosition)
        {
            // enemy.id 와 비교 구조물 트리거 등의 id와도 비교.
            float distance = Vector2.Distance(startPosition, endPosition);
            Vector2 midpoint = new Vector2((startPosition.x + endPosition.x) / 2, 
                (startPosition.y - endPosition.y) / 2 + startPosition.y);


        }
        private void ClickTarget()
        {
            //마우스 좌클릭과 동시에 UI element를 클릭한 경우가 아닌경우에만
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
                // 720초를 기준으로 리셋
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

        #region 초기화
        private void Start()
        {
            //데이터 로드
            myPlayer = GameObject.FindGameObjectWithTag("Player");
            LoadData();
            Init();
        }

        private void Init()
        {
            random = new Random();
            InitTimeState();
            CheckClock();

            
        }
        public void SetUI()
        {

            UI_Game gameUI = Managers.UI.SceneUI as UI_Game;
            if (gameUI == null)
            {
                //캐스팅 실패 -> 현재 ui가 인게임ui가 아님을 이야기하는것
                return;
            }

            
        }

        #endregion
        // Update is called once per frame
        private void Update()
        {
            ClickTarget();
            SetUI();
        }

        #region 시간관련

        public void CheckClock()
        {
            // 낮과 밤이 8분마다 바뀌었으면 해
            // Managers.Time.SetTimer()

            // 시간 받아오기
            ingameTime = Managers.Time.IngameTime;

            if (!isPaused)
            {
                Invoke("CheckClock", 3);
            }
        }
        
        public IEnumerator Evening()
        {
            Debug.Log("Evening");
            //9시전까지
            yield return new WaitUntil(() => ingameTime > 600);

            //12시전까지
            yield return new WaitUntil(() => ingameTime > 660);
            _timeCoroutine = StartCoroutine(Night());
        }
        public IEnumerator Night()
        {
            Debug.Log("Night");

            //2시전까지
            yield return new WaitUntil(() => ingameTime > 60 && ingameTime < 200);

            //6시전까지
            yield return new WaitUntil(() => ingameTime > 180);
            _timeCoroutine = StartCoroutine(Dawn());
        }
        public IEnumerator Dawn()
        {
            Debug.Log("Dawn");

            //9시전까지
            yield return new WaitUntil(() =>ingameTime > 270);

            //12시전까지
            yield return new WaitUntil(() => ingameTime > 360);
            _timeCoroutine = StartCoroutine(Afternoon());
        }

        public IEnumerator Afternoon()
        {
            Debug.Log("Afternoon");

            //15시전까지
            yield return new WaitUntil(() => ingameTime > 450);

            //18시전까지
            yield return new WaitUntil(() => ingameTime > 540);
            _timeCoroutine = StartCoroutine(Evening());
        }
        #endregion
    }
}