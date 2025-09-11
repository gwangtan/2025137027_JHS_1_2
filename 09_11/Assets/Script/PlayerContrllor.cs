using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement settings
    [Header("이동 설정")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10;

    // Attack settings
    [Header("공격 설정")]
    public float attackDuration = 0.8f;
    public bool canMoveWhileAttacking = false;

    // Components
    [Header("컴포넌트")]
    public Animator animator;
    private CharacterController controller;
    private Camera playerCamera;

    // Current state
    private float currentSpeed;
    private bool isAttacking = false; // Check if attacking

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }

    // Handles movement
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // When there is input from either
        if (horizontal != 0 || vertical != 0)
        {
            // Set the direction the camera is facing as forward
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Set move direction
            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

            // Change to run mode if Left Shift is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }

            // Character controller movement input
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            // Look in the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Speed is 0 if not moving
            currentSpeed = 0;
        }
    }

    void UpdateAnimator()
    {
        // Calculate based on max speed (runSpeed) from 0 to 1
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runSpeed);
        animator.SetFloat("speed", animatorSpeed);
    }
}