using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceGravity : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField]
    private LayerMask collideLayer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, float.MaxValue, collideLayer))
        {
            rig.AddForce(Physics.gravity, ForceMode.Force);
        }
    }
}
