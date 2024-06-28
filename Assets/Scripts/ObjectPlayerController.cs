using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class ObjectPlayerController : ControllerBase
{
    private const float CheckRadius = 0.1f;

    public InputProvider inputProvider;
    public LayerMask groundLayer;
    public float speed;
    public Vector2 leftGroundOffset;
    public Vector2 rightGroundOffset;

    private Transform tf;
    private Rigidbody2D rb;

    public bool Grounded { get; private set; }

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        var pos = (Vector2)tf.position;
        Grounded = Physics2D.OverlapCircle(pos + leftGroundOffset, CheckRadius, groundLayer) ||
                   Physics2D.OverlapCircle(pos + rightGroundOffset, CheckRadius, groundLayer);
        if (!Grounded) return;

        var velocity = rb.velocity;
        rb.velocity = new Vector2(inputProvider.move.x, 0f) * speed +
                      PhysicsUtility.Project(velocity, Physics2D.gravity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)leftGroundOffset, CheckRadius);
        Gizmos.DrawWireSphere(transform.position + (Vector3)rightGroundOffset, CheckRadius);
    }
}