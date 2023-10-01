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
            //매니저를 처음 불러와 실행함으로써 초기화
            Managers.UI.ShowSceneUI<UI_MainMenu>();
            SceneType = Enums.Scene.SceneMainMenu;
        }

    }

}