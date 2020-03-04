using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlsSelection : MonoBehaviour
{
    private InputMaster input;
    [SerializeField]
    private int controllerID = 0;

    [Header("Button Type")]
    [SerializeField]
    private Texture normalButton;
    [SerializeField]
    private Texture selectedButton;
    [SerializeField]
    private bool allControllers;

    // references
    [SerializeField]
    private RawImage Keyboard;
    [SerializeField]
    private RawImage Controllers;

    [Header("Sound")]
    public AudioPlayer player;
    [SerializeField]
    private AudioClip switchAudio;

    public bool connected { get; private set; }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("System").GetComponent<AudioPlayer>();

        input = new InputMaster();
        input.Enable();

        // bind handlers
        if (controllerID == 0)
        {
            connected = true;
            input.devices = new[] { InputDevice.all[0] };
            input.Home.ChangeSelection.performed += context => SwitchSelectedButton(context);
        }
        else
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Home.ChangeSelection.performed += context => SwitchSelectedButton(context);
            }
            else
            {
                connected = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Enable();

        ControllerManager manager = ControllerManager.Instance;

        if (manager.type == ControllerManager.Type.ALL_CONTROLLER)
        {
            Keyboard.texture = normalButton;
            Controllers.texture = selectedButton;
        }
        else
        {
            Keyboard.texture = selectedButton;
            Controllers.texture = normalButton;
        }
    }

    void OnDestroy()
    {
        input.Disable();
    }

    private void SwitchSelectedButton(InputAction.CallbackContext context)
    {
        allControllers = !allControllers;

        ControllerManager manager = ControllerManager.Instance;

        if (allControllers)
        {
            manager.type = ControllerManager.Type.ALL_CONTROLLER;
            Keyboard.texture = normalButton;
            Controllers.texture = selectedButton;
        }
        else
        {
            manager.type = ControllerManager.Type.STANDARD;
            Keyboard.texture = selectedButton;
            Controllers.texture = normalButton;
        }

        // Play Audio
        player.PlaySFX(switchAudio);
    }

    public void KeyboardSelected()
    {
        allControllers = false;
        ControllerManager.Instance.type = ControllerManager.Type.STANDARD;
        Keyboard.texture = selectedButton;
        Controllers.texture = normalButton;
    }

    public void ControllerSelected()
    {
        allControllers = true;
        ControllerManager.Instance.type = ControllerManager.Type.ALL_CONTROLLER;
        Keyboard.texture = normalButton;
        Controllers.texture = selectedButton;
    }
}
