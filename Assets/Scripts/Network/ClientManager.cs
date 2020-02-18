using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> references = new List<GameObject>();

    private Client client;
    private List<NetworkObject> objects;

    private void Awake()
    {
        client = Client.Instance;
        client.handlers[(byte)Packets_ID.IG_SPAWN] = SpawnHandler;
        client.handlers[(byte)Packets_ID.IG_TRANSFORM] = TransformHandler;

        objects = new List<NetworkObject>();
    }

    private void FixedUpdate()
    {
        client.FixedUpdate();
    }

    #region Receieve handlers

    private void SpawnHandler()
    {
        string raw = client.m_NetworkReader.ReadString();
        SpawnPacket packet = Serializer.ToObject<SpawnPacket>(raw);

        Transform instance = Instantiate(references[packet.type]).transform;
        instance.position = packet.position.GetVector3();
        instance.rotation = packet.rotation.GetQuaternion();

        NetworkObject network = instance.GetComponent<NetworkObject>();
        network.id = packet.id;

        objects.Add(network);
    }
    
    private void TransformHandler()
    {
        string raw = client.m_NetworkReader.ReadString();
        TransformPacket packet = Serializer.ToObject<TransformPacket>(raw);

        Interpolator interpolator = objects[packet.id].GetComponent<Interpolator>();
        interpolator.t = packet.t;
        interpolator.targetPosition = packet.position.GetVector3();
        interpolator.targetRotation = packet.rotation.GetQuaternion();
    }

    #endregion
}
