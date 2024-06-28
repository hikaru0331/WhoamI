using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class FloatingAIController : ControllerBase, IAIController
{
    public float speed;

    private Rigidbody2D rb;
    private Transform target;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        var direction = new Vector2(target.position.x - transform.position.x, 0f).normalized;
        rb.velocity = direction * speed;
    }

    // ReSharper disable once ParameterHidesMember
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}