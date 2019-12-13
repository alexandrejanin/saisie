using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;

public class HandDistance : MonoBehaviour {
[SerializeField] private Transform leftHand, rightHand;

[SerializeField] private float maxDistance, minDistance;

[SerializeField] private Image image;

    void Update() {
        if (!leftHand.gameObject.active || !rightHand.gameObject.active){
            Debug.LogWarning("Il manque une main!");
            image.color = Color.black;
            return;
        }

        var distance = (leftHand.position - rightHand.position).magnitude;

        var t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        var color =  Color.Lerp(Color.white, Color.black, t);

        image.color = color;
    }
}
