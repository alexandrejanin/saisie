using System.Linq;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class NetworkHand : HandBehavior {
    [SerializeField] private Vector3 sphereCenter = new Vector3(0, 0, 0.3f);
    [SerializeField] private float sphereRadius = 0.2f;
    [SerializeField] private Decoy decoy, staticDecoy;
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
            ShuffleStaticDecoy();
            networkObject.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            decoy.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (!networkObject.NetworkReady)
            return;

        if (networkObject.IsOwner) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ShuffleStaticDecoy();
            }

            var index = handPositionManager.Index;

            networkObject.active = index != null;

            if (index) {
                transform.position = index.position;
                networkObject.position = index.position;

                var maxColorValue = FindObjectsOfType<Decoy>()
                    .Select(d => d.GetLerpValue(index.position))
                    .Max();

                screenColorManager.Color = Color.Lerp(offColor, onColor, maxColorValue);
            } else {
                screenColorManager.Color = errorColor;
            }
        } else {
            transform.position = networkObject.position;
            decoy.gameObject.SetActive(networkObject.active);
        }

        staticDecoy.transform.position = networkObject.staticDecoyPosition;
        float h, s, v;
        Color.RGBToHSV(networkObject.color, out h, out s, out v);
        staticDecoy.Color = Color.HSVToRGB(h, s, v / 2);

        decoy.Color = networkObject.color;
    }

    private void ShuffleStaticDecoy() {
        networkObject.staticDecoyPosition = sphereCenter + sphereRadius * Random.insideUnitSphere;
    }
}