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
                //ĳ���� ���� -> ���� ui�� �ΰ���ui�� �ƴ��� �̾߱��ϴ°�
                return;
            }
        }

        private void Update()
        {
            SetUI();

            // Invoke, Coroutine ���� �ɷ� �����Ϸ������� �ڲ� ���� �߻����� ��¿�� ���� update�ȿ��� �˻��ϱ���ߴ�..
            if(Managers.Time.PlayingTime > timeLimit)
            {
                timeLimit += 30f;
                difficulty++;
                BulletManager.Instance.difficulty = difficulty;
            }

            // ���� ����
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

    }
}