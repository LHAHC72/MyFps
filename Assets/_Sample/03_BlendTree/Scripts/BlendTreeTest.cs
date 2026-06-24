using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{

    public class BlendTreeTest : MonoBehaviour
    {
        #region Variables
        // 참조
        private Animator animator;

        // 이동
        [SerializeField] private float moveSpeed = 5f;


        // 인풋 시스템
        public InputActionReference moveAction;

        public Vector2 InputMove { get; private set; }



        #endregion

        #region Properties

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

        }

        private void OnDisable()
        {
            // 인풋 액션 비활성화
            moveAction.action.Disable();

        }

        private void Update()
        {
            // 인풋 처리
            InputMove = moveAction.action.ReadValue<Vector2>();
            // 캐릭터 이동 : 방향 * Time.deltaTime * 속도
            Vector3 dir = new Vector3(InputMove.x, 0f, InputMove.y);
            transform.Translate(dir * Time.deltaTime * moveSpeed, Space.World);

            // 애니메이션 상태 처리
            // AnimationStateTest(InputMove);

            // 블랜더 트리로 애니메이션 상태 처리
            AnimationBlendTreeTest(InputMove);
        }

        #endregion

        #region Custom Methods

        void AnimationBlendTreeTest(Vector2 moveDir)
        {
            animator.SetFloat(moveX, moveDir.x);
            animator.SetFloat(moveY, moveDir.y);
        }
        

        // 애니메이션
        private string moveState = "MoveState";
        private string moveX = "MoveX";
        private string moveY = "MoveY";

        void AnimationStateTest(Vector2 moveDir)
        {
            if (moveDir == Vector2.zero)
            {
                animator.SetInteger(moveState, 0);
            }

            else
            {
                if (moveDir.y > 0f)
                {
                    animator.SetInteger(moveState, 1);
                }

                if (moveDir.y < 0f)
                {
                    animator.SetInteger(moveState, 2);
                }

                if (moveDir.x < 0f)
                {
                    animator.SetInteger(moveState, 3);
                }


                if (moveDir.x > 0f)
                {
                    animator.SetInteger(moveState, 4);
                }


            }

        }

        #endregion
    }
}
