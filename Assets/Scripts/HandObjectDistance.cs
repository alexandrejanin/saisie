using UnityEngine;
using UnityEngine.UI;

public class HandObjectDistance : MonoBehaviour {
    [SerializeField] private Color errorColor, offColor, onColor;
    [SerializeField] private bool fade;
    [SerializeField] private Image image;
    [SerializeField] private Transform item;
    [SerializeField] private float minDistance, maxDistance;
    [SerializeField] private Vector3 sphereCenter = new Vector3(0, 0, 0.3f);
    [SerializeField] private float sphereRadius = 0.25f;

    private HandPositionManager handPositionManager;

    private void Awake() {
        handPositionManager = GetComponent<HandPositionManager>();
        ShuffleObject();
    }

    private void Update() {
        item.localScale = Vector3.one * maxDistance;

        if (Input.GetKeyDown(KeyCode.Space))
            ShuffleObject();

        if (Input.GetKeyDown(KeyCode.D)) image.enabled = !image.enabled;

        var finger = handPositionManager.Index;

        if (finger) {
            var distance = (finger.position - item.position).magnitude;

            if (fade) {
                var t = Mathf.InverseLerp(minDistance, maxDistance, distance);
                image.color = Color.Lerp(onColor, offColor, t);
            }
            else {
                image.color = distance < minDistance
                    ? onColor
                    : offColor;
            }
        }
        else {
            image.color = errorColor;
        }

        item.GetComponent<MeshRenderer>().material.color = image.color;
    }

    private void ShuffleObject() {
        item.position = sphereCenter + sphereRadius * Random.insideUnitSphere;
        Debug.Log($"Obstacle at {item.position}");
    }
}