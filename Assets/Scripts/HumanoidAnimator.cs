using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(IHumanoidState))]
public sealed class HumanoidAnimator : MonoBehaviour
{
    private static readonly int IsGrounded = Animator.StringToHash("isGround");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Jump = Animator.StringToHash("Jump");

    [SerializeField] private Animator animator;
    [SerializeField] private new SpriteRenderer renderer;
    private IHumanoidState state;
    private bool prevGrounded = true;

    private void Awake()
    {
        TryGetComponent(out state);
    }

    private void Update()
    {
        animator.SetFloat(Horizontal, state.Velocity.x);
        animator.SetFloat(Vertical, state.Velocity.y);
        animator.SetBool(IsGrounded, state.Grounded);
        renderer.flipX = state.Velocity.x < 0f;
        if (prevGrounded && !state.Grounded)
        {
            animator.SetTrigger(Jump);
        }

        prevGrounded = state.Grounded;
    }
}