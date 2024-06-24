using UnityEngine;

[DisallowMultipleComponent]
public sealed class InputProvider : MonoBehaviour
{
    public Vector2 move { get; private set; }
    public bool jump { get; private set; }

    private void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jump = Input.GetKeyDown(KeyCode.Space);
    }
}