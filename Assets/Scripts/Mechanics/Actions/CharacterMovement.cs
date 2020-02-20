using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStatistics))]
public class CharacterMovement : MonoBehaviour
{
    private int id;

    private Rigidbody rig;
    private CharacterStats stats;
    public float turnSpeed = 1.0f;
    private Camera playerCamera;

    [SerializeField]
    private bool grounded;
    public LayerMask groundLayer;
    private Vector3 groundNormal;
    private RaycastHit groundHit;
    public float maxGroundAngle;
    private float groundAngle;
    public float characterOrginOffset = -0.5f;
    public float groundDistance = 0.3f;
    private Vector3 forward;

    [SerializeField]
    private float pickupRadius = 2.0f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    private void Start()
    {
        stats = GetComponent<CharacterStatistics>().stats;
    }

    private void Update()
    {
        CheckGround();
        CalculateGroundAngle();
        ApplyGravity();
        Debug.DrawRay(transform.position + new Vector3(0, characterOrginOffset, 0), -transform.up * groundDistance, Color.red);

        Debug.DrawRay(transform.position + new Vector3(0, 0, characterOrginOffset), forward * 100, Color.blue);
    }


    private void FixedUpdate()
    {
        ApplyGravity();
        //Move(GetComponent<CharacterInput>().horizontal, GetComponent<CharacterInput>().vertical);
    }

    public void OnMove()
    {
        Debug.Log("Hello there");
    }

    public void ApplyGravity()
    {
        if (grounded)
            return;

        rig.AddForce(Physics.gravity, ForceMode.Acceleration);
    }

    public void Move(float x, float y)
    {
        if (groundAngle >= maxGroundAngle)
            return;

        Vector3 move = new Vector3(x, 0, y);
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, groundNormal);
        float rotationAmount = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        float force = Mathf.Sqrt(x * x + y * y) * stats.characterSpeed;

        rig.AddForce(forward * force, ForceMode.Impulse);
        rig.rotation = Quaternion.RotateTowards(rig.rotation, Quaternion.Euler(0, rig.transform.localEulerAngles.y + rotationAmount, 0), turnSpeed);
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, characterOrginOffset, 0), -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, groundDistance, groundLayer))
        {
            grounded = true;
            groundNormal = hit.normal;
            forward = Vector3.Cross(transform.right, groundNormal);
        }
        else 
        {
            grounded = false;
            forward = transform.forward;
        }
    }

    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(groundHit.normal, transform.forward);
    }

    public void ActivateAdrenaline()
    {

    }
}