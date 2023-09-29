using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class SceneMainMenu : SceneBase
    {
        IEnumerator LoadJson()
        {
            Debug.Log("Main Menu :: LoadJson run");
            yield return new WaitUntil(() => Managers.Data.IsJsonLoaded());
            Debug.Log("Main Menu :: MakeJsontoDict run");
            Managers.Data.MakeJsontoDict();
            Managers.Data.MakeJsontoList();
            StartCoroutine(LoadItem());
        }
        public override void Clear()
        {
            
        }

        IEnumerator LoadItem()
        {
            Debug.Log("Main Menu :: LoadItem run");
            yield return new WaitForSeconds(0f);
            Managers.InitWhenLoading();
        }
        protected override void Init()
        {
            base.Init();
            StartCoroutine(LoadJson());
            
            Managers.UI.ShowSceneUI<UI_MainMenu>();
            SceneType = Enums.Scene.SceneMainMenu;

            
            //���θ޴��� ������ ��, ���� �� �κ��丮���� DB�˻� �� �κ��丮 �ʱ�ȭ
            
        }

    }

}