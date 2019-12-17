using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class NetworkHand : HandBehavior {
    [SerializeField] private GameObject graphics;
    [SerializeField] private Color errorColor, offColor, onColor;
    [SerializeField] private float minDistance = 0.05f;
    [SerializeField] private float maxDistance = 0.1f;

    private HandPositionManager handPositionManager;
    private ScreenColorManager screenColorManager;

    private void Awake() {
        handPositionManager = FindObjectOfType<HandPositionManager>();
        screenColorManager = FindObjectOfType<ScreenColorManager>();
    }

    protected override void NetworkStart() {
        base.NetworkStart();
        networkObject.UpdateInterval = 20;
        graphics.SetActive(!networkObject.IsOwner);
    }

    private void Update() {
        if (!networkObject.NetworkReady)
            return;

        if (!networkObject.IsOwner) {
            transform.position = networkObject.position;
            return;
        }

        var index = handPositionManager.Index;

        if (!index) {
            screenColorManager.Color = errorColor;
            return;
        }

        transform.position = index.position;
        networkObject.position = index.position;

        var shortestDistance = Mathf.Infinity;

        foreach (var decoy in FindObjectsOfType<StaticDecoy>()) {
            var dist = Vector3.Distance(index.position, decoy.transform.position);
            if (dist < shortestDistance)
                shortestDistance = dist;
        }

        foreach (var decoy in FindObjectsOfType<Decoy>()) {
            var dist = Vector3.Distance(index.position, decoy.transform.position);
            if (dist < shortestDistance)
                shortestDistance = dist;
        }

        foreach (var hand in FindObjectsOfType<NetworkHand>()) {
            if (hand == this)
                continue;
            var dist = Vector3.Distance(index.position, hand.transform.position);
            if (dist < shortestDistance)
                shortestDistance = dist;
        }

        var t = Mathf.InverseLerp(minDistance, maxDistance, shortestDistance);
        screenColorManager.Color = Color.Lerp(onColor, offColor, t);
    }
}