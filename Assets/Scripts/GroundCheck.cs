using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder(-10)]
public sealed class GroundCheck : MonoBehaviour
{
    private const float CheckRadius = 0.1f;

    public LayerMask groundLayer;
    public Vector2 leftGroundOffset;
    public Vector2 rightGroundOffset;
    private Transform tf;

    public bool Grounded { get; private set; }

    private void Awake()
    {
        tf = transform;
    }

    private void FixedUpdate()
    {
        var pos = (Vector2)tf.position;
        Grounded = Physics2D.OverlapCircle(pos + leftGroundOffset, CheckRadius, groundLayer) ||
                   Physics2D.OverlapCircle(pos + rightGroundOffset, CheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)leftGroundOffset, CheckRadius);
        Gizmos.DrawWireSphere(transform.position + (Vector3)rightGroundOffset, CheckRadius);
    }
}