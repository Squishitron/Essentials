using UnityEngine;

public class NewCharactorAnimationController : MonoBehaviour
{
    public CharacterController controller;
    private Animator animator;
    private int jumpCount = 0;
    private int maxJumps = 1;

    private readonly int 
        run = Animator.StringToHash("Run"),
        idle = Animator.StringToHash("Idle"),
        jump = Animator.StringToHash("Jump"),
        wallJump = Animator.StringToHash("WallJump");

    private void Start()
    {
        // Cache the Animator component attached to CharacterArt
        animator = GetComponent<Animator>();
        //controller in parent object
        controller = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            animator.SetBool(jump, true);
            jumpCount++;
        }
        else if (controller.isGrounded && jumpCount >= maxJumps)
        {
            animator.SetBool(jump, false);
            jumpCount = 0;
        }

        if (Mathf.Abs(horizontalMove) > 0 && jump != 1)
        {
            animator.SetBool(run, true);
            animator.SetBool(idle, false);
        }
        else
        {
            animator.SetBool(run, false);
            animator.SetBool(idle, true);
        }
    }
}