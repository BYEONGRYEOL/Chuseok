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
        // UI_MainMenu������ �ȿ� �����ϴ� ������Ʈ �������� �̸��� ��Ȯ�� �Ȱ��� �Ͽ� enum�� ���,
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

            //prefab ���Ͽ� �ִ� ui ������Ʈ�� ���
            Bind<Button>(typeof(Buttons));
            Bind<TextMeshProUGUI>(typeof(TextMeshPro)); 

            //Text ����
            GetText((int)TextMeshPro.Tmpro_Label).GetComponent<TextMeshProUGUI>().text = "�Ĺ�Ȩ";


            // ���� ���� ��ư�� ������ 
            BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, PointerEventData 
                => {
                    Managers.Sound.Play("Button"); //ȿ����
                    Managers.Scene.LoadScene(Enums.Scene.SceneGame);  // �� ����
                    GameManagerEX.Instance.ReStartGame(); // �ʱ�ȭ
                });

            BindEvent(GetButton((int)Buttons.Btn_Mute).gameObject, PointerEventData =>
            {
                Managers.Sound.MuteAll(); // ���Ұ�
            });

            BindEvent(GetButton((int)Buttons.Btn_HowToPlay).gameObject, PointerEventData =>
            {
                Managers.Scene.LoadScene(Enums.Scene.SceneHowToPlay);
            });
        }
    }

}