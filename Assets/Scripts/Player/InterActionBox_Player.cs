using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class InterActionBox_Player : MonoBehaviour
    {
        public bool IsAble = false;
        
        public void SetAble()
        {
            IsAble = true;
            bowing = false;
        }
        public void SetDisable()
        {
            IsAble = false;
            bowing = false;
            bowRoutine = null;
        }
        
        float time = 0;
        float startTime = 0;
        public bool bowing = false;
        
        public int bowDirection = -1;

        Coroutine bowRoutine;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("House"))
            {
                Managers.Scene.LoadScene(Enums.Scene.SceneEnd);
            }
        }
        // Start is called before the first frame update
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (IsAble == false)
                return;
            if (collision.gameObject.layer == LayerMask.NameToLayer("Grandma"))
            {
                if(bowRoutine == null)
                {
                    Grandma grandma = collision.GetComponent<Grandma>();
                    bowRoutine = StartCoroutine(Bowing(0.8f, grandma));
                }
            }
        }

        IEnumerator Bowing(float seconds, Grandma grandma)
        {
            Debug.Log("�� �ڷ�ƾ ����");
            yield return new WaitForSeconds(seconds);
            // �� �ð����� coroutine�� null�� ���� �ʾҴٸ� ����
            // 0.8�ʰ� �� ����� �����ϰ��־�� �Ѵٴ� ���̴�.
            Debug.Log("�� ����");
            if(IsAble)
                grandma.TryBow(bowDirection);
        }
    } 
}
