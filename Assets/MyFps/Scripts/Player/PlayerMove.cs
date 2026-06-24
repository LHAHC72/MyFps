using System;
using UnityEngine;

namespace MyFps
{

    // 플레이어의 이동을 관리하는 클래스

    public class PlayerMove : MonoBehaviour
    {

        #region Variables
        // 참조
        private CharacterController controller;
        private CharacterInput input;

        [Header("player")] // 해더 특성 : 직렬화된 속성중에 플레이어 관련 속성들을 그룹화하여 표시
        
        // 이동
        [SerializeField] private float walkSpeed = 4f; // 걷기 속도
        [SerializeField] private float sprintSpeed = 7f; // 뛰기 속도
        private float moveSpeed; // 현재 이동 속도

        [Header("Jump & Gravity")] // 점프와 중력 관련 속성 그룹화

        // 점프 / 중력
        [SerializeField] private float jumpHeight = 1.2f;   // 점프 높이(미터)
        [SerializeField] private float gravity = -15f;      // 중력 가속도

        // 지면 체크
        [SerializeField] private float groundedOffset = -0.14f; // 지면 체크 구의 위치 오프셋(발 밑)
        [SerializeField] private float groundedRadius = 0.28f;  // 지면 체크 구의 반지름
        [SerializeField] private LayerMask groundLayers;        // 지면으로 인정할 레이어

        private float verticalVelocity;  // 수직 속도(중력/점프 누적값)
        private bool isGrounded;         // 현재 지면에 닿아있는지 여부
        private const float terminalVelocity = -53f; // 최대 낙하 속도 제한


        #endregion

        #region Unity Event Methods

        private void Awake()
        {
            // 참조 초기화
            controller = GetComponent<CharacterController>();
            input = GetComponent<CharacterInput>();
        }
        #endregion

        private void Update()
        {
            // 지면 체크
            GroundCheck();
            // 점프 / 중력 처리
            JumpAndGravity();
            // 이동 처리
            Move();
        }

        #region Custiom methods
        void Move()
        {
            moveSpeed = walkSpeed; // 현재 이동 속도를 걷기 속도로 초기화

            // 이동 인풋 체크
            if(input.Move == Vector2.zero) // 입력값이 없으면 이동하지 않음
            {
                moveSpeed = 0f;
            }

            // 인풋에서 방향값 얻어오기
            Vector3 inputDirection = new Vector3(input.Move.x, 0f, input.Move.y).normalized; // 입력값을 정규화하여 방향 벡터 생성

            // 플레이어의 로컬 방향 구하기
            if(input.Move != Vector2.zero) // 입력값이 있으면
            {
                inputDirection = transform.right * input.Move.x + transform.forward * input.Move.y; // 로컬 방향으로 변환
            }

            // 이동 : 수평 이동(방향 * 속도) + 수직 이동(중력/점프) 을 한번에 처리
            controller.Move(inputDirection.normalized * moveSpeed * Time.deltaTime
                + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);

        }

        // 발 밑에 구를 만들어 지면과 겹치는지 검사
        void GroundCheck()
        {
            // 플레이어 위치 기준 발 밑 지점 계산
            Vector3 spherePosition = new Vector3(transform.position.x,
                transform.position.y + groundedOffset, transform.position.z);

            // 해당 위치에 groundLayers와 겹치는 콜라이더가 있는지 검사
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius,
                groundLayers, QueryTriggerInteraction.Ignore);
        }

        // 점프 입력과 중력을 처리해 verticalVelocity 를 갱신
        void JumpAndGravity()
        {
            if (isGrounded)
            {
                // 지면에 닿으면 낙하 속도 리셋(완전히 0으로 두면 땅에 안 붙으므로 약간 눌러줌)
                if (verticalVelocity < 0f)
                {
                    verticalVelocity = -2f;
                }

                // 스페이스 키 입력 시 점프
                /*if (input.Jump)
                {
                    // 원하는 점프 높이에 도달하기 위한 초기 속도 계산 : v = √(h * -2 * g)
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }*/
            }

            // 중력 적용(최대 낙하 속도 이하로는 더 빨라지지 않게 제한)
            if (verticalVelocity > terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }
        #endregion

    }
}
