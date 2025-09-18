using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;
    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public CInemachineSwitcher switcher;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>(); // Virtual Camera의 POV 컴포넌트 가져오기
    }

    void Update()
    {
        // 땅에 닿아 있는지 확인
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 지면에 붙이기
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 카메라 기준 방향 계산
        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized; // 이동 방향 = 카메라 forward/right 기준
        
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
            virtualCam.m_Lens.FieldOfView = 80f;
        }
        else
        {
            speed = 5;
            virtualCam.m_Lens.FieldOfView = 60f;

        }

        if (!switcher.usingFreeLook)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        float cameraYaw = pov.m_HorizontalAxis.Value; // 미우스 최종 회전값
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        // 점프
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}
