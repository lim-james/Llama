using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fruit : MonoBehaviour
{
    [SerializeField]
    private uint id;
    private Rigidbody rig;

    //public string fruitName;
    public FruitStats stats;

    public uint ID
    {
        set { id = value; }
        get{ return id; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
