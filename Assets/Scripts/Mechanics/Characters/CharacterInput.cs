using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : MonoBehaviour
{
    private int id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";
    public string adrenalineButtonName = "Adrenaline";

    private CharacterMovement movement;

    [SerializeField]
    public float horizontal = 0.0f;
    [SerializeField]
    public float vertical = 0.0f;
    public bool moveable = true;

    public int controllerID = 1;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (!moveable)
            return;

        horizontal = Input.GetAxis(horizontalAxisName + controllerID.ToString());
        vertical = Input.GetAxis(verticalAxisName + controllerID.ToString());

        if (Input.GetButtonDown(pickUpName + controllerID.ToString()))
            movement.PickUpNearbyFruit();
        if (Input.GetButtonDown(adrenalineButtonName + controllerID.ToString()))
            movement.ActivateAdrenaline();
    }

    private void FixedUpdate()
    {
        movement.Move(horizontal, vertical);
    }
}