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
    public string throwButtonName = "Throw";

    private CharacterMovement movement;
    private CharacterInventory inventory;

    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;
    [SerializeField]
    private float throwForce = 0.0f;
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
        if (Input.GetButton(throwButtonName + controllerID.ToString()))
            throwForce = Mathf.Min(throwForce + Time.deltaTime * movement.stats.characterStrength * movement.stats.characterStrength, movement.stats.characterStrength * movement.stats.characterStrength);
        if (Input.GetButtonUp(throwButtonName + controllerID.ToString()))
        {
            inventory.DropFruit(throwForce);
            throwForce = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        movement.Move(horizontal, vertical);
    }
}