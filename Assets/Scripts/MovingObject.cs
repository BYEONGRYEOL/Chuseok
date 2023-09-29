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

                vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); //vecotr �������� ���ϰ� Set�� �������

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

         private Vector3 vector; //���͵� ��������������.

         public float runSpeed;
         private float applyRunSpeed;
         private bool applyRunFlag = false;

         // Ÿ�� ������ �̵��ϴ� �ڵ�
         public int walkCount;
         private int currentWalkCount;
         // speed = 2.4, walkCount = 20
         // 2.4 * 20 = 4.8 (walkCount ������ ���� �ѹ��� 4.8�� �̵��ϰԵ�)

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
                     if (vector.x != 0) //vector.x�� 0�� �ƴ϶��
                     {
                         transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); // vector.x�� speed��ŭ ���Ѵ�.
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
                 // HorizontalŰ�� 0�� �ƴ϶��(= -1, 1�� ���ȴٸ�)
                 {
                     canMove = false;
                     StartCoroutine(MoveCoroutine());
                 }
             }
         }*/
    }


}