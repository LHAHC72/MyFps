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

            // 이동 : 방향 * Time.deltaTime * 속도
            controller.Move(inputDirection.normalized * Time.deltaTime * moveSpeed);

        }
        #endregion

    }
}
