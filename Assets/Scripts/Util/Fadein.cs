using Isometric.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Isometric
{

    public class Fadein : MonoBehaviour
    {
        public Image imageToFade;  // ���̵� ���� �̹����� UI ���
        public float fadeDuration = 1.0f;  // ���̵� �� ���� �ð� (��)

        private void Start()
        {
            StartCoroutine("DoFadeIn");
        }

        private IEnumerator DoFadeIn()
        {
            // �ʱ� ���� ���� 0���� ����
            Color startColor = imageToFade.color;
            startColor.a = 0f;
            imageToFade.color = startColor;

            float startTime = Time.time;
            float endTime = startTime + fadeDuration;

            bool flag = true;
            while (Time.time < endTime)
            {

                // �ð��� ���� ���� ���� ������ �������� ���̵� �� ȿ�� ����
                float elapsed = Time.time - startTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);

                Color newColor = imageToFade.color;
                newColor.a = alpha;
                imageToFade.color = newColor;

                if(flag && alpha > 0.4f)
                {
                    Managers.Sound.Play("Opening"); // �ε� ȿ����
                    flag = false;
                }
                yield return null;
            }


            // ���̵� ���� �Ϸ�� �Ŀ��� �ش� ������Ʈ ��Ȱ��ȭ �Ǵ� �ٸ� �۾� ���� ����
            Managers.Scene.LoadScene(Enums.Scene.SceneMainMenu);
            gameObject.SetActive(false);

        }
    }

}