using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private static ControllerManager _instance = null;
    public static ControllerManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ControllerManager();

            return _instance;
        }
    }

    public enum Type
    {
        STANDARD, ALL_CONTROLLER, SPLIT_KEYBOARD
    }

    public Type type;

    public ControllerManager()
    {
        type = Type.ALL_CONTROLLER;
    }

}
