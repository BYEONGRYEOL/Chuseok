using System;
using System.Collections.Generic;
using UnityEngine;


 
public class FadeInOut : MonoBehaviour
{
    // 플레이어가 나무 뒤로 들어가는 경우 나무가 투명해져서(alpha값 조절) 플레이어가 보일 수 있도록 하는 클래스
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
        //Debug.Log("ontrigger enter");
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("player 충돌 인식");
            FadeOut();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FadeIn();
        }
    }
}
