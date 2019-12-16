using UnityEngine;
using UnityEngine.UI;

public class HandDistance : MonoBehaviour {
    [SerializeField] private float maxDistance, minDistance;

    [SerializeField] private Image image;

    private HandPositionManager handPositionManager;

    private void Awake() {
        handPositionManager = GetComponent<HandPositionManager>();
    }

    private void Update() {
        var leftHand = handPositionManager.LeftPalm;
        var rightHand = handPositionManager.RightPalm;

        if (!leftHand || !rightHand) {
            image.color = Color.black;
            return;
        }

        var distance = (leftHand.position - rightHand.position).magnitude;

        var t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        var color = Color.Lerp(Color.white, Color.black, t);

        image.color = color;
    }
}