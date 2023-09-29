using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class ColorTrans : MonoBehaviour
    {
        // Start is called before the first frame update
        SpriteRenderer sr;
        public GameObject go;

        void Start()
        {
            sr = go.GetComponent<SpriteRenderer>();
            sr.material.color = Color.clear;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}