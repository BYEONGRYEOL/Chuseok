using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Isometric.Utility
{
    [Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;

    }
    
    [Serializable]

    public class LocalizationItem 
    {
        public string key;
        public string value;
        public LocalizationItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }


}
