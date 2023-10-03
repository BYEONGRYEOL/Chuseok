using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;
using Isometric.UI;
using UnityEngine.EventSystems;
using Isometric.Utility;
using UnityEditor;

namespace Isometric
{
    public class PlayerController : SingletonDontDestroyMonobehavior<PlayerController>
    {
        //플레이어 조작에 관한 코드 전체

        #region Inspector

        // mobile 조작의 경우 터치패드로부터 이동 방향(8방향)을 계산하기 위한 벡터
        public Vector3 mobileDirection;
        // 원을 8등분하여, 모바일 터치패드에 조이스틱에 위치에 따라 8방향 중 어디로 이동할지 계산하는데 사용
        float sin22 = Mathf.Sin(22.5f * Mathf.Deg2Rad);
        float sinminus22 = Mathf.Sin(-22.5f * Mathf.Deg2Rad);

        // 상태 bool
        public bool running;
        public bool bowing;

        // 플레이어의 이동 방향 계산 벡터
        public Vector3 originalDirection;
        // 실제 이동에 사용하기위해 정규화된 방향벡터
        public Vector3 normalizedDirection;

        protected Animator myAnimator;
        protected Coroutine attackRoutine;
        protected Coroutine takeDamageRoutine;


        private Vector3 velocity = new Vector3(0, 0, 0);

        // 필요한 컴포넌트
        [SerializeField]
        private Camera theCamera;
        private Rigidbody2D myRigid2D;
        
        // 절 할때 할머니가 Interactionbox 내에 있어야 인정
        [SerializeField] private InterActionBox_Player interActionBox;
        protected float interActionDuration;

        // 기본 이동속도를 2로 설정
        float moveSpeed = 2f;

        protected Enums.CharacterState state = Enums.CharacterState.Idle;

        // 플레이어 상태 머신 
        public Enums.CharacterState State
        {
            get => state;
            set
            {
                state = value;
                Debug.Log(state);
                switch (state)
                {
                    //아무 조작도 없는 상태
                    case Enums.CharacterState.Idle:
                        ActivateAnimationLayer(Enums.AnimationLayer.IdleLayer);
                        myAnimator.SetBool("isIdle", true);
                        break;
                    // 이동
                    case Enums.CharacterState.Move:
                        ActivateAnimationLayer(Enums.AnimationLayer.WalkLayer);
                        break;
                    // 달리기 상태
                    case Enums.CharacterState.Run:
                        ActivateAnimationLayer(Enums.AnimationLayer.RunLayer);
                        break;

                    // 상호작용 (절) 상태
                    case Enums.CharacterState.Interaction:
                        ActivateAnimationLayer(Enums.AnimationLayer.BowLayer);
                        myAnimator.SetBool("isIdle", false);
                        myAnimator.SetBool("isBowing", true);
                        break;


                    #region legacy
                    // 아래는 현재 코육대 프로젝트에는 사용되진 않지만 오류 수정 등이 오래걸려 제출 기한때문에 어쩔수 없이

                    case Enums.CharacterState.Attack_1:
                        ActivateAnimationLayer(Enums.AnimationLayer.AttackLayer);
                        myAnimator.SetBool("isAttacking", true);
                        myAnimator.SetInteger("attackCount", 1);
                        break;
                    case Enums.CharacterState.TakeDamage:
                        ActivateAnimationLayer(Enums.AnimationLayer.HitDamageLayer);
                        Enter_TakeDamage();
                        break;
                    //우선은 플레이어 전용
                    case Enums.CharacterState.Attack_2:
                        myAnimator.SetInteger("attackCount", 2);
                        break;

                    case Enums.CharacterState.Attack_3:
                        myAnimator.SetInteger("attackCount", 3);
                        break;

                    case Enums.CharacterState.Attack_Jump:
                        break;

                    case Enums.CharacterState.Attack_Run:
                        break;

                    case Enums.CharacterState.Runaway:
                        break;
                        #endregion
                }
            }
        }
        #endregion

        #region Initialize

        public void Reset()
        {
            // 플레이어를 싱글톤으로 사용하고있기때문에 씬을 옮겨다니며 게임이 재시작 된 경우 재 초기화
            this.transform.position = new Vector3(0, 2, 0);
            myAnimator = GetComponent<Animator>();
            State = Enums.CharacterState.Idle;
        }
        protected void Start()
        {
            Init();
        }
        protected void Init()
        {
            
            Reset();

            // 키보드 입력 액션 bind
            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;

            interActionDuration = 0.5f;
            interActionBox.gameObject.SetActive(true);
            myRigid2D = GetComponent<Rigidbody2D>();
            Time.timeScale = 1f;
        }

