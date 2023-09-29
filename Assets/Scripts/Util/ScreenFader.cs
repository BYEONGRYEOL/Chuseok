using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Isometric.Utility
{


    public class ScreenFader : MonoBehaviour
    {
        [SerializeField] protected float solidAlpha = 1f;
        [SerializeField] protected float clearAlpha = 0f;

        [SerializeField] private MaskableGraphic[] graphicsToFade;
        [SerializeField] private float fadeOnDuration = 1f;
        public float FadeOnDuration { get { return fadeOnDuration; } }
        [SerializeField] private float fadeOffDuration = 1f;
        public float FadeOffDuration { get { return fadeOffDuration; } }
        private void Start()
        {
            FadeOn();
        }
        protected void SetAlpha(float alpha)
        {
            foreach (MaskableGraphic graphic in graphicsToFade)
            {
                if (graphic != null)
                {
                    graphic.canvasRenderer.SetAlpha(alpha);
                }
            }
        }

        private void Fade(float targetAlpha, float duration)
        {
            foreach (MaskableGraphic graphic in graphicsToFade)
            {
                graphic.CrossFadeAlpha(targetAlpha, duration, true);
            }
        }

        public void FadeOff()
        {
            SetAlpha(solidAlpha);
            Fade(clearAlpha, fadeOffDuration);
        }

        public void FadeOn()
        {
            SetAlpha(clearAlpha);
            Fade(solidAlpha, fadeOnDuration);
        }
    }

}