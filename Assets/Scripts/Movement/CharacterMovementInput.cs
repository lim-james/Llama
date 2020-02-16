using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public float horizontal = 0.0f;
    public float vertical = 0.0f;
    public int sendRate = 60;
    private float sendFrequency = 0.0f;
    private float sendTimer = 0.0f;
    public bool moveable = true;
    private bool playerInput = false;

    // Start is called before the first frame update
    void Start()
    {
        sendFrequency = 1.0f / sendRate;
    }

    // Update is called once per frame
    void Update()
    {
        sendTimer += Time.unscaledDeltaTime;
        horizontal = Input.GetAxis(horizontalAxisName);
        vertical = Input.GetAxis(verticalAxisName);
        playerInput = (Input.GetButton(horizontalAxisName) || Input.GetButton(verticalAxisName));

        if (sendTimer >= sendFrequency)
        {
            GetComponent<CharacterMovement>().Move(horizontal, vertical, playerInput);
            sendTimer = 0.0f;
        }
    }
}
