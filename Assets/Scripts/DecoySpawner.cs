using UnityEngine;

public class DecoySpawner : MonoBehaviour {
    [SerializeField] private Decoy decoyPrefab;
    [SerializeField] private Vector3 sphereCenter = new Vector3(0, 0, 0.3f);
    [SerializeField] private float sphereRadius = 0.2f;

    private void Awake() {
        var position = sphereCenter + sphereRadius * Random.insideUnitSphere;
        Instantiate(decoyPrefab, position, Quaternion.identity);
    }
}