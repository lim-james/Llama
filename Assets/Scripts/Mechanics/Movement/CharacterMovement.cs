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

    [SerializeField]
    private Vector3 groundNormal;
    private float characterOrginOffset = -0.5f;
    private float groundDistance = 0.3f;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, characterOrginOffset, 0), -transform.up * groundDistance, Color.red);
    }

    public void Move(float x, float y)
    {
        Vector3 move = new Vector3(x, 0, y);
        move = transform.InverseTransformDirection(move);
        CheckGround();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        float rotationAmount = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        float force = Mathf.Sqrt(x * x + y * y) * stats.characterSpeed;

        rig.AddForce(transform.forward * force, ForceMode.Impulse);
        rig.rotation = Quaternion.RotateTowards(rig.rotation, Quaternion.Euler(0, rig.transform.localEulerAngles.y + rotationAmount, 0), turnSpeed);
    }

    public void CheckGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, characterOrginOffset, 0), -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, groundDistance))
        {
            groundNormal = hit.normal;
        }
    }
}