using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace Isometric.UI
{
    public class UI_Game : UI_Scene
    {

        public UI_Option option;
        public Image joyStick;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI difficultyText;

        private float currentFill;
        enum GameObjects
        {
            StatGroup
        }
        enum Images
        {
            JoyStick
        }

        enum TextMeshProUGUIs
        { 
            Txt_Money,
            Txt_Time,
            Txt_Difficulty
        }
        private void Update()
        {
            //���� ���� �ð� ����� ����� update�� �ƴ϶� �ݹ� �������� ������Ʈ ���ִ°� ���� �ٵ� �ð��� ��� �켱��...�Ф�
            moneyText.text = GameManagerEX.Instance.Money.ToString();
            timeText.text = Math.Round(Managers.Time.PlayingTime, 1).ToString();
            difficultyText.text = GameManagerEX.Instance.difficulty.ToString();
        }

        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
            Bind<Image>(typeof(Images));

            joyStick = GetImage((int)Images.JoyStick);
            moneyText = GetText((int)TextMeshProUGUIs.Txt_Money);
            timeText = GetText((int)TextMeshProUGUIs.Txt_Time); 
            difficultyText = GetText((int)TextMeshProUGUIs.Txt_Difficulty);

            moneyText.text = "0";
            
            if(Managers.Data.gameData.IsMobile == false)
            {
                joyStick.gameObject.SetActive(false);
            }
        }
    }
}