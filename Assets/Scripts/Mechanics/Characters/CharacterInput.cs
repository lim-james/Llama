using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : MonoBehaviour
{
    private int id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";

    private CharacterMovement movement;

    [SerializeField]
    public float horizontal = 0.0f;
    [SerializeField]
    public float vertical = 0.0f;
    public bool moveable = true;
    public bool pickup = false;

    public int sendRate = 60;
    private float sendFrequency;
    private float sendTimer = 0.0f;

    public int controllerID = 1;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        sendFrequency = 1.0f / sendRate;
    }

    private void Update()
    {
        horizontal = Input.GetAxis(horizontalAxisName + controllerID.ToString());
        vertical = Input.GetAxis(verticalAxisName + controllerID.ToString());
        pickup = Input.GetButtonDown(pickUpName + controllerID.ToString());

        if (pickup)
            movement.PickUpNearbyFruit();

        sendTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (sendTimer >= sendFrequency)
        {
            movement.Move(horizontal, vertical);
            sendTimer = 0.0f;
        }
    }
}