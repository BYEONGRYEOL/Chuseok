using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
using Isometric.Utility;

namespace Isometric.UI
{
    public class UI_End : UI_Scene
    {
        //메인메뉴 UI가 갖고있어야할 Item들 enum으로 미리 선언,
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

            // UI산하에 버튼과 Tmpro가 있음을 Dictionary로 받아 알고있도록
            Bind<Button>(typeof(Buttons));
            Bind<GameObject>(typeof(GameObjects));
            //Event 지정
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
            string moneyString = money.ToString();

            string[] endingThings = {"과자", "치킨", "캐쉬나 더", "컴퓨터" };
            int endingThingIndex = 3;
            if(money < 300000)
            {
                Get<GameObject>((int)GameObjects.TypingEffect).GetComponent<TypingEffect>().TypingString =
                    "용돈을 30만원 받았는데 왜 " + moneyString + "원 밖에 없냐고 쫓겨났다.. 집에 가고싶어ㅠㅠㅠ";
                return;
            }
            

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


        }
    }

}