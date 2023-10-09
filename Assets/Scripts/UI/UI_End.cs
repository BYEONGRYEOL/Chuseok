using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
using Isometric.Utility;
using System;

namespace Isometric.UI
{
    public class UI_End : UI_Scene
    {
        //���θ޴� UI�� �����־���� Item�� enum���� �̸� ����,
        enum Buttons
        {
            Btn_MainMenu
        }
        enum GameObjects 
        {
            TypingEffect
        }


        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();

            // UI���Ͽ� ��ư�� Tmpro�� ������ Dictionary�� �޾� �˰��ֵ���
            Bind<Button>(typeof(Buttons));
            Bind<GameObject>(typeof(GameObjects));
            //Event ����
            BindEvent(GetButton((int)Buttons.Btn_MainMenu).gameObject, PointerEventData
                => {
                    Managers.Sound.Play("Button");
                    Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu); 
                });
            SetEndingText();
        }

        public void SetEndingText()
        {
            int money = GameManagerEX.Instance.Money;
            string time = Math.Round(Managers.Time.PlayingTime, 1).ToString();
            string moneyString = money.ToString();

            string[] endingThings = {"����", "ġŲ", "ĳ���� ��", "��ǻ��" };
            int endingThingIndex = 3;
            if(money < 300000)
            {
                Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString =
                    "�뵷�� 30���� �޾Ҵµ� �� " + moneyString + "�� �ۿ� ���İ� �Ѱܳ���.. \n" + time+"�� �����̳� �Ѱܳ� �־ ���� ����;�ФФ�";
                return;
            }
            else
                Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString = "@3�ʸ��� ���� ���ƿԴ�! \n������ ���� ĳ���� ȯ������ �ȴ�.\n @1�� �������ϱ� �̰ɷ� @2 �����~~";

            if (money - 300000 >= 1000000)
                endingThingIndex = 3;
            else if (money - 300000 >= 100000)
                endingThingIndex = 2;
            else if (money - 300000 >= 20000)
                endingThingIndex = 1;
            else if (money - 300000 >= 10000)
                endingThingIndex = 0;
            
            

            Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString.Replace("@1", moneyString);
            Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString.Replace("@2", endingThings[endingThingIndex]);
            Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString.Replace("@3", time);



        }
    }

}