using UnityEngine;
using UnityEngine.UI;

public class HandText : MonoBehaviour {
    private HandPositionManager handPositionManager;
    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
        handPositionManager = FindObjectOfType<HandPositionManager>();
    }

    private void Update() {
        text.text = "";

        if (handPositionManager.LeftPalm)
            text.text += $"Position main gauche: {handPositionManager.LeftPalm.position}\n";

        if (handPositionManager.LeftIndex)
            text.text += $"Position index gauche: {handPositionManager.LeftIndex.position}\n";

        if (handPositionManager.RightPalm)
            text.text += $"Position main droite: {handPositionManager.RightPalm.position}\n";

        if (handPositionManager.RightIndex)
            text.text += $"Position index droit: {handPositionManager.RightIndex.position}\n";
    }
}