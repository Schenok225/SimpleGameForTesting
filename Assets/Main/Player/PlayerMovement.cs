using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float fallThreshold = -0.5f;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float fallDeathY = -10f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;
    private bool wasGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            if (!wasGrounded)
            {
                velocity.y = -2f;
                isJumping = false;
                playerAnimator.SetGrounded(true);
                playerAnimator.SetJump(false);
                playerAnimator.SetFreeFall(false);
            }
            else if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true;
                playerAnimator.SetGrounded(false);
                playerAnimator.SetJump(true);
                playerAnimator.SetFreeFall(false);
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            if (wasGrounded && !isJumping)
            {
                playerAnimator.SetGrounded(false);
            }

            velocity.y += gravity * Time.deltaTime;

            if (isJumping && velocity.y < 0)
            {
                playerAnimator.SetJump(false);
                playerAnimator.SetFreeFall(true);
            }
            else if (velocity.y < fallThreshold && !playerAnimator.GetAnimatorBool("FreeFall"))
            {
                playerAnimator.SetFreeFall(true);
                playerAnimator.SetJump(false);
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
        controller.Move(velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 10f * Time.deltaTime);
        }

        float speed = new Vector3(x, 0, z).magnitude;
        playerAnimator.SetSpeed(speed);
        playerAnimator.SetMotionSpeed(move.magnitude);

        if (transform.position.y < fallDeathY)
        {
            GetComponent<PlayerHealth>().Die();
        }


    }
}