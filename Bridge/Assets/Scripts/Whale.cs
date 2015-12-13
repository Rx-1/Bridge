using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour {

    void OnTriggerEnter(Collider c) {
        if(c.isTrigger) {
            transform.position += transform.up * 10;
            Destroy(gameObject, 2);
        } else {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
