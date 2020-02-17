using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";

    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;

    public int sendRate = 60;
    private float sendFrequency = 0.0f;
    private float sendTimer = 0.0f;
    public bool moveable = true;

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

        if (sendTimer >= sendFrequency)
        {
            //Change this to phase locking call
            GetComponent<CharacterMovement>().Move(horizontal, vertical);
            sendTimer = 0.0f;
        }
    }
}