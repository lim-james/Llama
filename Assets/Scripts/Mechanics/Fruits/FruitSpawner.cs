using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ServerManager))]
public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay;
    private float spawnTime;

    private ServerManager manager;
    private Server server;

    private bool spawn = true;

    private void Awake()
    {
        manager = GetComponent<ServerManager>();
        server = Server.Instance;
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawn && spawnTime > spawnDelay)
        {
            spawn = false;
            //spawnTime = 0.0f;
            manager.SpawnObject(1, new Vector3(0, 5, 0), Vector3.zero);
            //server.SendToAll(Packets_ID.IG_SPAWN, new SpawnPacket());
        }
    }
}
