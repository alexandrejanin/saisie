using UnityEngine;

public class Decoy : MonoBehaviour {
    [SerializeField] private float minDistance = 0.05f;
    [SerializeField] private float maxDistance = 0.1f;
    [SerializeField] private new Renderer renderer;

    public Color Color {
        set => renderer.material.color = value;
    }

    private void Awake() {
        transform.localScale = Vector3.one * maxDistance;
    }

    public float GetLerpValue(Vector3 position) {
        return Mathf.InverseLerp(maxDistance, minDistance, Vector3.Distance(transform.position, position));
    }
}