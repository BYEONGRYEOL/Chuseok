using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class SceneMainMenu : SceneBase
    {
        public override void Clear()
        {
            
        }

        protected override void Init()
        {
            //�Ŵ����� ó�� �ҷ��� ���������ν� �ʱ�ȭ
            Managers.UI.ShowSceneUI<UI_MainMenu>();
            SceneType = Enums.Scene.SceneMainMenu;
        }

    }

}