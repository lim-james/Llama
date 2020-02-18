using UnityEngine;

[RequireComponent(typeof(Interpolator))]
public class ClientInput : MonoBehaviour
{
    private int id;
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string pickUpName = "Jump";

    [SerializeField]
    private float horizontal = 0.0f;
    [SerializeField]
    private float vertical = 0.0f;
    public bool moveable = true;
    public bool pickup = false;

    public int sendRate = 60;
    private float sendFrequency;
    private float sendTimer = 0.0f;

    private void Start()
    {
        id = GetComponent<NetworkObject>().id;
        sendFrequency = 1.0f / sendRate;
    }

    private void Update()
    {
        horizontal = Input.GetAxis(horizontalAxisName);
        vertical = Input.GetAxis(verticalAxisName);
        pickup = Input.GetButtonDown(pickUpName);

        sendTimer += Time.deltaTime;

        if (pickup)
        {
            /*
            Client.Instance.Send(Packets_ID.PICK_UP, new MovementPacket(
                id, horizontal, vertical
            ));
            */
            GetComponent<CharacterMovement>().PickUpNearbyFruit();
        }
    }

    private void FixedUpdate()
    {
        //Change this to phase locking call
        if (sendTimer >= sendFrequency)
        {
            GetComponent<CharacterMovement>().Move(horizontal, vertical);
            /*
            Client.Instance.Send(Packets_ID.IG_MOVEMENT, new MovementPacket(
                id, horizontal, vertical
            ));
            */

            sendTimer = 0.0f;
        }
    }
}