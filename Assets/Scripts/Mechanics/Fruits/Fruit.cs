﻿using System.Collections;
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

    public LayerMask layer;

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

    private void Update()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, float.MaxValue, layer))
        {
            rig.AddForce(Physics.gravity, ForceMode.Force);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<CharacterMovement>())
            return;

        Vector3 dir = (collision.transform.position - transform.position).normalized;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * rig.velocity.magnitude, ForceMode.Impulse);

        if(rig.velocity.magnitude > 1) // Fruit must be in motion to trigger damage?
        {
            collision.gameObject.GetComponent<CharacterMovement>().GetAnimator.SetTrigger("TriggerDamage");

            // Hit
            if (!collision.gameObject.transform.GetChild(4).gameObject.activeInHierarchy)
                collision.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            else
                collision.gameObject.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>().Play();

            // Star
            if (!collision.gameObject.transform.GetChild(3).gameObject.activeInHierarchy)
                collision.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            else
                collision.gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
        }

        rig.velocity = Vector3.Reflect(rig.velocity.normalized, collision.contacts[0].normal);
    }
}
