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
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>(); // Virtual Camera�� POV ������Ʈ ��������
    }

    void Update()
    {
        // ���� ��� �ִ��� Ȯ��
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ���鿡 ���̱�
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ī�޶� ���� ���� ���
        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized; // �̵� ���� = ī�޶� forward/right ����
        
        
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

        float cameraYaw = pov.m_HorizontalAxis.Value; // �̿콺 ���� ȸ����
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        // ����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}
