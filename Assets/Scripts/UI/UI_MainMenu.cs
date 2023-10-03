using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
using Isometric.Utility;
using UnityEditor;

namespace Isometric.UI
{
    public class UI_MainMenu : UI_Scene
    {
        // UI_MainMenu프리팹 안에 존재하는 컴포넌트 종류별로 이름을 정확히 똑같이 하여 enum에 기록,
        enum Buttons
        {
            Btn_Mute,
            Btn_Play,
            Btn_HowToPlay
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
            Managers.Sound.Play("BGM", Enums.Sound.Bgm);
            base.Init();

            //prefab 산하에 있는 ui 컴포넌트들 등록
            Bind<Button>(typeof(Buttons));
            Bind<TextMeshProUGUI>(typeof(TextMeshPro)); 

            //Text 지정
            GetText((int)TextMeshPro.Tmpro_Label).GetComponent<TextMeshProUGUI>().text = "컴백홈";


            // 게임 시작 버튼을 누르면 
            BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, PointerEventData 
                => {
                    Managers.Sound.Play("Button"); //효과음
                    Managers.Scene.LoadScene(Enums.Scene.SceneGame);  // 씬 변경
                    GameManagerEX.Instance.ReStartGame(); // 초기화
                });

            BindEvent(GetButton((int)Buttons.Btn_Mute).gameObject, PointerEventData =>
            {
                Managers.Sound.MuteAll(); // 음소거
            });

            BindEvent(GetButton((int)Buttons.Btn_HowToPlay).gameObject, PointerEventData =>
            {
                Managers.Scene.LoadScene(Enums.Scene.SceneHowToPlay);
            });
        }
    }

}