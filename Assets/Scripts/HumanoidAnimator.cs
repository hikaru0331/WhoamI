using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(GroundCheck))]
public sealed class HumanoidAnimator : MonoBehaviour
{
    private static readonly int IsGrounded = Animator.StringToHash("isGround");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Jump = Animator.StringToHash("Jump");

    [SerializeField] private Animator animator;
    [SerializeField] private new SpriteRenderer renderer;
    private Transform tf;
    private GroundCheck gc;
    private Vector2 prevPos;
    private Vector2 move;
    private bool prevGrounded = true;

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out gc);
    }

    private void Update()
    {
        animator.SetFloat(Horizontal, move.x);
        animator.SetFloat(Vertical, move.y);
        animator.SetBool(IsGrounded, gc.Grounded);
        if (!Mathf.Approximately(move.x, 0f))
        {
            renderer.flipX = move.x < 0f;
        }

        if (prevGrounded && !gc.Grounded)
        {
            animator.SetTrigger(Jump);
        }

        prevGrounded = gc.Grounded;
    }

    private void FixedUpdate()
    {
        move = ((Vector2)tf.position - prevPos).normalized;
        prevPos = tf.position;
    }
}