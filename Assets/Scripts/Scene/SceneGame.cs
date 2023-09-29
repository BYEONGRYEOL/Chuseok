using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class SceneGame : SceneBase
    {
        UI_Inventory inventory;
        enum PopUpKeys
        {
            
        }
        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowSceneUI<UI_Game>();
            inventory = GetComponent<UI_Inventory>();
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
                //캐스팅 실패 -> 현재 ui가 인게임ui가 아님을 이야기하는것
                return;
            }
            UI_Inventory inventory = gameUI.inventory;


            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isActiveAndEnabled)
                {
                    inventory.gameObject.SetActive(false);
                }
                else
                {
                    inventory.gameObject.SetActive(true);
                    inventory.RefreshSlot();
                }
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