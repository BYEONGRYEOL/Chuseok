using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Utility
{
    public class TransitionFader : ScreenFader
    {
        [SerializeField] private float lifetime = 1f;
        [SerializeField] private float delay = 0.3f;

        protected void Awake()
        {
            lifetime = Mathf.Clamp(lifetime, FadeOnDuration + FadeOffDuration + delay, 10f);
            DontDestroyOnLoad(this);
        }

        private IEnumerator PlayCoroutine()
        {
            SetAlpha(clearAlpha);
            yield return new WaitForSeconds(delay);

            FadeOn();
            float onTime = lifetime - (FadeOffDuration + delay);

            yield return new WaitForSeconds(onTime);

            FadeOff();
            Destroy(gameObject, FadeOffDuration);
        }

        public void Transition()
        {
            StartCoroutine(PlayCoroutine());
        }

        public static void PlayTransition(TransitionFader transitionPrefab)
        {
            if (transitionPrefab != null)
            {
                TransitionFader instance = Object.Instantiate(transitionPrefab, Vector3.zero, Quaternion.identity);
                instance.Transition();
            }
        }
    }

}