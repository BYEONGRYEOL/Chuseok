using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class MovingObject : MonoBehaviour
    {
        public float speed;

        private Vector3 vector;

        private Animator animator;

        public float runSpeed;
        private float applyRunSpeed;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        void Update()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    applyRunSpeed = runSpeed;
                }
                else
                {
                    applyRunSpeed = 0f;
                }

                vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); //vecotr 변수선언 안하고 Set문 못끌어옴

                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
            }
        }
        /* public float speed;

         private Vector3 vector; //벡터도 변수선언해주자.

         public float runSpeed;
         private float applyRunSpeed;
         private bool applyRunFlag = false;

         // 타일 단위로 이동하는 코드
         public int walkCount;
         private int currentWalkCount;
         // speed = 2.4, walkCount = 20
         // 2.4 * 20 = 4.8 (walkCount 변수로 인해 한번에 4.8씩 이동하게됨)

         private bool canMove;

         void Start()
         {
         }
             IEnumerator MoveCoroutine()
             {
                 if (Input.GetKey(KeyCode.LeftShift))
                 {
                     applyRunSpeed = runSpeed;
                     applyRunFlag = true;
                 }
                 else
                     applyRunSpeed = 0;
                     applyRunFlag = false;
                 vector.Set(Input.GetAxisRaw("Horizonta"), Input.GetAxisRaw("Vertical"), transform.position.z);

                 while (currentWalkCount < walkCount)
                 {
                     if (vector.x != 0) //vector.x가 0이 아니라면
                     {
                         transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); // vector.x에 speed만큼 더한다.
                     }
                     else if (vector.y != 0)
                     {
                         transform.Translate(vector.y * (speed + applyRunSpeed), 0, 0);
                         // = transform.position = vector;
                     }

                     currentWalkCount++;
                     yield return new WaitForSeconds(0.01f);
             }
             if (applyRunFlag)
                 currentWalkCount++;
             currentWalkCount = 0;
             canMove = true;
             }


         void Update()
         {
             if(canMove)
             {
                 if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                 // Horizontal키가 0이 아니라면(= -1, 1이 눌렸다면)
                 {
                     canMove = false;
                     StartCoroutine(MoveCoroutine());
                 }
             }
         }*/
    }


}