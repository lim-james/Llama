using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class MoveHandler : UnityEvent<Vector2> { }

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInventory))]
[RequireComponent(typeof(CharacterAdrenaline))]
public class CharacterInput : MonoBehaviour
{
    private CharacterInfo info;
    private CharacterMovement movement;
    private CharacterInventory inventory;
    private CharacterAdrenaline adrenaline;

    private InputMaster input;

    [Header("Axes values")]
    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;

    public bool moveable = true;

    private void Awake()
    {
        info = GetComponent<CharacterInfo>();
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
        adrenaline = GetComponent<CharacterAdrenaline>();

        input = new InputMaster();
        input.Enable();
    }

    private void Start()
    {
        // bind handlers
        if (!info.AI)
        {
            input.Enable();
            if (info.playerID == 0)
                input.devices = new[] { InputDevice.all[0] };
            else
                input.devices = new[] { Gamepad.all[info.playerID - 1] };
            // movement
            input.Player.Move.performed += context => OnMove(context);
            input.Player.Move.canceled += context => OnMove(context);
            // pick up
            input.Player.Pickup.performed += _ => inventory.PickUpNearbyFruit();
            // switch
            input.Player.Switch.performed += context => OnSwitch(context);
            // release 
            input.Player.Release.performed += context =>
            {
                if (context.ReadValue<float>() > 0.5f)
                    inventory.HoldFruit();
                else
                    inventory.ReleaseFruit();
            };
            // consume
            input.Player.Consume.performed += _ => adrenaline.Consume();
        }
    }

    private void OnDestroy()
    {
        input.Disable();
    }

    private void FixedUpdate()
    {
        if (moveable) movement.Move(horizontal, vertical);
    }

    /// Input handlers
    
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        horizontal = v.x;
        vertical = v.y;
    }

    private void OnSwitch(InputAction.CallbackContext context)
    {
        inventory.selectedIndex += (int)context.ReadValue<float>();
    }
}