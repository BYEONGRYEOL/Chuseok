using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Isometric.Utility
{

    public class LocalizedText : MonoBehaviour
    {
        // Start is called before the first frame update
        public string key;

        // Update is called once per frame
        void Start()
        {/*
            TextMeshProUGUI tmPro = GetComponent<TextMeshProUGUI>();
            *//*tmPro.font = *//*
            Debug.Log(key);
            tmPro.text = LocalizationManager.Instance.GetLocalizedValue(key);*/
            
        }
        public void setText(string inputkey)
        {/*
            key = inputkey;
            TextMeshProUGUI tmPro = GetComponent<TextMeshProUGUI>();
            *//*tmPro.font = *//*
            Debug.Log(key);
            tmPro.text = LocalizationManager.Instance.GetLocalizedValue(key);*/

        }

    }

}