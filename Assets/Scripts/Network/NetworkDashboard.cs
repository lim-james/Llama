using UnityEngine;
using UnityEngine.UI;

public class NetworkDashboard : MonoBehaviour
{

    /// <summary>
    ///  References
    /// </summary>

    // Guest
    [Header("Guest")]
    [SerializeField]
    private InputField ipField;
    [SerializeField]
    private InputField hostPortField;
    [SerializeField]
    private Button joinButton;
    [SerializeField]
    private Button leaveButton;

    // Host
    [Header("Host")]
    [SerializeField]
    private InputField portField;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button stopButton;

    // Messages
    [Header("Messages")]
    [SerializeField]
    private InputField inputField;

    // Results
    [Header("Results")]
    [SerializeField]
    private Text statsLabel;
    [SerializeField]
    private Text messagesLabel;

    private string log;

    /// <summary>
    /// Standard Methods
    /// </summary>

    private void Awake()
    {
        log = "";

        Client.Instance.handlers[(byte)Packets_ID.CL_MESSAGE] = ClientReceive;
        Server.Instance.handlers[(byte)Packets_ID.CL_MESSAGE] = ServerReceive;
    }

    private void FixedUpdate()
    {
        Client.Instance.FixedUpdate();
        Server.Instance.FixedUpdate();

        statsLabel.text = Server.Instance.info + "\n\n" + Client.Instance.info;
        messagesLabel.text = log == "" ? "No results" : log;
    }

    private void OnDestroy()
    {
        Client.Instance.Disconnect();
        Server.Instance.StopServer();
    }

    /// <summary>
    /// Helper Methods
    /// </summary>

    // Guest
    public void Join()
    {
        string ip = ipField.text;
        string port = hostPortField.text;
        Debug.Log("[Dashboard] Connecting to server at " + ip + ":" + port);
        Client.Instance.Connect(ip, int.Parse(port));

        joinButton.interactable = false;
        leaveButton.interactable = true;
    }

    public void Leave()
    {
        Client.Instance.Disconnect();

        joinButton.interactable = true;
        leaveButton.interactable = false;
    }

    // Host
    public void StartServer()
    {
        string port = portField.text;
        Debug.Log("[Dashboard] Creating server on " + port);
        if (Server.Instance.StartServer(int.Parse(port), 2))
        {
            startButton.interactable = false;
            stopButton.interactable = true;
        }
    }

    public void StopServer()
    {
        Server.Instance.StopServer();
        startButton.interactable = true;
        stopButton.interactable = false;
    }

    // Message
    public void Send()
    {
        string message = inputField.text;
        Client.Instance.Send(Packets_ID.CL_MESSAGE, message);
    }

    // Result
    private void ServerReceive()
    {
        Server server = Server.Instance;

        string message = server.m_NetworkReader.ReadString();
        log += "[Server] " + message + "\n";

        server.SendToAll(Packets_ID.CL_MESSAGE, message);
    }

    private void ClientReceive()
    {
        log += "[Client] " + Client.Instance.m_NetworkReader.ReadString() + "\n";
    }
}

