using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class HandSpawner : MonoBehaviour {
    private void Awake() {
        if (NetworkManager.Instance && !NetworkManager.Instance.IsServer)
            NetworkManager.Instance.InstantiateHand();
    }
}