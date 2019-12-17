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
        if (Input.GetKeyDown(KeyCode.D))
            fullscreenImage.enabled = !fullscreenImage.enabled;
    }
}