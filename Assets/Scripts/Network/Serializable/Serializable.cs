using System;
using UnityEngine;

[Serializable]
public class SColor
{
    public float r, g, b, a;

    public SColor(Color color)
    {
        r = color.r;
        g = color.g;
        b = color.b;
        a = color.a;
    }

    public Color GetColor()
    {
        return new Color(r, g, b, a);
    }
}

[Serializable]
public class SVector3
{
    public float x, y, z;

    public SVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}

[Serializable]
public class SQuaternion
{
    public float x, y, z, w;

    public SQuaternion(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Quaternion GetQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }
}
