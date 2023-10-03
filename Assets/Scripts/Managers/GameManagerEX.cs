using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
using Isometric.Utility;
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

        public float timeLimit = 30f;
        public float ingameTime;
        public int difficulty = 1;
        int money;
        public int Money
        {
            get => money;
            set => money = value;
        }
        

        public Vector3 clickPosition;


        public void ReStartGame()
        {
            Managers.Sound.Play("BGM", Enums.Sound.Bgm);
            Init();
            PlayerController.Instance.Reset();
            timeLimit = 30f;
            difficulty = 1;
        }


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            Money = 5000;
            Managers.Time.StartGameTimer();
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

        private void Update()
        {
            SetUI();

            // Invoke, Coroutine 같은 걸로 구현하려했으나 자꾸 오류 발생으로 어쩔수 없이 update안에서 검사하기로했다..
            if(Managers.Time.PlayingTime > timeLimit)
            {
                timeLimit += 30f;
                difficulty++;
                BulletManager.Instance.difficulty = difficulty;
            }

            // 게임 끄기
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

    }
}