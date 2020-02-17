using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickNet : MonoBehaviour
{

    [SerializeField]
    private GameObject serverCube;
    [SerializeField]
    private GameObject clientCube;

    private void Awake()
    {
        Client.Instance.handlers[(byte)Packets_ID.IG_SPAWN] = SpawnHandler;
    }

    private void Start()
    {
        int port = 12;
        if (Server.Instance.StartServer(port, 2))
            Client.Instance.Connect("localhost", port);
    }

    private void OnDestroy()
    {
        Server.Instance.StopServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform instance = Instantiate(serverCube).transform;
            instance.position = new Vector3(0, 0, 0);

            Server.Instance.SendToAll(Packets_ID.IG_SPAWN,
                new SpawnPacket(instance.position, instance.rotation)
            );

            //Server.Instance.SendToAll(Packets_ID.IG_SPAWN, "Hello world");
        }
    }

    private void FixedUpdate()
    {
        Client.Instance.FixedUpdate();
        Server.Instance.FixedUpdate();
    }

    #region Helper methods

    // encapsulate 

    private void SpawnHandler()
    {
        Debug.Log("[Client] Received spawn packet ");
        SpawnPacket packet = Client.Instance.m_NetworkReader.Read<SpawnPacket>();
        Transform instance = Instantiate(clientCube).transform;
        instance.position = packet.position.GetVector3();
        instance.rotation = packet.rotation.GetQuaternion();
    }

    #endregion

}
