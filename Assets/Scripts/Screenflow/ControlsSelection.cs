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
    private int index;

    // references
    [SerializeField]
    private RawImage Keyboard;
    [SerializeField]
    private RawImage DualKeyboard;
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
    }

    // Update is called once per frame
    void Update()
    {
        // do stuff here
        if (Keyboard.texture == selectedButton)
        {

        }
        else if (DualKeyboard.texture == selectedButton)
        {

        }
        else if (Controllers.texture == selectedButton)
        {

        }
    }

    void OnDestroy()
    {
        input.Disable();
    }

    private void SwitchSelectedButton(InputAction.CallbackContext context)
    {
        index += (int)context.ReadValue<float>();

        if (index < 0)
            index = 0;
        else if (index > 2)
            index = 2;

        switch (index)
        {
            case 0:
                Keyboard.texture = selectedButton;
                DualKeyboard.texture = normalButton;
                Controllers.texture = normalButton;
                break;
            case 1:
                Keyboard.texture = normalButton;
                DualKeyboard.texture = selectedButton;
                Controllers.texture = normalButton;
                break;
            case 2:
                Keyboard.texture = normalButton;
                DualKeyboard.texture = normalButton;
                Controllers.texture = selectedButton;
                break;
        }

        // Play Audio
        player.PlaySFX(switchAudio);
    }
}
