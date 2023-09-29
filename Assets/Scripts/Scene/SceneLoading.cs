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
            //base.Init();
            //�Ŵ����� ó�� �ҷ��� ���������ν� �ʱ�ȭ
            Managers.UI.ShowSceneUI<UI_Loading>();
            SceneType = Enums.Scene.SceneLoading;
        }

        public override void Clear()
        {
            
        }
    }

}