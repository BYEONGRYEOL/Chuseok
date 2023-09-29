using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class Obstacle : MonoBehaviour, IComparable<Obstacle>
    {
        public SpriteRenderer individualSpriteRenderer { get; set; }
        private Color defaultColor;
        private Color fadedColor;
        public int CompareTo(Obstacle other)
        {
            if(individualSpriteRenderer.sortingOrder > other.individualSpriteRenderer.sortingOrder)
            {
                return 1;
            }
            else if(individualSpriteRenderer.sortingOrder < other.individualSpriteRenderer.sortingOrder)
            {
                return -1;
            }
            return 0;
        }
        void Start()
        {
            individualSpriteRenderer = GetComponent<SpriteRenderer>();

            defaultColor = individualSpriteRenderer.color;
            fadedColor = defaultColor;
            fadedColor.a = 0.7f;
        }

        public void FadeOut()
        {
            individualSpriteRenderer.color = fadedColor;
        }
        public void FadeIn()
        {
            individualSpriteRenderer.color = defaultColor;
        }
    }

}