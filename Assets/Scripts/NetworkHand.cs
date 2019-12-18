using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class NetworkHand : HandBehavior {
    [SerializeField] private Color errorColor, offColor, onColor;

    private HandPositionManager handPositionManager;
    private ScreenColorManager screenColorManager;

    private void Awake() {
        handPositionManager = FindObjectOfType<HandPositionManager>();
        screenColorManager = FindObjectOfType<ScreenColorManager>();
    }

    protected override void NetworkStart() {
        base.NetworkStart();
        networkObject.UpdateInterval = 20;
        if (networkObject.IsOwner)
            Destroy(transform.GetChild(0).gameObject);
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

        var maxColorValue = FindObjectsOfType<Decoy>()
            .Select(decoy => decoy.GetLerpValue(index.position))
            .Max();

        screenColorManager.Color = Color.Lerp(offColor, onColor, maxColorValue);
    }
}