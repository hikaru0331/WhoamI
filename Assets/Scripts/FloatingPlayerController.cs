using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public sealed class FloatingPlayerController : MonoBehaviour
{
    public InputProvider inputProvider;
    public float speed;

    private Transform tf;
    private Rigidbody2D rb;

    private void Awake()
    {
        tf = transform;
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        rb.velocity = inputProvider.move * speed;
    }
}