using UnityEngine;

public class HandPositionManager : MonoBehaviour {
    [SerializeField] private Transform leftPalm, leftIndex, rightPalm, rightIndex;

    public Transform Palm => LeftPalm ?? RightPalm ?? null;
    public Transform Index => LeftIndex ?? RightIndex ?? null;

    public Transform LeftPalm => IfActive(leftPalm);
    public Transform LeftIndex => IfActive(leftIndex);
    public Transform RightPalm => IfActive(rightPalm);
    public Transform RightIndex => IfActive(rightIndex);

    private static Transform IfActive(Transform t) {
        return t.gameObject.activeInHierarchy ? t : null;
    }
}