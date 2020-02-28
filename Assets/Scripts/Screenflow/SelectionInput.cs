using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectionInput : MonoBehaviour
{
    // input
    private InputMaster input;
    [SerializeField]
    private int controllerID = 0;

    [Header("Player input")]
    public int index;
    public int characterIndex;

    [Header("Data")]
    [SerializeField]
    private TeamGroup teams;
    [SerializeField]
    private CharacterGroup characters;
    [SerializeField]
    private GameObject characterModel;
    [SerializeField]
    private RawImage strength;
    [SerializeField]
    private RawImage speed;
    [SerializeField]
    private RawImage weight;
    [SerializeField]
    private RawImage balance;

    // references
    //private Image background;
    private RawImage background;
    private Text label;
    public bool connected { get; private set; }

    private GameObject temp;

    private Versus versusText;

    // animation
    [SerializeField]
    private UnityEvent OnModeChange;

    private void Awake()
    {
        input = new InputMaster();
        input.Enable();

        // bind handlers
        if (controllerID == 0)
        {
            connected = true;
            input.devices = new[] { InputDevice.all[0] };
            input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
            input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);
        }
        else
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
                input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);
            }
            else
            {
                connected = false;
            }
        }

        background = GetComponent<RawImage>();
        label = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        input.Enable();
        //background.color = teams.group[index].color;
        background.texture = teams.group[index].teamBackground;

        temp = Instantiate(characters.group[characterIndex].characterModel, characterModel.transform.localPosition, characterModel.transform.localRotation);
        temp.transform.localScale = characters.group[characterIndex].modelScale;

        strength.texture = characters.group[characterIndex].strength;
        speed.texture = characters.group[characterIndex].speed;
        weight.texture = characters.group[characterIndex].weight;
        balance.texture = characters.group[characterIndex].balance;
    }

    private void FixedUpdate()
    {
        if (!connected)
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
                input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);
            }
        }
    }

    public void Unbind()
    {
        input.Disable();
    }

    private void SwitchTeamHandler(InputAction.CallbackContext context)
    {
        index += (int)context.ReadValue<float>();

        if (index < 0)
            index = teams.group.Length - 1;
        else if (index >= teams.group.Length)
            index = 0;

        //background.color = teams.group[index].color;
        background.texture = teams.group[index].teamBackground;
        label.text = teams.group[index].name.ToString() + " TEAM";

        if (OnModeChange != null) OnModeChange.Invoke();

        versusText.ChangeText();
    }

    private void SwitchCharacterHandler(InputAction.CallbackContext context)
    {
        characterIndex += (int)context.ReadValue<float>();

        if (characterIndex < 0)
            characterIndex = characters.group.Length - 1;
        else if (characterIndex >= teams.group.Length)
            characterIndex = 0;

        Destroy(temp);
        temp = Instantiate(characters.group[characterIndex].characterModel, characterModel.transform.localPosition, characterModel.transform.localRotation);
        temp.transform.localScale = characters.group[characterIndex].modelScale;
        //originalModelScale = temp.transform.localScale;

        BounceAnimation bounce = temp.AddComponent<BounceAnimation>();
        bounce.duration = 0.5f;
        bounce.scale = new Vector3(1.1f, 1.1f, 1.1f);
        bounce.Start();
        bounce.Animate();

        //if (OnModeChange != null) OnModeChange.Invoke();

        strength.texture = characters.group[characterIndex].strength;
        speed.texture = characters.group[characterIndex].speed;
        weight.texture = characters.group[characterIndex].weight;
        balance.texture = characters.group[characterIndex].balance;
    }
}
