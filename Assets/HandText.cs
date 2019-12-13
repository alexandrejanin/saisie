using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandText : MonoBehaviour {
    [SerializeField] private Transform leftHand, rightHand;
    private Text text;

    void Awake() {
        text = GetComponent<Text>();
    }

    void Update(){
     text.text = "";
     text.text += $"Position main gauche: {leftHand.position}\n";
     text.text += $"Position main droite: {rightHand.position}\n";
     text.text += $"Rotation main gauche: {leftHand.rotation}\n";
     text.text += $"Rotation main droite: {rightHand.rotation}\n";   
    }
}
