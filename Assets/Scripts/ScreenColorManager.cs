using UnityEngine;
using UnityEngine.UI;

public class ScreenColorManager : MonoBehaviour {
    private Image image;

    public Color Color {
        set => image.color = value;
    }

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D))
            image.enabled = !image.enabled;
    }
}