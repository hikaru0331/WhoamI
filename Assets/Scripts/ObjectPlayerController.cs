using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public sealed class ObjectPlayerController : ControllerBase
{
    public InputProvider inputProvider;
    public float speed;

    private Rigidbody2D rb;
    private GroundCheck gc;

    private void Awake()
    {
        TryGetComponent(out rb);
        TryGetComponent(out gc);
    }

    private void FixedUpdate()
    {
        if (!gc.Grounded) return;

        var velocity = rb.velocity;
        rb.velocity = new Vector2(inputProvider.move.x, 0f) * speed +
                      PhysicsUtility.Project(velocity, Physics2D.gravity);
    }
}