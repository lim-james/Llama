using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionInput : MonoBehaviour
{

    private InputMaster input;
    public int controllerID = 0;

    private void Awake()
    {
        input = new InputMaster();
        input.Enable();

        // bind handlers
        if (controllerID == 0)
            input.devices = new[] { InputDevice.all[0] };
        else
            input.devices = new[] { Gamepad.all[controllerID - 1] };

        input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
    }

    private void SwitchTeamHandler(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
    }

}
