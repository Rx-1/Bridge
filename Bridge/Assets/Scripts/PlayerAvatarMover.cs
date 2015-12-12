using UnityEngine;
using System.Collections;

public class PlayerAvatarMover : MonoBehaviour {

    public string ourAxis = "Horizontal";
    public float minimumInput;

    public Transform leftRestrain;
    public Transform rightRestrain;

    public float acceleration = 20;
    public float topSpeed = 10;

    Animator AC;

    float leftR;
    float rightR;

    float speed = 0;

    GameObject button;

    void OnTriggerEnter(Collider c) {
        if (button)
            Debug.LogError("Entered button while alraedy having button.");
        button = c.gameObject;
    }

    void OnTriggerExit(Collider c) {
        if (button != c.gameObject) {
            Debug.LogError("Left an unassigned button.");
        } else {
            button = null;
        }
    }

    // Use this for initialization
    void Start () {
        if(leftRestrain) {
            leftR = leftRestrain.localPosition.x;
        } else {
            if(rightRestrain) {
                Debug.LogError("Undefined left restrain.");
            } else {
                Debug.LogError("Undefined both restrains.");
            }
        }
        if(rightRestrain) {
            rightR = rightRestrain.localPosition.x;
        } else {
            Debug.LogError("Undefined right restrain.");
        }
        AC = gameObject.GetComponent<Animator>();
        if (!AC)
            Debug.LogError("No animator found.");
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxisRaw(ourAxis)) > minimumInput) {
            float inputStrengthXx = Mathf.Clamp(Input.GetAxisRaw(ourAxis), -1f, 1f);
            speed += inputStrengthXx * acceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, -topSpeed, topSpeed);
        } else if (button) {
            float directionXx = button.transform.localPosition.x - transform.localPosition.x;
            if (directionXx != 0) {
                speed += (directionXx / Mathf.Abs(directionXx)) * acceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, -topSpeed, topSpeed);
                if (directionXx > 0 != button.transform.localPosition.x - (transform.localPosition + Vector3.right * speed * Time.deltaTime).x > 0) {
                    transform.localPosition += Vector3.right * directionXx;
                    speed = 0;
                }
            } else {
                speed = 0;
            }
        } else if (speed != 0) {
            if (Mathf.Abs(speed) < Mathf.Abs(acceleration * Time.deltaTime)) {
                speed = 0;
            } else {
                speed -= (speed / Mathf.Abs(speed)) * acceleration * Time.deltaTime;
                speed = Mathf.Clamp(speed, -topSpeed, topSpeed);
            }
        }
        transform.localPosition += Vector3.right * speed * Time.deltaTime;
        if(transform.localPosition.x < leftR) {
            transform.localPosition += Vector3.right * (leftR - transform.localPosition.x);
            speed = 0;
        } else if(transform.localPosition.x > rightR) {
            transform.localPosition += Vector3.right * (rightR - transform.localPosition.x);
            speed = 0;
        }
        if (speed < 0) {
            AC.Play("WalkLeft");
        } else if (speed > 0) {
            AC.Play("WalkRight");
        } else if (button) {
            AC.Play("Interact");
        } else {
            AC.Play("Idle");
        }
	}
}
