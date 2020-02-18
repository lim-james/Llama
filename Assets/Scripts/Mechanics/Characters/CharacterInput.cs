using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterInput : MonoBehaviour
{
    private int id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";

    private CharacterMovement movement;
    private CharacterInventory inventory;

    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;
    public bool moveable = true;
    public bool pickup = false;

    public int sendRate = 60;
    private float sendFrequency;
    private float sendTimer = 0.0f;

    public int controllerID;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
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
            inventory.PickUpNearbyFruit();

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