using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public sealed class HumanoidPlayerController : ControllerBase
{
    private const float JumpThreshold = 0.5f;

    public InputProvider inputProvider;
    public float speed;
    public float jumpHeight;

    private Rigidbody2D rb;
    private GroundCheck gc;
    private float jumpTimestamp = -JumpThreshold;
    private bool requestJump;

    public Vector2 Velocity { get; private set; }

    private void Awake()
    {
        TryGetComponent(out rb);
        TryGetComponent(out gc);
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

    private bool CanJump() => requestJump && gc.Grounded && Time.time - jumpTimestamp > JumpThreshold;
}