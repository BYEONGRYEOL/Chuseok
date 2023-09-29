using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.UI
{
    public class UI_Popup : UI_Base
    {
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        public override void Init()
        {
            Managers.UI.SetCanvas(gameObject, true);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void ClosePopupUI()
        {
            Managers.UI.ClosePopupUI(this);
        }
    }

}