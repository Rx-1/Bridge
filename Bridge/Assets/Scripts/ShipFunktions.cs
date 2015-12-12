using UnityEngine;
using System.Collections;

public enum InputXx { Null, TurnLeft, TurnRight, SpeedUp, SpeedDown, Fire };

public class ShipFunktions : MonoBehaviour {

    public static ShipFunktions player;

    public float maxForvardSpeed = 20;
    public float initialSpeedPersentageFromMaxForvardSpeed = 50;
    public float anglesPerSecond = 90;

    [HideInInspector]
    public InputXx myInput = InputXx.Null;

    float speed = 0;

    Rigidbody RB;

    void Awake() {
        if (player)
            Debug.LogError("Only one ship allowed");
        player = this;
    }

	// Use this for initialization
	void Start () {
        RB = gameObject.GetComponent<Rigidbody>();
        speed = maxForvardSpeed * Mathf.Clamp(initialSpeedPersentageFromMaxForvardSpeed / 100, 0, 1);
        RB.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
	    if(myInput == InputXx.TurnLeft) {
            RB.rotation = Quaternion.Euler(RB.rotation.eulerAngles - transform.up * anglesPerSecond * Time.deltaTime);
            RB.velocity = transform.forward * speed;
        } else if(myInput == InputXx.TurnRight) {
            RB.rotation = Quaternion.Euler(RB.rotation.eulerAngles + transform.up * anglesPerSecond * Time.deltaTime);
            RB.velocity = transform.forward * speed;
        }
	}
}
