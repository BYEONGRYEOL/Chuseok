using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;
using Isometric.UI;
using UnityEngine.EventSystems;
using Isometric.Utility;
namespace Isometric
{
    public class PlayerController : CharacterController
    {
        #region Inspector
        // 스피드 조정 변수
        private static PlayerController instance;
        public static PlayerController Instance { get { return instance; } }

        public PlayerStat stat;

        // 점프 정도
        [SerializeField]
        private float jumpForce;
        private Vector3 player_sight = new Vector3(0, 0, 0);
        private Vector3 velocity = new Vector3(0, 0, 0);

        // 앉았을 때 얼마나 앉을지 결정하는 변수
        [SerializeField]
        private float crouchPosY;
        private float applyCrouchPosY;
        
        // 필요한 컴포넌트
        [SerializeField]
        private Camera theCamera;
        private Rigidbody2D myRigid2D;

        [SerializeField] private Transform interActionBox;
        GameObject target;
        protected float interActionDuration;

        Coroutine interActionCo;
        float moveSpeed = 2f;
        #endregion

        #region Initialize
        protected override void Start()
        {
            base.Start();
        }
        protected override void Init()
        {
            base.Init();
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            myAnimator = GetComponent<Animator>();

            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;

            //콜백처리
            //player state 안에서 처리할수 없어서 OnMouse에서 직접 조건걸기
            Managers.Input.MouseAction -= OnMouse;
            Managers.Input.MouseAction += OnMouse;

            //Debug.Log(Managers.KeyBind.KeyBinds["INTERACTION"]);

            interActionDuration = 0.1f;

            myRigid2D = GetComponent<Rigidbody2D>();
            Time.timeScale = 1f;
            //Cursor.visible = false;
            target = GameObject.Find("target");
        }

        #endregion

        void Update()
        { 
            if (Input.GetKey(Managers.KeyBind.KeyBinds["UP"]))
                originalDirection.y = 1;
            else if (Input.GetKey(Managers.KeyBind.KeyBinds["DOWN"]))
                originalDirection.y = -1;
            else
            {
                originalDirection.y = 0;
            }
            if (Input.GetKey(Managers.KeyBind.KeyBinds["RIGHT"]))
                originalDirection.x = 1;
            else if (Input.GetKey(Managers.KeyBind.KeyBinds["LEFT"]))
                originalDirection.x = -1;
            else
            {
                originalDirection.x = 0;
            }
            

            InterActionBox_Location_Set();
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case Enums.CharacterState.Idle:
                    Update_Idle();
                    break;
                case Enums.CharacterState.Move:
                    Update_Move();
                    break;
                case Enums.CharacterState.Attack_1:
                    Update_Attack();
                    break;
                case Enums.CharacterState.Run:
                    Update_Run();
                    break;
                case Enums.CharacterState.Interaction:
                    Update_InterAction();
                    break;
                case Enums.CharacterState.Dodge:
                    Update_Dodge();
                    break;
                case Enums.CharacterState.Die:
                    Update_Die();
                    break;
                case Enums.CharacterState.Climbing:
                    Update_Climbing();
                    break;
                case Enums.CharacterState.TakeDamage:
                    Update_TakeDamage();
                    break;
            }
        }
        protected override void Enter_TakeDamage()
        {
            //공격받아 딜레이 중
            Managers.Time.SetTimer(0.5f, () => State = Enums.CharacterState.Idle );
        }
        
        #region Update_state
        protected override void Update_TakeDamage()
        {
            ActivateAnimationLayer(Enums.AnimationLayer.HitDamageLayer);
        }
        protected override void Update_Dodge()
        {

        }
        protected override void Update_Die()
        {

        }
        
        protected override void Update_Idle()
        {
            

            if(originalDirection.sqrMagnitude > 0)
            {
                State = Enums.CharacterState.Move;
            }
        }
        protected override void Update_Move()
        {
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            myMove(stat.MoveSpeed);

            if (direction.sqrMagnitude == 0)
            {
                State = Enums.CharacterState.Idle;
            }
        }
        protected override void Update_Attack()
        {
            
        }
        protected override void Update_Run()
        {
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
            myMove(moveSpeed*2);

            if (originalDirection.sqrMagnitude == 0)
            {
                State = Enums.CharacterState.Idle;
            }
        }
        
