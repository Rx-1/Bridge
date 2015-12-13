using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    public float lifespawn = 10;
    [HideInInspector]
    public Vector3 projectileVelocity;
    [HideInInspector]
    public Vector3 projectileGravity;

    Vector3 gravityXx;

    Rigidbody RB;

	// Use this for initialization
	void Start () {
        RB = gameObject.GetComponent<Rigidbody>();
        RB.velocity = projectileVelocity;
        gravityXx = projectileGravity * RB.mass;
        Destroy(gameObject, lifespawn);
	}

    void FixedUpdate() {
        RB.AddForce(gravityXx);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
