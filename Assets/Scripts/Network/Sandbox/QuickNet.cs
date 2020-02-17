using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickNet : MonoBehaviour
{

    [SerializeField]
    private GameObject serverCube;
    [SerializeField]
    private GameObject clientCube;

    private Client client;
    private Server server;

    private List<NetworkObject> serverObjects;
    private List<NetworkObject> clientObjects;

    public float packetDelay = 0.5f;
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
        //int port = 12;
        //if (server.StartServer(port, 2))
        //    client.Connect("localhost", port);
    }

    private void OnDestroy()
    {
        server.StopServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform instance = Instantiate(serverCube).transform;
            instance.position = new Vector3(0, 0, 0);
            instance.localEulerAngles = new Vector3(Random.value * 90.0f, 0, Random.value * 90.0f);

            NetworkObject network = instance.GetComponent<NetworkObject>();
            network.id = serverObjects.Count;
            serverObjects.Add(network);

            server.SendToAll(Packets_ID.IG_SPAWN,
                new SpawnPacket(network.id, instance.position, instance.rotation)
            );
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

    private void SpawnHandler()
    {
        string raw = client.m_NetworkReader.ReadString();
        SpawnPacket packet = Serializer.ToObject<SpawnPacket>(raw);

        Transform instance = Instantiate(clientCube).transform;
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
