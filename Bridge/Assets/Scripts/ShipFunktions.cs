using UnityEngine;
using System.Collections;

public enum InputXx {Null, TurnLeft, TurnRight, SpeedUp, SpeedDown, Fire, Charge};

public class ShipFunktions : MonoBehaviour {

    public static ShipFunktions player;

    public float maxForvardSpeed = 20;
    public float acceleration = 5;
    public float initialSpeedPersentageFromMaxForvardSpeed = 50;
    public float anglesPerSecond = 90;
    public float angleAcceleration = 90;
    public float maxTilt = 20;
    public float tiltSpeed = 10;
    public bool tiltInwards = true;

    [HideInInspector]
    public InputXx myInput = InputXx.Null;

    float speed = 0;
    float tilt = 0;

    Rigidbody RB;

    Cannon cannon;

    Transform catShip;

    void OnCollisionEnter() {
        Destroy(RB);
        transform.rotation = Quaternion.Euler(-30, transform.eulerAngles.y, 25);
        this.enabled = false;
        GameManager.Crashed();
    }

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
        cannon = Cannon.cannon;
        catShip = transform.Find("catShip");
    }

    void TiltLeft() {
        if (tilt > -maxTilt)
            tilt -= tiltSpeed * Time.deltaTime;
        if (tilt < -maxTilt)
            tilt = -maxTilt;
    }

    void TiltRight() {
        if (tilt < maxTilt)
            tilt += tiltSpeed * Time.deltaTime;
        if (tilt > maxTilt)
            tilt = maxTilt;
    }

    void FixedUpdate () {
        RB.velocity = transform.forward * RB.velocity.magnitude;
        if (myInput == InputXx.TurnLeft) {
            if(RB.angularVelocity.y > Mathf.Deg2Rad * -anglesPerSecond) {
                RB.AddTorque(Vector3.down * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            } else if (RB.angularVelocity.y < Mathf.Deg2Rad * anglesPerSecond) {
                RB.AddTorque(Vector3.up * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            }
        } else if (myInput == InputXx.TurnRight) {
            if (RB.angularVelocity.y < Mathf.Deg2Rad * anglesPerSecond) {
                RB.AddTorque(Vector3.up * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            } else if (RB.angularVelocity.y > Mathf.Deg2Rad * anglesPerSecond) {
                RB.AddTorque(Vector3.down * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            }
        } else {
            if (RB.angularVelocity.y > 0) {
                RB.AddTorque(Vector3.down * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            } else if (RB.angularVelocity.y < 0) {
                RB.AddTorque(Vector3.up * Mathf.Deg2Rad * angleAcceleration, ForceMode.Acceleration);
            }
        }
        if (RB.velocity.magnitude < speed) {
            RB.AddForce(acceleration * transform.forward, ForceMode.Acceleration);
        } else if (RB.velocity.magnitude > speed)
            RB.AddForce(-acceleration * transform.forward, ForceMode.Acceleration);
    }
	
	// Update is called once per frame
	void Update () {
        if (myInput == InputXx.TurnLeft) {
            if (tiltInwards) {
                TiltLeft();
            } else {
                TiltRight();
            }
        } else if (myInput == InputXx.TurnRight) {
            if (tiltInwards) {
                TiltRight();
            } else {
                TiltLeft();
            }
        } else {
            if (tilt > 0) {
                tilt -= tiltSpeed * Time.deltaTime;
                if (tilt < 0)
                    tilt = 0;
            } else if (tilt < 0) {
                tilt += tiltSpeed * Time.deltaTime;
                if (tilt > 0)
                    tilt = 0;
            }
            if (myInput == InputXx.Fire) {
                cannon.Fire();
            } else if (myInput == InputXx.Charge) {
                cannon.Charge();
            }
        }
        catShip.eulerAngles = Vector3.up * (90 + transform.eulerAngles.y) + tilt * Vector3.right;
        cannon.CannonUpdate();
	}
}
