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
        //���θ޴� UI�� �����־���� Item�� enum���� �̸� ����,
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

            //Event ����
            BindEvent(GetButton((int)Buttons.Btn_Back).gameObject, PointerEventData 
                => {
                    Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu);
                });

        }
    }

}