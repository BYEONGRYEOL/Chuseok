using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.UI
{
    // ��� �� UI���� �θ� Ŭ����
    public class UI_Scene : UI_Base
    {
        private static UI_Scene instance;
        public static UI_Scene Instance { get => instance; set => instance = value; }
        
        public override void Init()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
            Managers.UI.SetCanvas(gameObject, false);
        }
        public void OnDestroy()
        {
            instance = null;
        }
    }

}