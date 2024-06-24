using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class AvatarMove : MonoBehaviour
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
    private bool grounded;
    private float jumpTimestamp = -JumpThreshold;
    private bool requestJump;

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
        var move = new Vector2(inputProvider.move.x, 0f);
        var velocity = rb.velocity;
        var gravityVelocity = Project(velocity, Physics2D.gravity);
        rb.velocity = move * speed + gravityVelocity;
        grounded = Physics2D.OverlapCircle((Vector2)tf.position + groundOffset, CheckRadius, groundLayer);

        if (CanJump())
        {
            jumpTimestamp = Time.time;
            // ジャンプの初速を計算 v = sqrt(2 * g * h)
            var jumpVel = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            rb.velocity = new Vector2(velocity.x, jumpVel);
        }

        requestJump = false;
    }

    private bool CanJump() => requestJump && grounded && Time.time - jumpTimestamp > JumpThreshold;

    /// <summary>
    /// vectorをonNormalに下ろした正射影ベクトルを返す
    /// </summary>
    /// <param name="vector">投影元のベクトル</param>
    /// <param name="onNormal">投影先のベクトル</param>
    /// <returns>正射影ベクトル</returns>
    private static Vector2 Project(Vector2 vector, Vector2 onNormal)
    {
        var dotProduct = Vector2.Dot(vector, onNormal);
        var sqrMag = onNormal.sqrMagnitude;
        var projection = dotProduct / sqrMag * onNormal;
        return projection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)groundOffset, CheckRadius);
    }
}