        #endregion

        void Update()
        {
            // 좀 아쉬움이 남는 코드, 버그는 없으나 너무 보기 싫게 생겼다.
            // 8방향 이동을 구현함 모바일의 경우 모바일 터치패드로 부터 구한 방향으로 8방향 설정
            if (Managers.Data.gameData.IsMobile)
            {
                if (mobileDirection.x == 0 && mobileDirection.y == 0)
                {
                    originalDirection.x = 0;
                    originalDirection.y = 0;
                }
                //Debug.Log(mobileDirection);
                else if(sin22 > mobileDirection.x && sinminus22 < mobileDirection.x)
                {
                    // x가 sin -22.5 와 22.5 사이에 있다면 상 or 하
                    if(mobileDirection.y > 0) //상
                    {
                        interActionBox.bowDirection = 0;
                        originalDirection.x = 0;
                        originalDirection.y = 1;
                    }
                    else // 하
                    {
                        interActionBox.bowDirection = 4;
                        originalDirection.x = 0;
                        originalDirection.y = -1;
                    }
                }
                else if(sin22 > mobileDirection.y && sinminus22 < mobileDirection.y)
                {
                    // y가 sin-22.5와 22.5 사이에 있다면 좌or 우
                    if(mobileDirection.x > 0) // 우
                    {
                        interActionBox.bowDirection = 2;
                        originalDirection.x = 1;
                        originalDirection.y = 0;

                    }
                    else // 좌
                    {
                        interActionBox.bowDirection = 6;
                        originalDirection.x = -1;
                        originalDirection.y = 0;
                    }
                }
                else
                {
                    if (mobileDirection.x > 0 && mobileDirection.y > 0)
                    {
                        originalDirection.x = 1;
                        originalDirection.y = 1;
                        interActionBox.bowDirection = 1;
                    }
                    else if ( mobileDirection.x > 0 && mobileDirection.y < 0)
                    {
                        originalDirection.x = 1;
                        originalDirection.y = -1;
                        interActionBox.bowDirection = 3;
                    }
                    else if (mobileDirection.x < 0 && mobileDirection.y > 0)
                    {
                        originalDirection.x = -1;
                        originalDirection.y = 1;
                        interActionBox.bowDirection = 7;
                    }
                    else
                    {
                        originalDirection.x = -1;
                        originalDirection.y = -1;
                        interActionBox.bowDirection = 5;

                    }
                }
            }
            else
            {
                // 모바일이 아닌 경우 8방향 설정하는 로직
                if (Input.GetKey(KeyCode.W)) // 상
                    originalDirection.y = 1;
                else if (Input.GetKey(KeyCode.S)) // 하
                    originalDirection.y = -1;
                else
                    originalDirection.y = 0; // 좌 우 인경우 y방향은 0


                if (Input.GetKey(KeyCode.D)) // 우 
                    originalDirection.x = 1;
                else if (Input.GetKey(KeyCode.A)) // 좌 
                    originalDirection.x = -1;
                else
                    originalDirection.x = 0; // 상 하 이동인경우 x방향은 0

            }
            // z축이 안맞아서 충돌하지않는 경우가 있다.
             originalDirection.z = 0;
        }

        private void FixedUpdate()
        {
            // 상태머신, 현재 상태일때는 매 프레임당 어떤 것을 검사하는지, 어떤 조건에 의해 다른 상태로 변하는지 구현
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
                case Enums.CharacterState.TakeDamage:
                    Update_TakeDamage();
                    break;
            }
        }

        #region legacy
        protected void Enter_TakeDamage()
        {
            //공격받아 딜레이 중
            Managers.Time.SetTimer(0.5f, () => State = Enums.CharacterState.Idle );
        }
        #endregion 


        #region Update_state
        protected void Update_TakeDamage()
        {
            ActivateAnimationLayer(Enums.AnimationLayer.HitDamageLayer);
        }
        
        
        protected  void Update_Idle()
        {

            if(originalDirection.sqrMagnitude > 0)
            {
               // Debug.Log("idle 상태에서 이동 감지");
                State = Enums.CharacterState.Move;
            }
        }
        protected  void Update_Move()
        {
            myAnimator.SetFloat("x", normalizedDirection.x);
            myAnimator.SetFloat("y", normalizedDirection.y);

            myMove(moveSpeed);

            if (normalizedDirection.sqrMagnitude == 0)
            {
                State = Enums.CharacterState.Idle;
            }
        }
        protected  void Update_Attack()
        {
            
        }
        protected  void Update_Run()
        {
            myAnimator.SetFloat("x", normalizedDirection.x);
            myAnimator.SetFloat("y", normalizedDirection.y);
            myMove(moveSpeed*2);

            if (originalDirection.sqrMagnitude == 0)
            {
               // Debug.Log("멈춰서 idle로 돌아가기 ");
                State = Enums.CharacterState.Idle;
            }
        }
        
