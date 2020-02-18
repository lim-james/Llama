using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> references = new List<GameObject>();

    private Server server;
    private List<NetworkObject> objects;

    // temp
    [Header("Attributes")]
    [SerializeField]
    private bool autoconnect = true;
    [SerializeField]
    private int port = 8080;
    [SerializeField]
    private uint tickrate = 30;
    private float updateDelay;
    private float packetTime;

    private void Awake()
    {
        server = Server.Instance;
        objects = new List<NetworkObject>();
    }

    private void Start()
    {
        // to be temp 
        updateDelay = 1.0f / (float)tickrate;
        packetTime = 0.0f;

        if (autoconnect && server.StartServer(port, 2))
            Client.Instance.Connect("localhost", port);
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

       packetTime += Time.deltaTime;
        if (packetTime > updateDelay)
        {
            foreach (NetworkObject network in objects)
            {
                server.SendToAll(Packets_ID.IG_TRANSFORM,
                    new TransformPacket(network.id, packetTime, network.transform.position, network.transform.rotation)
                );
            }

            packetTime = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        server.FixedUpdate();
    }

    #region Helper methods

    public void SpawnObject(int type, Vector3 position, Vector3 rotation)
    {
        Transform instance = Instantiate(references[type]).transform;
        instance.position = position;
        instance.localEulerAngles = rotation;

        NetworkObject network = instance.GetComponent<NetworkObject>();
        network.id = objects.Count;
        objects.Add(network);

        server.SendToAll(Packets_ID.IG_SPAWN,
            new SpawnPacket(network.id, type, instance.position, instance.rotation)
        );
    }

    #endregion
}
