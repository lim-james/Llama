using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private AudioPlayer player;

    private InputMaster input;

    [Header("Axes values")]
    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;

    public UIController ui;

    public EndGame endGame;

    public bool moveable = true;

    private bool doOnce;

    private void Awake()
    {
        info = GetComponent<CharacterInfo>();
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
        adrenaline = GetComponent<CharacterAdrenaline>();

        input = new InputMaster();
        input.Enable();

        
        ui = GameObject.Find("UI Canvas").GetComponent<UIController>();

        endGame = GameObject.Find("EndGame").GetComponent<EndGame>();

        doOnce = false;
    }

    private void Start()
    {
        // bind handlers
        if (!info.AI)
        {
            input.Enable();

            ControllerManager manager = ControllerManager.Instance;

            int controllerOffset = 0; ;

            if (manager.type == ControllerManager.Type.STANDARD)
                controllerOffset = 1;
            else if (manager.type == ControllerManager.Type.SPLIT_KEYBOARD)
                controllerOffset = 2;
            else if (manager.type == ControllerManager.Type.ALL_CONTROLLER)
                controllerOffset = 0;


            if (info.playerID < controllerOffset)
            {
                input.devices = new[] { InputDevice.all[0] };
            }
            else
            {
                if (Gamepad.all.Count > info.playerID + controllerOffset)
                {
                    input.devices = new[] { Gamepad.all[info.playerID - controllerOffset] };
                }
            }

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
            // pause game
            input.Player.Pause.performed += context => OnPause(context);
            input.Player.ChangeSelection.performed += context => OnPauseSwitch(context);
            input.Player.Select.performed += context => OnSelect(context);

            player = GameObject.FindGameObjectWithTag("System").GetComponent<AudioPlayer>();
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

    private void Update()
    {
        if (endGame.allowRestart && !doOnce)
        {
            if (!info.AI)
            {
                input.Player.Restart.performed += context => OnRestart(context);
                doOnce = true;
            }
        }
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

    private void OnPause(InputAction.CallbackContext context)
    {
        ui.TogglePause();
    }

    private void OnPauseSwitch(InputAction.CallbackContext context)
    {
        if (ui.paused)
            ui.ToggleButton(context.ReadValue<float>());
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        if (ui.paused)
            ui.Enter();
    }

    private void OnRestart(InputAction.CallbackContext context)
    {
        player.PlayBGM(0,true);
        SceneManager.LoadScene("Lobby");
    }
}