        private void Update_InterAction()
        {

        }
        
        private void Update_Climbing()
        {

        }
        #endregion

        
        #region Input
        private void OnKeyboard()
        {
            originalDirection.z = 0;
            if (Input.GetKeyUp(Managers.KeyBind.KeyBinds["RUN"]))
                RunningCancel();

            if (Input.GetKey(Managers.KeyBind.KeyBinds["RUN"]))
                Running();
            
            
            foreach (string action in Managers.KeyBind.ActionBinds.Keys)
            {
                if (Input.GetKeyDown(Managers.KeyBind.ActionBinds[action]))
                {
                    //UI_Ingame_R.Instance.ClickActionButton(action);
                    Debug.Log(action + "키입력받음");
                }
            }
        }
        private void OnMouse()
        {
            if(State == Enums.CharacterState.Die)
                return;
            //공격
            //마우스 클릭을 인식하는 경우 항상 UI elements 클릭시와 구별할 수 있도록 코드 추가
            if (Input.GetMouseButtonUp(0))
            {
                StopInterAction();
            }
            if (Input.GetMouseButtonDown(0))
            {
                InterAction();
            }
        }
        #endregion
        private void myMove(float speed)
        {

            direction = originalDirection.normalized;
            velocity = direction * speed;
            myRigid2D.MovePosition(transform.position + velocity * Time.deltaTime);
        }

        #region Attack
        private void InterAction_Activate()
        {
            interActionBox.gameObject.SetActive(true);
        }

        private IEnumerator InterActionAnimation()
        {
            State = Enums.CharacterState.Attack_1;

            myAnimator.SetBool("isBowing", true);

            InterAction_Activate();
            yield return new WaitForSeconds(interActionDuration);
            InterActionBox_Disable();
            StopInterAction();
        }

        
        private void InterActionBox_Location_Set()
        {
            // 자식 오브젝트의 위치 = localPosition으로 하자
            interActionBox.localPosition = new Vector3(originalDirection.x * 0.5f, originalDirection.y * 0.5f, 0);

            if (originalDirection.x + originalDirection.y == 2)
            {
                interActionBox.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x + originalDirection.y == -2)
            {
                interActionBox.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x - originalDirection.y == 2)
            {
                interActionBox.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.y - originalDirection.x == 2)
            {
                interActionBox.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.x - originalDirection.y == 1)
            {
                if (originalDirection.x > 0)
                {
                    //우
                    interActionBox.rotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    //하
                    interActionBox.rotation = Quaternion.Euler(0, 0, 90);

                }
            }
            else if (originalDirection.x - originalDirection.y == -1)
            {
                if (originalDirection.y > 0)
                {
                    //상
                    interActionBox.rotation = Quaternion.Euler(0, 0, 90);

                }
                else
                {
                    //좌
                    interActionBox.rotation = Quaternion.Euler(0, 0, 0);

                }
            }
        }
        public void InterActionBox_Disable()
        {
            interActionBox.gameObject.SetActive(false);
        }

        public void OnADHit(EnemyController hittedEnemy)
        {
            Debug.Log("플레이어가 적 공격 판정");
            hittedEnemy.GetComponent<EnemyController>().GetAttacked(stat);
        }
        #endregion

        #region GetAttacked
        public void GetAttacked(Stat attacker)
        {
            stat.GetAttacked(attacker);
            State = Enums.CharacterState.TakeDamage;
        }

        #endregion

        private void InterAction()
        {
            // 이동, 뛰기, 기본, 상태일때만 되는거야
            if (State != Enums.CharacterState.Idle && State != Enums.CharacterState.Move && State!= Enums.CharacterState.Run)
                return;

            State = Enums.CharacterState.Interaction;

            interActionCo = StartCoroutine(InterActionAnimation());
        }
        
        private void Running()
        {
            //idle, Move, 
            if (State != Enums.CharacterState.Idle && State != Enums.CharacterState.Move)
                return;

            State = Enums.CharacterState.Run;
        }

        // 달리기 취소
        private void RunningCancel()
        {
            if (State != Enums.CharacterState.Run)
                return;
            State = Enums.CharacterState.Move;
        }


        private void StopInterAction()
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                myAnimator.SetBool("isAttacking", false);
                State = Enums.CharacterState.Idle;
            }
        }


    } 
}