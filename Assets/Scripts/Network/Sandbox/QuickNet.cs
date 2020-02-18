using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickNet : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private List<GameObject> serverReferences = new List<GameObject>();
    [SerializeField]
    private List<GameObject> clientReferences = new List<GameObject>();

    private Client client;
    private Server server;

    private List<NetworkObject> serverObjects;
    private List<NetworkObject> clientObjects;

    [Header("Attributes")]
    [SerializeField]
    private bool autoconnect = true;
    [SerializeField]
    private int port = 8080;
    [SerializeField]
    private float packetDelay = 0.5f;
    private float packetTime;

    private void Awake()
    {
        client = Client.Instance;
        server = Server.Instance;

        serverObjects = new List<NetworkObject>();
        clientObjects = new List<NetworkObject>();

        client.handlers[(byte)Packets_ID.IG_SPAWN] = SpawnHandler;
        client.handlers[(byte)Packets_ID.IG_TRANSFORM] = TransformHandler;
    }

    private void Start()
    {
        if (autoconnect && server.StartServer(port, 2))
            client.Connect("localhost", port);
    }

    private void OnDestroy()
    {
        server.StopServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject(0, new Vector3(0, 1, 0), Vector3.zero);
        }
    }

    private void FixedUpdate()
    {
        client.FixedUpdate();
        server.FixedUpdate();

        packetTime += Time.fixedDeltaTime;
        if (packetTime > packetDelay)
        {
            foreach (NetworkObject network in serverObjects)
            {
                server.SendToAll(Packets_ID.IG_TRANSFORM,
                    new TransformPacket(network.id, packetTime, network.transform.position, network.transform.rotation)
                );
            }

            packetTime = 0.0f;
        }
    }

    #region Helper methods

    // encapsulate 

    public void SpawnObject(int type, Vector3 position, Vector3 rotation)
    {
        Transform instance = Instantiate(serverReferences[type]).transform;
        instance.position = position;
        instance.localEulerAngles = rotation;

        NetworkObject network = instance.GetComponent<NetworkObject>();
        network.id = serverObjects.Count;
        serverObjects.Add(network);

        server.SendToAll(Packets_ID.IG_SPAWN,
            new SpawnPacket(network.id, type, instance.position, instance.rotation)
        );
    }

    private void SpawnHandler()
    {
        string raw = client.m_NetworkReader.ReadString();
        SpawnPacket packet = Serializer.ToObject<SpawnPacket>(raw);

        Transform instance = Instantiate(clientReferences[packet.type]).transform;
        instance.position = packet.position.GetVector3();
        instance.rotation = packet.rotation.GetQuaternion();

        NetworkObject network = instance.GetComponent<NetworkObject>();
        network.id = packet.id;

        clientObjects.Add(network);
    }
    
    private void TransformHandler()
    {
        string raw = client.m_NetworkReader.ReadString();
        TransformPacket packet = Serializer.ToObject<TransformPacket>(raw);

        Interpolator interpolator = clientObjects[packet.id].GetComponent<Interpolator>();
        interpolator.t = packet.t;
        interpolator.targetPosition = packet.position.GetVector3();
        interpolator.targetRotation = packet.rotation.GetQuaternion();
    }


    #endregion

}
