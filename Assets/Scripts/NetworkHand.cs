using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class NetworkHand : HandBehavior {
    [SerializeField] private Decoy decoy;
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
        if (networkObject.IsOwner) {
            networkObject.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            decoy.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (!networkObject.NetworkReady)
            return;

        decoy.Color = networkObject.color;

        if (!networkObject.IsOwner) {
            transform.position = networkObject.position;
            decoy.gameObject.SetActive(networkObject.active);
            return;
        }

        var index = handPositionManager.Index;

        networkObject.active = index != null;

        if (!index) {
            screenColorManager.Color = errorColor;
            return;
        }

        transform.position = index.position;
        networkObject.position = index.position;

        var maxColorValue = FindObjectsOfType<Decoy>()
            .Select(d => d.GetLerpValue(index.position))
            .Max();

        screenColorManager.Color = Color.Lerp(offColor, onColor, maxColorValue);
    }
}