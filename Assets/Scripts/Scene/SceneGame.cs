using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class SceneGame : SceneBase
    {
        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowSceneUI<UI_Game>();
            SceneType = Enums.Scene.SceneGame;
        }
        public override void Clear()
        {

        }
        public void SetUI()
        {

            UI_Game gameUI = Managers.UI.SceneUI as UI_Game;
            if (gameUI == null)
            {
                //ĳ���� ���� -> ���� ui�� �ΰ���ui�� �ƴ��� �̾߱��ϴ°�
                return;
            }


        }
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}