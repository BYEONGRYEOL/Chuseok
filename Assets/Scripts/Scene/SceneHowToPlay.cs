using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class SceneHowToPlay : SceneBase
    {
        public override void Clear()
        {
            
        }

        protected override void Init()
        {
            //�Ŵ����� ó�� �ҷ��� ���������ν� �ʱ�ȭ
            Managers.UI.ShowSceneUI<UI_HowToPlay>();
            SceneType = Enums.Scene.SceneMainMenu;
        }

    }

}