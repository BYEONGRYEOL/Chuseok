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
    public class UI_HowToPlay: UI_Scene
    {
        //메인메뉴 UI가 갖고있어야할 Item들 enum으로 미리 선언,
        enum Buttons
        {
            Btn_Back
        }

        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();

            Bind<Button>(typeof(Buttons));

            //Event 지정
            BindEvent(GetButton((int)Buttons.Btn_Back).gameObject, PointerEventData 
                => {
                    Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu);
                });

        }
    }

}