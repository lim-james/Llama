﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Back : MonoBehaviour
{
    private InputMaster input;
    [SerializeField]
    private int controllerID = 0;

    [Header("Go Back To Scene")]
    public string SceneName;

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
            input.Home.Back.performed += context => GoToScene(context);
        }
        else
        {
            if (Gamepad.all.Count >= controllerID)
            {
                connected = true;
                input.devices = new[] { Gamepad.all[controllerID - 1] };
                input.Home.Back.performed += context => GoToScene(context);
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

    private void GoToScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneName);
    }
}