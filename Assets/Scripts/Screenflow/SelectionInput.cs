using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SelectionInput : MonoBehaviour
{
    // input
    private InputMaster input;
    [SerializeField]
    private int controllerID = 0;

    [Header("Scroll material")]
    [SerializeField]
    private Renderer scroll;

    [Header("Player input")]
    public int teamIndex;
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

    [Header("Sound")]
    public AudioPlayer player;
    [SerializeField]
    private AudioClip switchAudio;
    [SerializeField]
    private AudioClip highAudio;
    [SerializeField]
    private AudioClip lowAudio;
    [SerializeField]
    private AudioClip normalAudio;
    private bool played;

    // references
    //private Image background;
    private RawImage background;
    private Text label;
    public bool connected { get; private set; }

    private MaterialManager selected;

    private GameObject character;

    // Hold
    static private float progress;
    public bool isHolding { get; private set; }

    // animation
    [SerializeField]
    private UnityEvent OnModeChange;

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
            input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
            input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);
            // hold to start
            input.Lobby.StartGame.performed += context => HoldHandler(context);
            input.Lobby.StartGame.canceled += context => HoldHandler(context);
        }
        else
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Lobby.SwitchTeam.performed += context => SwitchTeamHandler(context);
                input.Lobby.SwitchCharacter.performed += context => SwitchCharacterHandler(context);            
                // hold to start
                input.Lobby.StartGame.performed += context => HoldHandler(context);
                input.Lobby.StartGame.canceled += context => HoldHandler(context);
            }
            else
            {
                connected = false;
            }
        }

        background = GetComponent<RawImage>();
        label = GetComponentInChildren<Text>();

        isHolding = false;
        played = false;
    }

    private void Start()
    {
        input.Enable();
        background.texture = teams.group[teamIndex].teamBackground;

        UpdateCharacter();
        progress = 0.0f;
    }

    private void Update()
    {
        if (isHolding)
        {
            progress += Time.deltaTime * 0.5f;
            scroll.material.SetFloat("_Progress", progress);
            
            // play audio
            if(!played)
            {
                //if (characterIndex == 0)
                //    player.PlaySFX(highAudio);
                //else if (characterIndex == 1)
                //    player.PlaySFX(normalAudio);
                //else
                player.PlaySFX(lowAudio);

                played = true;
                
                character.GetComponent<BounceAnimation>().Start();
                character.GetComponent<BounceAnimation>().Animate();
            }
        }
        else
        {
            played = false;
        }

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
        teamIndex += (int)context.ReadValue<float>();

        if (teamIndex < 0)
            teamIndex = teams.group.Length - 1;
        else if (teamIndex >= teams.group.Length)
            teamIndex = 0;

        //background.color = teams.group[index].color;
        background.texture = teams.group[teamIndex].teamBackground;
        label.text = teams.group[teamIndex].name.ToString() + " TEAM";

        MaterialPack pack = characters.group[characterIndex].character.teamMaterials[teamIndex];
        selected.SetMaterialPack(pack);

        if (OnModeChange != null) OnModeChange.Invoke();

        // Play sound
        player.PlaySFX(switchAudio);
    }

    private void SwitchCharacterHandler(InputAction.CallbackContext context)
    {
        characterIndex += (int)context.ReadValue<float>();

        if (characterIndex < 0)
            characterIndex = characters.group.Length - 1;
        else if (characterIndex >= teams.group.Length)
            characterIndex = 0;

        Destroy(selected.gameObject);
        UpdateCharacter();

        // Play sound
        player.PlaySFX(switchAudio);
    }

    private void HoldHandler(InputAction.CallbackContext context)
    {
        isHolding = context.ReadValue<float>() == 1;
        
        if(context.ReadValue<float>() == 0)
        {
            character.GetComponent<BounceAnimation>().Start();
            character.GetComponent<BounceAnimation>().Animate();
        }
    }

    private void UpdateCharacter()
    {
        GameObject go = Instantiate(characters.group[characterIndex].model, characterModel.transform.localPosition, characterModel.transform.localRotation);
        character = go;

        selected = go.GetComponent<MaterialManager>();
        selected.transform.localScale = characters.group[characterIndex].modelScale;

        // set team texture
        MaterialPack pack = characters.group[characterIndex].character.teamMaterials[teamIndex];
        selected.GetComponent<MaterialManager>().SetMaterialPack(pack);

        // bounce model
        BounceAnimation bounce = go.AddComponent<BounceAnimation>();
        bounce.duration = 0.5f;
        bounce.scale = new Vector3(1.1f, 1.1f, 1.1f);
        bounce.Start();
        bounce.Animate();

        // set stats 
        strength.texture = characters.group[characterIndex].strength;
        speed.texture = characters.group[characterIndex].speed;
        weight.texture = characters.group[characterIndex].weight;
        balance.texture = characters.group[characterIndex].balance;
    }

}
