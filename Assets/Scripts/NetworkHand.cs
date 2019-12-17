using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class NetworkHand : HandBehavior {
    [SerializeField] private Transform indexPrefab;

    private Transform index;
    private HandPositionManager handPositionManager;

    private void Awake() {
        handPositionManager = FindObjectOfType<HandPositionManager>();
    }

    protected override void NetworkStart() {
        base.NetworkStart();
        networkObject.UpdateInterval = 20;
    }

    private void Update() {
        if (!networkObject.NetworkReady)
            return;
        
        if (networkObject.IsOwner) {
            networkObject.position = handPositionManager.Index.position;
        } else {
            if (!index)
                index = Instantiate(indexPrefab);
            index.position = networkObject.position;
        }
    }
}