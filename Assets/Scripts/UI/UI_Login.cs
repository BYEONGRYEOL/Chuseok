using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.UI
{

    public class UI_Login : UI_Scene
    {
        
        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            // 넥슨 게임 로그인처럼 창이 뜨고나서, fade out 해야해
        }
    }

}