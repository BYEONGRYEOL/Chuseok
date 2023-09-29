using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
using Isometric.Data;
namespace Isometric
{

    public class SceneLogin : SceneBase
    {
        public override void Clear()
        {
            Managers.Data.Load();
        }

        // Start is called before the first frame update
        protected override void Init()
        {
            base.Init();
            SceneType = Enums.Scene.SceneLogin;
            Managers.UI.ShowSceneUI<UI_Login>();

        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}