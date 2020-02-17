using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementInput : MonoBehaviour
{
    private uint id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";

    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;
    public bool moveable = true;
    public bool pickup = false;

    public uint ID
    {
        set { id = value; }
        get { return id; }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(horizontalAxisName);
        vertical = Input.GetAxis(verticalAxisName);
        pickup = Input.GetButtonDown(pickUpName);
    }

    private void FixedUpdate()
    {
        //Change this to phase locking call
        GetComponent<CharacterMovement>().Move(horizontal, vertical);

        if (pickup)
            GetComponent<CharacterMovement>().PickUpNearbyFruit();
    }
}