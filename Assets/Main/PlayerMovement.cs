using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float fallThreshold = -0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;
    public bool isJumping;
    public bool wasGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            if (isJumping)
            {
                isJumping = false;
                playerAnimator.SetJump(false);
                playerAnimator.SetFreeFall(false);
            }

            if (!wasGrounded)
            {
                playerAnimator.SetGrounded(true);
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 move = right * x + forward * z;
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            isJumping = true;
            playerAnimator.SetGrounded(false);
            playerAnimator.SetJump(true);
            playerAnimator.SetFreeFall(false);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 10f * Time.deltaTime);
        }

        if (!isGrounded && !isJumping && velocity.y < fallThreshold)
        {
            playerAnimator.SetFreeFall(true);
        }
       
    }
}