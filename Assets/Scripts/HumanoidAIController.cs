using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public sealed class HumanoidAIController : ControllerBase, IAIController
{
    private const float JumpThreshold = 0.5f;

    public float speed;
    public float jumpHeight;

    private Transform tf;
    private Transform target;
    private Rigidbody2D rb;
    private GroundCheck gc;
    private float jumpTimestamp = -JumpThreshold;
    private bool requestJump;

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out rb);
        TryGetComponent(out gc);
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        var moveX = target.position.x - tf.position.x;
        if (Mathf.Abs(moveX) < 0.1f)
        {
            moveX = 0f;
        }

        var move = new Vector2(moveX, 0f).normalized;
        if (Mathf.Abs(target.position.y - tf.position.y) > 0.01f)
        {
            requestJump = true;
        }

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
    }

    private bool CanJump() => requestJump && gc.Grounded && Time.time - jumpTimestamp > JumpThreshold;

    // ReSharper disable once ParameterHidesMember
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}