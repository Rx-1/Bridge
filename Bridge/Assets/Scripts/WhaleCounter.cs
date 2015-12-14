using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhaleCounter : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Whales fed " + GameManager.WhalesSaved() + " of " + GameManager.Goal();
	}
}
