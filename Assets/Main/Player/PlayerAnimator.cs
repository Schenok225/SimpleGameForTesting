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

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
    public void SetMotionSpeed(float motionSpeed)
    {
        animator.SetFloat("MotionSpeed", motionSpeed);
    }

    public void SetIsJumpingFalse()
    {
        animator.SetBool("Grounded", false);
    }
    public bool GetAnimatorBool(string name)
    {
        if (animator != null)
        {
            return animator.GetBool(name);
        }
        return false;
    }
}
