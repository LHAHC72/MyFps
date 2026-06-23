using UnityEngine;

namespace MyFps
{
    // 플레이어 인풋을 관리하는 클래스 : 뉴인풋 버전

    public class CharacterInput : MonoBehaviour
    {



        #region Valuables
        // inputSystem class 선언
        private InputSystem_Actions inputActions;

        // 이름 입력 입력 값 wasd
        private Vector2 move;

        // 마우스 입력 값 - 마우스 위치 이동
        private Vector2 look;



        // 외부에서 이동 입력값을 읽을 수 있도록 공개하는 프로퍼티
        // public Vector2 Move => move;
        public Vector2 Move {
            get => move;
            set => move = value;
        }

        // 외부에서 시선 입력값을 읽을 수 있도록 공개하는 프로퍼티
        public Vector2 Look {
            get => look;
            set => look = value;
        }

        #endregion





        #region Unity Event Methods
        private void Awake()
        {
            // inputSystem class 초기화
            inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            // inputSystem 활성화
            inputActions.Enable();
        }
        private void OnDisable()
        {
            // inputSystem 비활성화
            inputActions.Disable();
        }



        private void Update()
        {
            // wasd 입력값 처리 : 인스턴스이름.액션맵이름.액션이름.readValue()
            move = inputActions.Player.Move.ReadValue<Vector2>();
            Look = inputActions.Player.Look.ReadValue<Vector2>();

            

        }

        #endregion


    }
}