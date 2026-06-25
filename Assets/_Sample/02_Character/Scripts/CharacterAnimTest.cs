using UnityEngine;
using UnityEngine.InputSystem;


namespace MySample
{

    public class CharacterAnimTest : MonoBehaviour
    {
        // 캐릭터 애니메이션을 제어하는 예제 클래스
        // 뉴 인풋 시스템
        // 기본이 대기상태
        // W키가 들어오면 걷기 애니메이션 재생
        // + Shift키가 들어오면 달리기 애니메이션 재생


        #region Variables
        // 참조
        private Animator animator;

        [SerializeField] private bool isMove;
        [SerializeField] private bool isRun;

        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 4f;
        [SerializeField] private float moveSpeed = 0;
        [SerializeField] private float accelerationSpeed = 0.1f;

        [SerializeField] private string velocity = "Velocity";

        // 애니 파라미터 스트링
        private string isMoving = "IsMove";
        private string isRuning = "IsRun";

        // 인풋 시스템
        public InputActionReference moveAction;
        public InputActionReference sprintAction;
        

        #endregion

        #region Properties

        public bool IsMove
        {
            get => isMove;
            private set
            {
                isMove = value;
                animator.SetBool(isMoving, value);
            }
        }

        public bool IsRun
        {
            get => isRun;
            private set
            {
                isRun = value;
                animator.SetBool(isRuning, value);
            }
        }

        public float MoveSpeed
        {
            get {  return moveSpeed; }
            private set
            {
                moveSpeed = value;
                animator.SetFloat(velocity, value);
            }

        }

        #endregion

        #region Unity Event Methods
        private void Awake()
        {
            // 참조 초기화
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            // 인풋 액션 활성화
            moveAction.action.Enable();
            sprintAction.action.Enable();
        }

        private void OnDisable()
        {
            // 인풋 액션 비활성화
            moveAction.action.Disable();
            sprintAction.action.Disable();
        }

        private void Update()
        {
            // 인풋 처리
            Vector2 inputMove = moveAction.action.ReadValue<Vector2>();
            IsMove = inputMove != Vector2.zero; // 이동 입력이 있으면 IsMove를 true로 설정

            if (sprintAction.action.WasPressedThisFrame())
            {
                IsRun = true; // Shift키가 눌리면 달리기 상태로 전환
            }
            else if (sprintAction.action.WasReleasedThisFrame())
            {
                IsRun = false; // Shift키가 떼어지면 달리기 상태 해제
            }

            // 걷기
            if (IsMove && !IsRun)
            {
                if (moveSpeed > walkSpeed)
                {
                    moveSpeed -= accelerationSpeed;
                    if (moveSpeed >= walkSpeed)
                    {
                        moveSpeed = walkSpeed;
                    }
                }
                // 뛰기
                else if (IsMove && IsRun)
                {
                    if (moveSpeed >= runSpeed)
                    {
                        moveSpeed = runSpeed;
                    }
                }
            }

        }
    }
        #endregion

    #region Custom Methods

    #endregion

}
