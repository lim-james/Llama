using System.Collections.Generic;
using UnityEngine;
using static Peer;

public delegate void ReceieveHandler();

public class Server
{
    private static Server instance = null;

    public static Server Instance
    {
        get
        {
            if (instance == null)
                instance = new Server();
            return instance;
        }
    }

    public Peer peer { get; private set; }
    public NetworkReader m_NetworkReader { get; private set; }
    public NetworkWriter m_NetworkWriter { get; private set; }

    public string info;

    public int MaxConnections { get; private set; } = -1;

    public List<Connection> connections = new List<Connection>();
    private Dictionary<ulong, Connection> connectionByGUID = new Dictionary<ulong, Connection>();

    public List<ulong> guids = new List<ulong>();

    public Dictionary<ulong, ReceieveHandler> handlers = new Dictionary<ulong, ReceieveHandler>();

    public bool StartServer(int port, int maxConnections)
    {
        if (peer == null)
        {
            peer = new Peer();
            peer = CreateServer("192.168.86.174", port, maxConnections);

            if (peer != null)
            {
                MaxConnections = maxConnections;
                Debug.Log("[Server] Server initialized on port " + port);

                Debug.Log("-------------------------------------------------");
                Debug.Log("|     Max connections: " + maxConnections);
                Debug.Log("|     Max FPS: " + (Application.targetFrameRate != -1 ? Application.targetFrameRate : 1000) + "(" + Time.deltaTime.ToString("f3") + " ms)");
                Debug.Log("|     Tickrate: " + (1 / Time.fixedDeltaTime) + "(" + Time.fixedDeltaTime.ToString("f3") + " ms)");
                Debug.Log("-------------------------------------------------");

                m_NetworkReader = new NetworkReader(peer);
                m_NetworkWriter = new NetworkWriter(peer);

                return true;
            }
            else
            {
                Debug.LogError("[Server] Starting failed...");
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public void StopServer()
    {
        if (peer != null)
        {
            peer.Close();
            peer = null;
            connections.Clear();
            connectionByGUID.Clear();

            Debug.LogError("[Server] Shutting down...");
        }
    }

    public void FixedUpdate()
    {
        if (peer != null)
        {
            while (peer.Receive())
            {
                m_NetworkReader.StartReading();
                byte b = m_NetworkReader.ReadByte();

                OnReceivedPacket(b);
            }

            string net_stat = peer != null ?
                    string.Format("in: {0} kb\t\t\t out: {1} kb\nin: {2} k/s\t\t\t out: {3} k/s",
                    ((double)peer.TOTAL_RECEIVED_BYTES / 1024).ToString("f2"),
                    ((double)peer.TOTAL_SENDED_BYTES / 1024).ToString("f2"),
                    ((double)peer.BYTES_IN / 1024).ToString("f2"),
                    ((double)peer.BYTES_OUT / 1024).ToString("f2")) : "-/-";

            info = "Server Info:\n" + net_stat + "\nLoss: " + peer.LOSS.ToString("f2") + "%" + "\n\nConnections: " + connections.Count + "/" + MaxConnections;
        }
        else
        {
            info = "No server started.";
        }
    }

    #region Send Methods

    public void SendTo(ulong target, Packets_ID id, string content)
    {
        if (m_NetworkWriter.StartWriting())
        {
            m_NetworkWriter.WritePacketID((byte)id);
            m_NetworkWriter.Write(content);
            m_NetworkWriter.Send(target, Peer.Priority.Immediate, Peer.Reliability.Reliable, 0);
        }
    }

    public void SendTo<T>(ulong target, Packets_ID id, T content)
    {
        SendTo(target, id, Serializer.ToString(content));
    }

    public void SendToOthers(ulong ignore, Packets_ID id, string content)
    {
        if (m_NetworkWriter.StartWriting())
        {
            m_NetworkWriter.WritePacketID((byte)id);
            m_NetworkWriter.Write(content);

            foreach (ulong guids in connectionByGUID.Keys)
            {
                if (guids == ignore)
                    continue;

                m_NetworkWriter.Send(guids, Priority.Immediate, Reliability.Reliable, 0);
            }
        }
    }

    public void SendToOthers<T>(ulong ignore, Packets_ID id, T content)
    {
        SendToOthers(ignore, id, Serializer.ToString(content));
    }

    public void SendToAll(Packets_ID id, string content)
    {
        if (m_NetworkWriter.StartWriting())
        {
            m_NetworkWriter.WritePacketID((byte)id);
            m_NetworkWriter.Write(content);

            foreach (ulong guids in connectionByGUID.Keys)
                m_NetworkWriter.Send(guids, Priority.Immediate, Reliability.Reliable, 0);
        }
    }

    public void SendToAll<T>(Packets_ID id, T content)
    {
        SendToAll(id, Serializer.ToString(content));
    }

    #endregion

    private void OnReceivedPacket(byte packet_id)
    {
        bool IsInternalNetworkPackets = packet_id <= 134;

        if (IsInternalNetworkPackets)
        {
            if (packet_id == (byte)RakNet_Packets_ID.NEW_INCOMING_CONNECTION)
            {
                OnConnected();//добавляем соединение
            }
            else if (packet_id == (byte)RakNet_Packets_ID.CONNECTION_LOST || packet_id == (byte)RakNet_Packets_ID.DISCONNECTION_NOTIFICATION)
            {
                Connection conn = FindConnection(peer.incomingGUID);

                if (conn != null)
                {
                    OnDisconnected(FindConnection(peer.incomingGUID));//удаляем соединение
                }
            }
        }
        else
        {
            switch (packet_id)
            {
                case (byte)Packets_ID.CL_INFO:
                    OnReceivedClientNetInfo(peer.incomingGUID);
                    break;
                default:
                    if (handlers[packet_id] != null)
                        handlers[packet_id].Invoke();
                    break;


            }
        }
    }

    #region Connections

    public Connection FindConnection(ulong guid)
    {
        if (connectionByGUID.TryGetValue(guid, out Connection value))
        {
            return value;
        }
        return null;
    }

    private void AddConnection(Connection connection)
    {
        connections.Add(connection);
        connectionByGUID.Add(connection.guid, connection);
        guids.Add(connection.guid);
    }

    private void RemoveConnection(Connection connection)
    {
        connectionByGUID.Remove(connection.guid);
        connections.Remove(connection);
        guids.Remove(connection.guid);
    }

    public static Connection[] Connections
    {
        get
        {
            return Instance.connections.ToArray();
        }
    }

    public static Connection GetByID(int id)
    {
        if (Connections.Length > 0)
        {
            return Connections[id];
        }

        return null;
    }

    public static Connection GetByIP(string ip)
    {
        foreach (Connection c in Connections)
        {
            if (c.ipaddress == ip)
            {
                return c;
            }
        }

        return null;
    }

    public static Connection GetByName(string name)
    {
        foreach (Connection c in Connections)
        {
            if (c.Info.name == name)
            {
                return c;
            }
        }

        return null;
    }

    public static Connection GetByHWID(string hwid)
    {
        foreach (Connection c in Connections)
        {
            if (c.Info.client_hwid == hwid)
            {
                return c;
            }
        }

        return null;
    }

    #endregion

    #region Events

    private void OnConnected()
    {
        Connection connection = new Connection(peer, peer.incomingGUID, connections.Count);

        //добавляем в список соединений
        AddConnection(connection);

        Debug.Log("[Server] Connection established " + connection.ipaddress);

        peer.SendPacket(connection, Packets_ID.CL_INFO, m_NetworkWriter);
    }

    private void OnDisconnected(Connection connection)
    {
        if (connection != null)
        {
            try
            {
                Debug.LogError("[Server] " + connection.Info.name + " disconnected [IP: " + connection.ipaddress + "]");

                RemoveConnection(connection);
            }
            catch
            {
                Debug.LogError("[Server] Unassgigned connection destroyed!");
            }
        }
    }

    private void OnReceivedClientNetInfo(ulong guid)
    {
        Connection connection = FindConnection(guid);

        if (connection != null)
        {
            if (connection.Info == null)
            {
                connection.Info = new ClientNetInfo();
                connection.Info.net_id = guid;
                connection.Info.name = m_NetworkReader.ReadString();
                connection.Info.local_id = m_NetworkReader.ReadPackedUInt64();
                connection.Info.client_hwid = m_NetworkReader.ReadString();
                connection.Info.client_version = m_NetworkReader.ReadString();
            }
            else
            {
                peer.SendPacket(connection, Packets_ID.CL_FAKE, Reliability.Reliable, m_NetworkWriter);
                peer.Kick(connection, 1);
            }
        }
    }

    #endregion
}