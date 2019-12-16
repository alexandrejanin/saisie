using UnityEngine;
using UnityEngine.UI;

public class HandObjectDistance : MonoBehaviour {
    [SerializeField] private Transform item;
    [SerializeField] private float minDistance, maxDistance;
    [SerializeField] private Vector3 sphereCenter = new Vector3(0, 0, 0.3f);
    [SerializeField] private float sphereRadius = 0.25f;
    [SerializeField] private Image image;
    [SerializeField] private bool fade;

    private HandPositionManager handPositionManager;

    private void Awake() {
        handPositionManager = GetComponent<HandPositionManager>();
        ShuffleObject();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            ShuffleObject();

        var finger = handPositionManager.Index;

        if (!finger) {
            image.color = Color.black;
            return;
        }

        var distance = (finger.position - item.position).magnitude;

        Color color;

        if (fade) {
            var t = Mathf.InverseLerp(minDistance, maxDistance, distance);
            color = Color.Lerp(Color.white, Color.black, t);
        } else {
            color = distance < minDistance
                ? Color.white
                : Color.black;
        }

        image.color = color;
    }

    private void ShuffleObject() {
        item.position = sphereCenter + sphereRadius * Random.insideUnitSphere;
        Debug.Log($"Obstacle at {item.position}");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(item.position, minDistance);
    }
}