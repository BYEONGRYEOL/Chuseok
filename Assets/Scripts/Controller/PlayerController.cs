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

        [SerializeField] private Transform attackBox_Tran;
        GameObject target;
        protected float attackDelay;
        protected float attackDuration;
        

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

            stat = GetComponent<PlayerStat>();
            myAnimator = GetComponent<Animator>();

            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;

            //콜백처리
            //player state 안에서 처리할수 없어서 OnMouse에서 직접 조건걸기
            Managers.Input.MouseAction -= OnMouse;
            Managers.Input.MouseAction += OnMouse;

            //Debug.Log(Managers.KeyBind.KeyBinds["INTERACTION"]);

            attackDelay = 0.5f;
            attackDuration = 0.1f;

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
            

            AttackBox_Location_Set();
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
            myMove(stat.MoveSpeed*2);

            if (originalDirection.sqrMagnitude == 0)
            {
                State = Enums.CharacterState.Idle;
            }
        }
        private void Update_Crouch()
        {

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
            //점프
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["JUMP"]))
                Jump();
            
            //상호작용
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["INTERACTION"]))
                InterAction();

            //웅크리기
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["CROUCH"]))
                Crouch();

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
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
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
        private void Attack_Activate()
        {
            attackBox_Tran.gameObject.SetActive(true);
        }

        private IEnumerator AttackAnimation()
        {
            State = Enums.CharacterState.Attack_1;

            myAnimator.SetBool("isAttacking", true);

            Attack_Activate();
            yield return new WaitForSeconds(attackDuration);

            AttackBox_Disable();
            yield return new WaitForSeconds(attackDelay);
            StopAttack();
        }

        private void Attack()
        {
            //공격 불가상태가 언제지?
            //crouch, 

            //달리면서 공격하면 다르게

            // 점프 공격은 다르게 

            // 첫 공격
            if(State != Enums.CharacterState.Attack_1 && State != Enums.CharacterState.Attack_2 && State != Enums.CharacterState.Attack_3)
            {
                Debug.Log("1차공격");
                State = Enums.CharacterState.Attack_1;
                attackRoutine = StartCoroutine(AttackAnimation());
                
            }
            
        }
        private void AttackBox_Location_Set()
        {
            // 자식 오브젝트의 위치 = localPosition으로 하자
            attackBox_Tran.localPosition = new Vector3(originalDirection.x * 0.5f, originalDirection.y * 0.5f, 0);

            if (originalDirection.x + originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x + originalDirection.y == -2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x - originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.y - originalDirection.x == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.x - originalDirection.y == 1)
            {
                if (originalDirection.x > 0)
                {
                    //우
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    //하
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 90);

                }
            }
            else if (originalDirection.x - originalDirection.y == -1)
            {
                if (originalDirection.y > 0)
                {
                    //상
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 90);

                }
                else
                {
                    //좌
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 0);

                }
            }
        }
        public void AttackBox_Disable()
        {
            attackBox_Tran.gameObject.SetActive(false);
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

            Debug.DrawRay(transform.position, player_sight * 10, Color.white);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player_sight, 3f, LayerMask.GetMask("object"));
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.CompareTag("grass"));
            }
            Debug.Log("interaction");
            Debug.Log(player_sight.x + player_sight.y);
        }
        // 점프
        private void Jump()
        {
            myRigid2D.velocity = transform.up * jumpForce;
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

        private void Dodge()
        {
            //맞은 상태, 공중, 에서는 안되어야해
            if (State == Enums.CharacterState.TakeDamage ||State == Enums.CharacterState.Die)
                return;
            State = Enums.CharacterState.Dodge;
        }

        // 웅크리기 동작
        private void Crouch()
        {
            if (State != Enums.CharacterState.Idle)
                return;
            //시간 보내기
        }

        private void StopAttack()
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                
                
                myAnimator.SetBool("isAttacking", false);
                State = Enums.CharacterState.Idle;
            }
        }

        
        public void CastSpell(int spellIndex)
        {
            
        }

    } 
}