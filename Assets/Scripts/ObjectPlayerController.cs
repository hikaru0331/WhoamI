using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class ObjectPlayerController : MonoBehaviour
{
    public InputProvider inputProvider;
    public float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        var velocity = rb.velocity;
        rb.velocity = new Vector2(inputProvider.move.x, 0f) * speed +
                      PhysicsUtility.Project(velocity, Physics2D.gravity);
    }
}