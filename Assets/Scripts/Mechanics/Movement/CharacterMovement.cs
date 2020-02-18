using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private int id;

    private Rigidbody rig;
    public CharacterStats stats;
    public float turnSpeed = 1.0f;
    private Camera playerCamera;

    [SerializeField]
    private Vector3 groundNormal;
    private float characterOrginOffset = -0.5f;
    private float groundDistance = 0.3f;

    private float pickupRadius = 2.0f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    private void Update()
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

    public void PickUpNearbyFruit()
    {
        Collider[] objectsNearBy = Physics.OverlapSphere(transform.position, pickupRadius);

        float nearestDist = float.MaxValue;
        GameObject nearestFruit = null;

        for (int i = 0; i < objectsNearBy.Length; ++i)
        {
            if (!objectsNearBy[i].GetComponent<Fruit>())
                continue;

            Vector3 dir = objectsNearBy[i].transform.position - transform.position;

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, dir, out hit))
                continue;
            //if (hit.transform.gameObject != objectsNearBy[i])
                //continue;
            if (Vector3.Dot(transform.forward, dir) < 0)
                continue;
            if (dir.magnitude >= nearestDist)
                continue;

            nearestFruit = objectsNearBy[i].gameObject;
            nearestDist = dir.magnitude;
        }

        if (!nearestFruit)
            return;

        //Need server code here
        Destroy(nearestFruit);
    }

    public void ThrowFruit(Fruit fruit)
    {
        fruit.transform.SetParent(null);
        fruit.GetComponent<Rigidbody>().isKinematic = false;
        fruit.GetComponent<Collider>().isTrigger = false;
        fruit.GetComponent<Rigidbody>().AddForce(transform.forward * stats.characterStrength, ForceMode.Impulse);
    }
}