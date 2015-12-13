using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    public float lifespawn = 10;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifespawn);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
