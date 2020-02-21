using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionInput : MonoBehaviour
{
    // input
    private InputMaster input;
    [SerializeField]
    private int controllerID = 0;

    [SerializeField]
    private int index;
    [SerializeField]
    private int characterIndex;
    [SerializeField]
    private TeamGroup teams;
    [SerializeField]
    private CharacterGroup characters;
    //[SerializeField]
    public GameObject characterModel;

    // referneces
    private Image background;
    private Text label;

    private GameObject temp;

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
        input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);

        background = GetComponent<Image>();
        label = GetComponentInChildren<Text>();
        //characterModel = GetComponent<GameObject>();
    }

    private void Start()
    {
        background.color = teams.group[index].color;

        temp = Instantiate(characters.group[characterIndex].characterModel, characterModel.transform.position, characterModel.transform.rotation);
    }

    private void SwitchTeamHandler(InputAction.CallbackContext context)
    {
        index += (int)context.ReadValue<float>();

        if (index < 0)
            index = teams.group.Length - 1;
        else if (index >= teams.group.Length)
            index = 0;

        background.color = teams.group[index].color;
        label.text = teams.group[index].name;
    }

    private void SwitchCharacterHandler(InputAction.CallbackContext context)
    {
        characterIndex += (int)context.ReadValue<float>();

        if (characterIndex < 0)
            characterIndex = characters.group.Length - 1;
        else if (characterIndex >= teams.group.Length)
            characterIndex = 0;

        Destroy(temp);
        temp = Instantiate(characters.group[characterIndex].characterModel, characterModel.transform.position, characterModel.transform.rotation);
    }
}
