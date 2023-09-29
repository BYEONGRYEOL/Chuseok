using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Isometric.Utility;
using System;
namespace Isometric.UI
{

    public class UI_Button : UI_Popup
    {
        
        enum Buttons
        {
            PointButton
        }
        enum Texts
        {
            hitext,
            byetext
        }
        void Start()
        {
            Bind<Button>(typeof(Buttons));
            
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}