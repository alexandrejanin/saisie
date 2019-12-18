using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class ServerDisabler : MonoBehaviour {
    [SerializeField] private GameObject[] gameObjectsToDisable;

    private void Awake() {
        if (NetworkManager.Instance.IsServer)
            foreach (var gameObject in gameObjectsToDisable) {
                gameObject.SetActive(false);
            }
    }
}