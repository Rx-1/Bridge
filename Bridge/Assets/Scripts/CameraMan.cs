using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {

    Transform target;
    Vector3 diff;

	// Use this for initialization
	void Start () {
        target = ShipFunktions.player.transform;
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
