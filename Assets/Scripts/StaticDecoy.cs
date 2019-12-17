using UnityEngine;
using Random = UnityEngine.Random;

public class StaticDecoy : MonoBehaviour {
    [SerializeField] private Vector3 sphereCenter = new Vector3(0, 0, 0.3f);

    [SerializeField] private float sphereRadius = 0.25f;

    private void Awake() {
        transform.position = sphereCenter + sphereRadius * Random.insideUnitSphere;
    }
}