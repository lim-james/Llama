using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterStatistics))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterMovement : MonoBehaviour
{
    private int id;

    private Animator animator;
    private Rigidbody rig;
    private CharacterStatistics stats;
    private CharacterInventory inventory;
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
    [SerializeField]
    private float autoAimBounce = 0.1f;
    private float bt;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rig = GetComponent<Rigidbody>();
        stats = GetComponent<CharacterStatistics>();
        inventory = GetComponent<CharacterInventory>();
        playerCamera = Camera.main;

        bt = 0.0f;
    }

    private void Start()
    {
        //animator.SetTrigger("TriggerIdle"); // on entry state, it'll be idle always.
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
        float force = Mathf.Sqrt(x * x + y * y) * stats.speed;

        if(animator)
        {
            if (move.magnitude != 0 && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged") || animator.GetCurrentAnimatorStateInfo(0).IsName("Pickup"))) 
                animator.SetBool("IsWalking", true);
            else 
                animator.SetBool("IsWalking", false);
        }

        float rAngle = rig.transform.localEulerAngles.y + rotationAmount;

        if (inventory.holding)
        {
            Collider[] nearbyPlayers = Physics.OverlapSphere(transform.position, inventory._magnitude, LayerMask.GetMask("Character"));

            Vector3 p1 = transform.position;

            float closest = float.MaxValue;
            float angle = 0.0f;

            foreach (Collider player in nearbyPlayers)
            {
                if (transform.position == player.transform.position) continue;
                Vector3 p2 = player.transform.position;
                Vector3 diff = p2 - p1;
                if (Vector3.Dot(diff, transform.forward) < 0.0f) continue;

                float temp = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
                float da = Mathf.Abs(temp - rAngle);

                while (da > 360.0f) da -= 360.0f;

                if (da > 60.0f || da > closest) continue;

                closest = da;
                angle = temp;
            }

            if (angle != 0.0f)
                rAngle = angle;
        }

        rig.AddForce(forward * force, ForceMode.Impulse);
        rig.rotation = Quaternion.RotateTowards(rig.rotation, Quaternion.Euler(0, rAngle, 0), turnSpeed);
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