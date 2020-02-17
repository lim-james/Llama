using System;
using UnityEngine;

[Serializable]
public class TransformPacket
{
    public int id;
    public float t;

    public SVector3 position; 
    public SQuaternion rotation; 

    public TransformPacket(int _id, float _t, Vector3 _position, Quaternion _rotation)
    {
        id = _id;
        t = _t;
        position = new SVector3(_position);
        rotation = new SQuaternion(_rotation);
    }
}

