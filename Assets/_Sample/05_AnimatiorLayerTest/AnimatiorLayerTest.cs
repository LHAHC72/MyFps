using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{

    public class AnimatiorLayerTest : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        #region Variables
        // 참조
        private Animator animator;

        [SerializeField] private bool isMove;

        private string isMoving = "isMove";

        // 인풋 액션
        public InputActionReference moveAction;

        #endregion

        #region Profert
        public bool IsMove
        {
            get => isMove;
            private set
            {
                isMove = value;
                animator.SetBool(isMoving, value);
            }
        }

        #endregion


        #region Unity Event Method
        private void Awake()
        {
            // 참조
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 inputMove = moveAction.action.ReadValue<Vector2>();
            IsMove = inputMove != Vector2.zero; // 이동 입력이 있으면 IsMove를 true로 설정

            if (isMove)
            {
                animator.SetLayerWeight(1, 0f);  // SetLayeWeight = 애니메이터 Layer의 index 값을 넣어야 함. 위부터 0 ,1, 2 ... 
            }
            else
            {
                animator.SetLayerWeight(0, 0f);
            }
            
        }

        #endregion
    }
}
