using UnityEngine;

public interface IHumanoidState
{
    public Vector2 Velocity { get; }
    public bool Grounded { get; }
}