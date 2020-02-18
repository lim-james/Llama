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
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;
    public bool moveable = true;
    public bool pickup = false;

    public int sendRate = 60;
    private float sendFrequency;
    private float sendTimer = 0.0f;

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
        horizontal = Input.GetAxis(horizontalAxisName);
        vertical = Input.GetAxis(verticalAxisName);
        pickup = Input.GetButtonDown(pickUpName);

        sendTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (sendTimer >= sendFrequency)
        {
            movement.Move(horizontal, vertical);

            if (pickup)
                movement.PickUpNearbyFruit();
            sendTimer = 0.0f;
        }
    }
}