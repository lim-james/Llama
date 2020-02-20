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
    private int id;

    private InputMaster input;
    private CharacterMovement movement;
    private CharacterInventory inventory;
    private CharacterAdrenaline adrenaline;

    [Header("Axes values")]
    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;

    public bool moveable = true;
    public int controllerID = 0;

    private void Awake()
    {
        input = new InputMaster();
        input.Enable();

        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
        adrenaline = GetComponent<CharacterAdrenaline>();
    }

    private void Start()
    {
        // bind handlers
        if (controllerID == 0)
            input.devices = new[] { InputDevice.all[0] };
        else
            input.devices = new[] { Gamepad.all[controllerID - 1] };
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
        inventory.SelectedIndex += (int)context.ReadValue<float>();
    }

}