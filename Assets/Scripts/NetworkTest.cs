using UnityEngine;
using UnityEngine.Networking;

public class NetworkTest : MonoBehaviour {
    [SerializeField] private int port = 5678;

    private int channelId;
    private ConnectionConfig config;

    private void Start() {
        NetworkTransport.Init();

        config = new ConnectionConfig();
        channelId = config.AddChannel(QosType.Unreliable);
    }
}