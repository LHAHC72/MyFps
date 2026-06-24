using UnityEngine;

namespace MyFps
{
    // 마우스 이동 따라 플레이어의 시선 이동을 관리하는 클래스
    public class MouseMove : MonoBehaviour
    {
        #region
        public Transform cameraRoot;    // 카메라 트래킹 트랜스폼

        // 참조
        private CharacterInput input;

        // 회전
        [SerializeField] private float rotationSpeed = 1.0f; // 회전 속도

        [SerializeField] private float sensivity = 100f; // 마우스 감도

        private float cameraTargetPitch = 0f;   // 카메라 회전 연산 값(위, 아래)
        private float rotationVelocity = 0f;    // 카메라 회전 속도

        [SerializeField] private float topClamp = 70f; // 카메라 상단 회전 제한
        [SerializeField] private float bottomClamp = -30f; // 카메라 하단 회전 제한

        #endregion

        #region
        private void Awake()
        {
            // 참조 초기화
            input = GetComponent<CharacterInput>();
        }

        private void LateUpdate()
        {
            // 카메라 회전
            CameraRotation();
        }

        #endregion

        #region Custom method

        void CameraRotation()
        {
            // 입력 값 체크
            if (input.Look.sqrMagnitude < 0.01f)
            {
                return;
            }

            // 좌우(플레이어의 프랜스폼) 회전
            rotationVelocity = input.Look.x * rotationSpeed * Time.deltaTime * sensivity; // 마우스 좌우 이동에 따른 회전 속도 계산
            transform.Rotate(Vector3.up * rotationVelocity); // 플레이어 회전

            // 위아래(카메라 루트의 프랜스폼) 회전
            cameraTargetPitch -= input.Look.y * rotationSpeed * Time.deltaTime * sensivity; // 마우스 상하 이동에 따른 회전 속도 계산
            cameraTargetPitch = ClampAngle(cameraTargetPitch, bottomClamp, topClamp); // 회전 제한
            cameraRoot.localRotation = Quaternion.Euler(cameraTargetPitch, 0f, 0f); // 카메라 회전

        }

        private float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        #endregion

    }
}
