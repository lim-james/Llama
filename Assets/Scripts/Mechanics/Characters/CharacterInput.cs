using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterInput : MonoBehaviour
{
    private int id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";
    public string adrenalineButtonName = "Adrenaline";

    private CharacterMovement movement;
    private CharacterInventory inventory;

    [SerializeField]
    public float horizontal = 0.0f;
    [SerializeField]
    public float vertical = 0.0f;
    public bool moveable = true;

    public int controllerID = 1;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
    }

    private void Update()
    {
        if (!moveable)
            return;

        horizontal = Input.GetAxis(horizontalAxisName + controllerID.ToString());
        vertical = Input.GetAxis(verticalAxisName + controllerID.ToString());

        if (Input.GetButtonDown(adrenalineButtonName + controllerID.ToString()))
            movement.ActivateAdrenaline();
        if (Input.GetButtonDown(pickUpName + controllerID.ToString()))
            inventory.PickUpNearbyFruit();
    }

    private void FixedUpdate()
    {
        movement.Move(horizontal, vertical);
    }
}