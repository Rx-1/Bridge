using UnityEngine;
using System.Collections;

public class ButtonFunktions : MonoBehaviour {

    public InputXx myInput = InputXx.Null;

    bool buttonOn = false;

    PlayerAvatarMover player;
    ShipFunktions ship;

    void OnTriggerEnter() {
        buttonOn = true;
    }

    void OnTriggerExit() {
        buttonOn = false;
        ship.myInput = InputXx.Null;
    }

	// Use this for initialization
	void Start () {
        player = PlayerAvatarMover.player;
        ship = ShipFunktions.player;
	}
	
	// Update is called once per frame
	void Update () {
	    if(buttonOn) {
            if(player.IsStill()) {
                ship.myInput = myInput;
            } else {
                ship.myInput = InputXx.Null;
            }
        }
	}
}
