using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
namespace Isometric.UI
{
    
    public class UI_Loading : UI_Scene
    {


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
        }
    }
}