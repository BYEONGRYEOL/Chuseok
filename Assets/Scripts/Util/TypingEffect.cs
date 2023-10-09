using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Isometric
{

    public class TypingEffect : MonoBehaviour
    {
        public TextMeshProUGUI tx;
        public string TypingString = "@3�ʸ��� ���� ���ƿԴ�! \n������ ���� ĳ���� ȯ������ �ȴ�.\n @1�� �������ϱ� �̰ɷ� @2 �����~~";
        
        void Start()
        {
            Managers.Sound.Play("TypeWriting");
            StartCoroutine(_typing());
        }

        // Update is called once per frame
        IEnumerator _typing()
        {
            // �����ϰ� 0.2�� �� ���� 0.05�ʿ� ���ھ� Ÿ���� �Ǵ� �� ó�� ���̱�
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i <= TypingString.Length; i++)
            {
                tx.text = TypingString.Substring(0, i);

                yield return new WaitForSeconds(0.05f);
            }
            Managers.Sound.StopSFX();
        }
    }

}