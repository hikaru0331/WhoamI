using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class HumanoidPlayerController : ControllerBase, IHumanoidState
{
    private const float CheckRadius = 0.1f;
    private const float JumpThreshold = 0.5f;

    public InputProvider inputProvider;
    public LayerMask groundLayer;
    public float speed;
    public Vector2 groundOffset;
    public float jumpHeight;

    private Transform tf;
    private Rigidbody2D rb;
    private float jumpTimestamp = -JumpThreshold;
    private bool requestJump;

    public Vector2 Velocity { get; private set; }
    public bool Grounded { get; private set; }

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out rb);
    }

    private void Update()
    {
        if (inputProvider.jump)
        {
            requestJump = true;
        }
    }

    private void FixedUpdate()
    {
        // TODO: 最適化
        var move = new Vector2(inputProvider.move.x, 0f);
        var velocity = rb.velocity;
        var gravityVelocity = PhysicsUtility.Project(velocity, Physics2D.gravity);
        rb.velocity = move * speed + gravityVelocity;
        Grounded = Physics2D.OverlapCircle((Vector2)tf.position + groundOffset, CheckRadius, groundLayer);

        if (CanJump())
        {
            jumpTimestamp = Time.time;
            var gravity = rb.gravityScale * Physics2D.gravity;
            // ジャンプの初速を計算 v = sqrt(2 * g * h)
            var jumpVel = Mathf.Sqrt(2f * jumpHeight * -gravity.y);
            rb.velocity = new Vector2(velocity.x, jumpVel);
        }

        requestJump = false;
        Velocity = rb.velocity;
    }

    private bool CanJump() => requestJump && Grounded && Time.time - jumpTimestamp > JumpThreshold;

    private void OnDrawGizmos()
    {
        Gizmos.color = Grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)groundOffset, CheckRadius);
    }
}