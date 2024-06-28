using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class FloatingPlayerController : ControllerBase
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
        rb.velocity = inputProvider.move * speed;
    }
}