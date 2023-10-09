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
        public string TypingString = "@3초만에 집에 돌아왔다! \n엄마는 내가 캐쉬를 환불한줄 안다.\n @1원 남았으니까 이걸로 @2 사야지~~";
        
        void Start()
        {
            Managers.Sound.Play("TypeWriting");
            StartCoroutine(_typing());
        }

        // Update is called once per frame
        IEnumerator _typing()
        {
            // 시작하고 0.2초 후 부터 0.05초에 한자씩 타이핑 되는 것 처럼 보이기
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