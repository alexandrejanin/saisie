using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using Leap.Unity;
using UnityEngine;

public class NetworkHand : HandBehavior
{
    [SerializeField] private Transform index;

    private HandPositionManager handPositionManager;

    private void Awake()
    {
        handPositionManager = FindObjectOfType<HandPositionManager>();
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();
        networkObject.UpdateInterval = 20;
        GetComponentInChildren<HandIndex>().enabled = networkObject.IsOwner;

        foreach (var hmm in FindObjectsOfType<HandModelManager>())
        {
            hmm.gameObject.SetActive(NetworkManager.Instance.IsServer);
        }
        foreach (var lsp in FindObjectsOfType<LeapServiceProvider>())
        {
            lsp.gameObject.SetActive(NetworkManager.Instance.IsServer);
        }
    }

    private void Update()
    {
        if (!networkObject.NetworkReady)
            return;

        if (networkObject.IsOwner)
        {
            networkObject.position = index.position;
        }
        else
        {
            index.position = networkObject.position;
        }
    }
}