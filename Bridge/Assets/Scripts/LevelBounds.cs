using UnityEngine;
using System.Collections;

public class LevelBounds : MonoBehaviour {

    public Transform topLeftCorner;
    public Transform bottomRightCorner;

    public static float leftLimit;
    public static float rightLimit;
    public static float topLimit;
    public static float bottomLimit;

	// Use this for initialization
	void Start () {
        if (!topLeftCorner) {
            if (!bottomRightCorner) {
                Debug.LogError("Define Corners");
            } else {
                Debug.LogError("Define topLeftCorner");
            }
        } else if (!bottomRightCorner) {
            Debug.LogError("Define bottomRightCorner");
        } else {
            leftLimit = topLeftCorner.position.x;
            topLimit = topLeftCorner.position.z;
            rightLimit = bottomRightCorner.position.x;
            bottomLimit = bottomRightCorner.position.z;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
