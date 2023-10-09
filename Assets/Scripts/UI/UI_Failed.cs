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
    public class UI_Failed : UI_Scene
    {

       
        //���θ޴� UI�� �����־���� Item�� enum���� �̸� ����,
        enum Buttons
        {
            Btn_MainMenu,
            Btn_Retry
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
            
            //
            BindEvent(GetButton((int)Buttons.Btn_MainMenu).gameObject, PointerEventData
                => {
                    Managers.Sound.StopSFX(); // Ÿ�ڼҸ� ȿ���� ����
                    Managers.Sound.Play("Button");
                    Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu); 
                });
            BindEvent(GetButton((int)Buttons.Btn_Retry).gameObject, PointerEventData
                => {
                    Managers.Sound.StopSFX();
                    Managers.Sound.Play("Button");
                    GameManagerEX.Instance.ReStartGame();
                    Managers.Scene.LoadScene(Enums.Scene.SceneGame); 
                });
            SetFailedText();
        }

        public void SetFailedText()
        {
            string time = Math.Round(Managers.Time.PlayingTime, 1).ToString();

            int money = GameManagerEX.Instance.Money;
            Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString =
                "������ ���� �˾ƹ��� ����, \n"+time+"�ʸ��� ���� ���ư��� ���� �����ߴ�. \n������ ���� " + money.ToString() + "������ �����̳� ��Ծ����~";
        }
    }

}