using System;
using UnityEngine;

[Serializable]
public class SpawnPacket
{
    public SVector3 position; 
    public SQuaternion rotation; 

    public SpawnPacket(Vector3 _position, Quaternion _rotation)
    {
        position = new SVector3(_position);
        rotation = new SQuaternion(_rotation);
    }
}