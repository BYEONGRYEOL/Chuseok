using System;
using System.Collections.Generic;
using UnityEngine;


 
public class FadeInOut : MonoBehaviour
{

    public SpriteRenderer MySpriteRenderer { get; set; }

    private Color defaultColor;
    private Color fadedColor;


    public void FadeOut()
    {
        MySpriteRenderer.color = fadedColor;
    }

    public void FadeIn()
    {
        MySpriteRenderer.color = defaultColor;
    }
    private void Start()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();

        defaultColor = MySpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ontrigger enter");

        if (collision.CompareTag("Player"))
        {

            Debug.Log("player 충돌 인식");
            FadeOut();

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ontrigger exit");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("player 충돌 인식끝");

            FadeIn();

          
          
        }
    }
}
