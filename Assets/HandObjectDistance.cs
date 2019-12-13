using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity;

public class HandObjectDistance : MonoBehaviour {
[SerializeField] private Transform leftHand, rightHand, item;

[SerializeField] private float minDistance, maxDistance;

[SerializeField] private Vector3 minPosition, maxPosition;

[SerializeField] private Image image;

    void ShuffleObject(){
        item.position = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );
        Debug.Log($"Obstacle at {item.position}");
    }

    void Start() {
        ShuffleObject();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            ShuffleObject();
        }

        var hand = leftHand;

        if (!hand.gameObject.active){
            hand = rightHand;
        }

        if (!hand.gameObject.active){
            image.color = Color.black;
            return;
        }
        
        // Debug.Log(hand.position);

        var distance = (hand.position - item.position).magnitude;

        // Debug.Log(distance);

        // var t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        // Debug.Log(t);

        // var color =  Color.Lerp(Color.white, Color.black, t);

        var color = (distance < minDistance)
            ? Color.white
            : Color.black;

        image.color = color;
    }
}
