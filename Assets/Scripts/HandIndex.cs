using UnityEngine;

public class HandIndex : MonoBehaviour
{
    private HandPositionManager handPositionManager;
    void Start()
    {
        handPositionManager = FindObjectOfType<HandPositionManager>();
    }

    void Update()
    {
        if (handPositionManager.Index)
            transform.position = handPositionManager.Index.position;
    }
}
