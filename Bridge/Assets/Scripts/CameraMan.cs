using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {

    public Transform overrideTarget;

    Transform target;
    Vector3 diff;

	// Use this for initialization
	void Start () {
        if (overrideTarget) {
            target = overrideTarget;
        } else if (ShipFunktions.player) {
            target = ShipFunktions.player.transform;
        } else {
            Debug.LogError("No player ship found on sceen.");
            return;
        }
        diff = transform.position - target.position;
	}

    void FixedUpdate() {
        transform.position = target.position + diff;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + diff;
    }

    //Quaternion originalRotation;

    //void Awake() {
    //    originalRotation = transform.rotation;
    //}

    //// Use this for initialization
    //void Start() {

    //}

    //void FixedUpdate() {
    //    transform.rotation = originalRotation;
    //}

    //// Update is called once per frame
    //void Update() {
    //    transform.rotation = originalRotation;
    //}
}
