using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class SceneLoading : SceneBase
    {
        private void Awake()
        {
            Init();
        }
        protected override  void Init()
        {
            //매니저를 처음 불러와 실행함으로써 초기화
            Managers.Init();
            Managers.UI.ShowSceneUI<UI_Loading>();
            SceneType = Enums.Scene.SceneLoading;
        }

        public override void Clear()
        {
            
        }
    }

}