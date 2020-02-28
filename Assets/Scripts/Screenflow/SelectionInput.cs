﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool animateLabel;
    private bool labelLarge;
    private bool animateModel;
    private bool modelLarge;
    private Vector3 originalLabelScale;
    private Vector3 originalModelScale;

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

        //background = GetComponent<Image>();
        background = GetComponent<RawImage>();
        label = GetComponentInChildren<Text>();
        //characterModel = GetComponent<GameObject>();
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

        animateLabel = false;
        labelLarge = false;
        animateModel = false;
        modelLarge = false;
        originalLabelScale = background.GetComponent<RawImage>().transform.localScale;
        originalModelScale = temp.transform.localScale;
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

        animateLabel = true;

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
        originalModelScale = temp.transform.localScale;

        animateModel = true;

        strength.texture = characters.group[characterIndex].strength;
        speed.texture = characters.group[characterIndex].speed;
        weight.texture = characters.group[characterIndex].weight;
        balance.texture = characters.group[characterIndex].balance;
    }

    private void Update()
    {
        if(animateLabel)
        {
            if (!labelLarge && background.GetComponent<RawImage>().transform.localScale.x < originalLabelScale.x * 1.3)
            {
                background.GetComponent<RawImage>().transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, 0);
                label.transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, 0);
            }
            else if (!labelLarge && background.GetComponent<RawImage>().transform.localScale.x > originalLabelScale.x * 1.3)
                labelLarge = true;

            if (labelLarge && background.GetComponent<RawImage>().transform.localScale.x > originalLabelScale.x)
            {
                background.GetComponent<RawImage>().transform.localScale -= new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, 0);
                label.transform.localScale -= new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, 0);
            }
            else if(labelLarge && background.GetComponent<RawImage>().transform.localScale.x < originalLabelScale.x)
            {
                labelLarge = false;
                animateLabel = false;
            }
        }
        
        if(animateModel)
        {
            if (!modelLarge && temp.transform.localScale.x < originalModelScale.x * 1.3)
                temp.transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
            else if (!modelLarge && temp.transform.localScale.x > originalModelScale.x * 1.3)
                modelLarge = true;
        
            if (modelLarge && temp.transform.localScale.x > originalModelScale.x)
                temp.transform.localScale -= new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
            else if(modelLarge && temp.transform.localScale.x < originalModelScale.x)
            {
                modelLarge = false;
                animateModel = false;
            }
        }
    }
}
