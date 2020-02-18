using System;
using UnityEngine;

[Serializable]
public class MovementPacket
{
    public int id;

    public float horizontal;
    public float vertical;

    public MovementPacket(int _id, float _horizontal, float _vertical)
    {
        id = _id;
        horizontal = _horizontal;
        vertical = _vertical;
    }
}