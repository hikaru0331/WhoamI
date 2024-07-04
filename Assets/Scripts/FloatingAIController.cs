using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class FloatingAIController : ControllerBase, IAIController
{
    public float speed;
    public bool canMoveY = true;

    private Transform tf;
    private Rigidbody2D rb;
    private Transform target;

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        var move = target.position - tf.position;
        if (Mathf.Abs(move.x) < 0.1f)
        {
            move.x = 0f;
        }

        if (!canMoveY || Mathf.Abs(move.y) < 0.1f)
        {
            move.y = 0f;
        }

        var direction = move.normalized;
        rb.velocity = direction * speed;
    }

    // ReSharper disable once ParameterHidesMember
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}