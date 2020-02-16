using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rig;
    public CharacterStats stats;
    public float turnSpeed = 1.0f;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(float x, float y, bool playerInput)
    {
        rig.velocity = new Vector3(x * stats.characterSpeed, rig.velocity.y, y * stats.characterSpeed);
        if (playerInput)
            rig.rotation = Quaternion.RotateTowards(rig.rotation, Quaternion.Euler(0, Mathf.Atan2(rig.velocity.x, rig.velocity.z) * Mathf.Rad2Deg, 0), turnSpeed);
    }
}
