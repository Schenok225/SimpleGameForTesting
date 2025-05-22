using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetFreeFall(bool freeFall)
    {
        animator.SetBool("FreeFall", freeFall);
    }
    public void SetJump(bool jump)
    {
        animator.SetBool("Jump", jump);
    }
    public void SetGrounded(bool grounded)
    {
        animator.SetBool("Grounded", grounded);
    }
    public void SetIsJumpingFalse()
    {
        animator.SetBool("Grounded", false);
    }
}
