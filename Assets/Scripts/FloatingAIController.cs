using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class FloatingAIController : ControllerBase, IAIController
{
    public float speed;

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
        var moveX = target.position.x - tf.position.x;
        if (Mathf.Abs(moveX) < 0.1f)
        {
            moveX = 0f;
        }

        var direction = new Vector2(moveX, 0f).normalized;
        rb.velocity = direction * speed;
    }

    // ReSharper disable once ParameterHidesMember
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}