using Isometric.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Isometric
{

    public class Fadein : MonoBehaviour
    {
        public Image imageToFade;  // 페이드 인할 이미지나 UI 요소
        public float fadeDuration = 1.0f;  // 페이드 인 지속 시간 (초)

        private void Start()
        {
            StartCoroutine("DoFadeIn");
        }

        private IEnumerator DoFadeIn()
        {
            // 초기 알파 값을 0으로 설정
            Color startColor = imageToFade.color;
            startColor.a = 0f;
            imageToFade.color = startColor;

            float startTime = Time.time;
            float endTime = startTime + fadeDuration;

            bool flag = true;
            while (Time.time < endTime)
            {

                // 시간에 따라 알파 값을 서서히 증가시켜 페이드 인 효과 구현
                float elapsed = Time.time - startTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);

                Color newColor = imageToFade.color;
                newColor.a = alpha;
                imageToFade.color = newColor;

                if(flag && alpha > 0.4f)
                {
                    Managers.Sound.Play("Opening"); // 로딩 효과음
                    flag = false;
                }
                yield return null;
            }


            // 페이드 인이 완료된 후에는 해당 오브젝트 비활성화 또는 다른 작업 수행 가능
            Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu);
            gameObject.SetActive(false);

        }
    }

}