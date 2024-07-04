using UnityEngine;

[DisallowMultipleComponent]
public sealed class FlagAnimator : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");

    [SerializeField] private Animator animator;
    [SerializeField] private new SpriteRenderer renderer;
    private Transform tf;
    private Vector2 prevPos;
    private Vector2 move;

    private void Awake()
    {
        tf = transform;
    }

    private void Update()
    {
        animator.SetBool(Move, move != Vector2.zero);
        if (!Mathf.Approximately(move.x, 0f))
        {
            renderer.flipX = move.x > 0f;
        }
    }

    private void FixedUpdate()
    {
        move = ((Vector2)tf.position - prevPos).normalized;
        prevPos = tf.position;
    }
}