﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HomeSelection : MonoBehaviour
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
    private Button Lobbies;
    [SerializeField]
    private Button Controls;
    [SerializeField]
    private Button Credits;
    [SerializeField]
    private Button Exit;

    public bool connected { get; private set; }

    private void Awake()
    {
        input = new InputMaster();
        input.Enable();

        // bind handlers
        if (controllerID == 0)
        {
            connected = true;
            input.devices = new[] { InputDevice.all[0] };
            input.Home.ChangeSelection.performed += context => SwitchSelectedButton(context);
            input.Home.Select.performed += context => Enter(context);
        }
        else
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Home.ChangeSelection.performed += context => SwitchSelectedButton(context);
                input.Home.Select.performed += context => Enter(context);
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwitchSelectedButton(InputAction.CallbackContext context)
    {
        index += (int)context.ReadValue<float>();

        if (index < 0)
            index = 0;
        else if (index > 3)
            index = 3;

        switch(index)
        {
            case 0:
                Lobbies.GetComponent<RawImage>().texture = selectedButton;
                Controls.GetComponent<RawImage>().texture = normalButton;
                Credits.GetComponent<RawImage>().texture = normalButton;
                Exit.GetComponent<RawImage>().texture = normalButton;
                break;
            case 1:
                Lobbies.GetComponent<RawImage>().texture = normalButton;
                Controls.GetComponent<RawImage>().texture = selectedButton;
                Credits.GetComponent<RawImage>().texture = normalButton;
                Exit.GetComponent<RawImage>().texture = normalButton;
                break;
            case 2:
                Lobbies.GetComponent<RawImage>().texture = normalButton;
                Controls.GetComponent<RawImage>().texture = normalButton;
                Credits.GetComponent<RawImage>().texture = selectedButton;
                Exit.GetComponent<RawImage>().texture = normalButton;
                break;
            case 3:
                Lobbies.GetComponent<RawImage>().texture = normalButton;
                Controls.GetComponent<RawImage>().texture = normalButton;
                Credits.GetComponent<RawImage>().texture = normalButton;
                Exit.GetComponent<RawImage>().texture = selectedButton;
                break;

        }
    }

    private void Enter(InputAction.CallbackContext context)
    {
        if (Lobbies.GetComponent<RawImage>().texture == selectedButton)
            SceneManager.LoadScene("Lobby");
        else if (Controls.GetComponent<RawImage>().texture == selectedButton)
            SceneManager.LoadScene("Controls");
        else if (Credits.GetComponent<RawImage>().texture == selectedButton)
            SceneManager.LoadScene("Credits");

    }
}