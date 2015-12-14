using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugChargeDisplay : MonoBehaviour {

    Text text;
    int charges = 0;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(charges != Cannon.cannon.ChargeLevel()) {
            charges = Cannon.cannon.ChargeLevel();
            text.text = "Charges: " + charges;
        }
	}
}