        private void Update_InterAction()
        {
            myAnimator.SetFloat("x", normalizedDirection.x);
            myAnimator.SetFloat("y", normalizedDirection.y);
        }
       
        #endregion

        
        #region Input
        private void OnKeyboard()
        {
            // GetKeyUp을 상단에 위치시켜야 눌렀다가 뗄 때 버그가 없다
            if (Input.GetKeyUp(KeyCode.K))
                StopInterAction();
            if (Input.GetKeyDown(KeyCode.K))
                InterAction();
            if (Input.GetKeyUp(KeyCode.LeftShift))
                RunningCancel();
            if (Input.GetKey(KeyCode.LeftShift))
                Running();
        }
        
        #endregion
        private void myMove(float speed)
        {
            // rigidbody moveposition으로 속도와 방향 벡터로 캐릭터 이동
            normalizedDirection = originalDirection.normalized;
            velocity = normalizedDirection * speed;
            myRigid2D.MovePosition(transform.position + velocity * Time.deltaTime);
        }

        
        private void InterActionBox_Location_Set()
        {
            // 현재 벡터와 바라보는 방향에 따라서 할머니와 상호작용하는 collider 위치 변경
            // 자식 오브젝트의 위치 = localPosition으로 하자
            interActionBox.transform.localPosition = new Vector3(originalDirection.x * 0.5f, originalDirection.y * 0.5f, 0);

            if (originalDirection.x + originalDirection.y == 2) // 우상
            {
                interActionBox.bowDirection = 1;
                interActionBox.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x + originalDirection.y == -2) // 좌하
            {
                interActionBox.bowDirection = 5;
                interActionBox.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x - originalDirection.y == 2) // 우하
            {
                interActionBox.bowDirection = 3;
                interActionBox.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.y - originalDirection.x == 2) // 좌상
            {
                interActionBox.bowDirection = 7;
                interActionBox.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.x - originalDirection.y == 1)
            {
                if (originalDirection.x > 0) // 우
                {
                    interActionBox.bowDirection = 2;
                    interActionBox.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else // 하
                {
                    interActionBox.bowDirection = 4;
                    interActionBox.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
                
            else if (originalDirection.x - originalDirection.y == -1)
            {
                if (originalDirection.y > 0) // 상
                {
                    interActionBox.bowDirection = 0;
                    interActionBox.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else // 좌
                {
                    interActionBox.bowDirection = 6;
                    interActionBox.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
                
        }

        // 그냥 걸어다니다가 할머니랑 절 상호작용하면 안되니까 Disable, Activate 기능 구현
        public void InterActionBox_Disable()
        {
            interActionBox.SetDisable();
        }
        private void InterAction_Activate()
        {
            interActionBox.SetAble();
        }


        #region GetAttacked


        #endregion

        public void InterAction()
        {
            // 현재 상태를 상호작용 중으로 변경하고,
            State = Enums.CharacterState.Interaction;
            //Debug.Log("절");
            InterActionBox_Location_Set(); // 할머니 검사 레이어 위치 조정
            InterAction_Activate(); // 상호작용 박스 활성화
        }
        
        public void Running()
        {
            
            if (State != Enums.CharacterState.Idle && State != Enums.CharacterState.Move)
                return;

            State = Enums.CharacterState.Run;
        }

        // 달리기 취소
        public void RunningCancel()
        {
            if (State != Enums.CharacterState.Run)
                return;
            // 달리기 취소 시 Move로 변경
            State = Enums.CharacterState.Move;
        }


        public void StopInterAction()
        {
            //Debug.Log("절 취소");
            myAnimator.SetBool("isBowing", false);
            State = Enums.CharacterState.Idle;
            InterActionBox_Disable();
        }
        public void ActivateAnimationLayer(Enums.AnimationLayer layerName)
        {
            // 모든 레이어의 무게값을 0 으로 만들어 줍니다.
            for (int i = 0; i < myAnimator.layerCount; i++)
            {
                myAnimator.SetLayerWeight(i, 0);
            }
            myAnimator.SetLayerWeight((int)layerName, 1);
        }

    } 
}