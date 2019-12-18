using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ScreenColorManager : MonoBehaviour {
    [SerializeField] private Image fullscreenImage, smallImage;

    public Color Color {
        set {
            fullscreenImage.color = value;
            smallImage.color = value;
        }
    }

    private void Update() {
        if (NetworkManager.Instance.IsServer) {
            fullscreenImage.enabled = false;
            smallImage.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
            fullscreenImage.enabled = !fullscreenImage.enabled;
    }
}