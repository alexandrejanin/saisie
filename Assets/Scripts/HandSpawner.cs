using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class HandSpawner : MonoBehaviour
{
    void Start()
    {
        if (NetworkManager.Instance)
            NetworkManager.Instance.InstantiateHand();
    }

    void Update()
    {

    }
}
