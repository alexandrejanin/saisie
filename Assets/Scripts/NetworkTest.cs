using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkTest : MonoBehaviour {
    [SerializeField] private NetworkManager networkManagerPrefab;
    [SerializeField] private ushort port = 15937;

    private NetworkManager networkManager;

    public void Host() {
        var server = new UDPServer(2);
        server.Connect("localhost", port);

        networkManager = Instantiate(networkManagerPrefab);
        networkManager.Initialize(server);
        SceneManager.LoadScene(1);
    }

    public void Connect() {
        NetWorker.localServerLocated += LocalServerLocated;
        NetWorker.RefreshLocalUdpListings();
    }

    private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender) {
        Debug.Log("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
        var client = new UDPClient();
        client.serverAccepted += server => MainThreadManager.Run(() => {
            networkManager = Instantiate(networkManagerPrefab);
            networkManager.Initialize(client);
        });
        client.Connect(endpoint.Address, endpoint.Port);
    }
}