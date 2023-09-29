using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
namespace Isometric.UI
{
    public class UI_MainMenu : UI_Scene
    {
        //메인메뉴 UI가 갖고있어야할 Item들 enum으로 미리 선언,
        enum Buttons
        {
            Btn_Option,
            Btn_Play
        }
        enum TextMeshPro
        {
            Tmpro_Label
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
            Bind<TextMeshProUGUI>(typeof(TextMeshPro));

            //Text 지정
            GetText((int)TextMeshPro.Tmpro_Label).GetComponent<TextMeshProUGUI>().text = "A Cats Day";

            //Event 지정
            BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, PointerEventData 
                => { Managers.Scene.LoadScene(Enums.Scene.SceneGame); });
        }
    }

}