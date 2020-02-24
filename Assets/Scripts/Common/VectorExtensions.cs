using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Sign(this Vector3 v)
    {
        return new Vector3(
            Mathf.Sign(v.x), 
            Mathf.Sign(v.y), 
            Mathf.Sign(v.z)
        );
    }
}
