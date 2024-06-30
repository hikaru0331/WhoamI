using UnityEngine;

public static class PhysicsUtility
{
    /// <summary>
    /// vectorをonNormalに下ろした正射影ベクトルを返す
    /// </summary>
    /// <param name="vector">投影元のベクトル</param>
    /// <param name="onNormal">投影先のベクトル</param>
    /// <returns>正射影ベクトル</returns>
    public static Vector2 Project(in Vector2 vector, in Vector2 onNormal)
    {
        var dotProduct = Vector2.Dot(vector, onNormal);
        var sqrMag = onNormal.sqrMagnitude;
        var projection = dotProduct / sqrMag * onNormal;
        return projection;
    }
}