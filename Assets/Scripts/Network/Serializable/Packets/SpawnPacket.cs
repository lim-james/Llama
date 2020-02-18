using System;
using UnityEngine;

[Serializable]
public class SpawnPacket
{
    public int id;
    public int type;

    public SVector3 position; 
    public SQuaternion rotation; 

    public SpawnPacket(int _id, int _type, Vector3 _position, Quaternion _rotation)
    {
        id = _id;
        type = _type;
        position = new SVector3(_position);
        rotation = new SQuaternion(_rotation);
    }
}