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

       
        //메인메뉴 UI가 갖고있어야할 Item들 enum으로 미리 선언,
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

            // UI산하에 버튼과 Tmpro가 있음을 Dictionary로 받아 알고있도록
            Bind<Button>(typeof(Buttons));
            Bind<GameObject>(typeof(GameObjects));
            
            //
            BindEvent(GetButton((int)Buttons.Btn_MainMenu).gameObject, PointerEventData
                => {
                    Managers.Sound.StopSFX(); // 타자소리 효과음 멈춤
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
                "송편의 맛을 알아버린 나는, \n"+time+"초만에 집에 돌아가는 것을 포기했다. \n열심히 모은 " + money.ToString() + "원으로 송편이나 사먹어야지~";
        }
    }

}