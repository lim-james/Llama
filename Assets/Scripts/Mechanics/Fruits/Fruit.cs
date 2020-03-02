using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fruit : MonoBehaviour
{
    private Rigidbody rig;

    [Header("Ground Check Data")]
    [SerializeField]
    private float groundDistance = 0.3f;
    [SerializeField]
    private Vector3 transformOffset;
    public LayerMask groundCheckIgnoreLayer;

    private bool grounded;
    private Vector3 groundNormal;
    private Vector3 forward;
    private RaycastHit hit;

    [Header("Player Base Data")]
    [SerializeField]
    private LayerMask playerBaseIgnoreLayer;
    public LayerMask defaultLayerMask;
    public LayerMask fruitLayer;

    public bool throwing = false;

    //public string fruitName;
    public FruitStats stats;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        if (!throwing)
            return;

        CheckGround();
        CorrectVelocity();
    }

    private void CheckGround()
    {
        Ray ray = new Ray();
        ray.origin = transform.position + transformOffset;
        ray.direction = Vector3.down;

        //Get if grounded (yes it is suppose to have = instead of ==)
        if (grounded = Physics.Raycast(ray, out hit, groundDistance, groundCheckIgnoreLayer))
        {
            groundNormal = hit.normal;
            transform.position = new Vector3(transform.position.x, hit.point.y + groundDistance, transform.position.z);

            Vector3 right = Vector3.Cross(rig.velocity.normalized, Vector3.up);
            forward = Vector3.Cross(right, groundNormal);
        }
        else
        {
            groundNormal = transform.up;
            forward = rig.velocity.normalized;
        }
    }

    private void CorrectVelocity()
    {
        float velMag = rig.velocity.magnitude;
        Vector3 movingDir = Vector3.ProjectOnPlane(rig.velocity.normalized, groundNormal);
        rig.velocity = movingDir * velMag;
    }

    public void OnCollisionEnter(Collision collision)
    {
        throwing = false;

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

    public void RemovePlayerBaseScore()
    {
        Collider[] playerBases = Physics.OverlapSphere(transform.position, 1.0f, playerBaseIgnoreLayer);

        if (playerBases.Length == 0)
            return;

        for (int i = 0; i < playerBases.Length; ++i)
        {
            playerBases[i].GetComponent<PlayerBase>().fruitCount -= stats.points;
        }
    }
    
    public void AddPlayerBaseScore()
    {
        Collider[] playerBases = Physics.OverlapSphere(transform.position, 1.0f, playerBaseIgnoreLayer);

        if (playerBases.Length == 0)
            return;

        for (int i = 0; i < playerBases.Length; ++i)
        {
            playerBases[i].GetComponent<PlayerBase>().fruitCount += stats.points;
        }
    }
}