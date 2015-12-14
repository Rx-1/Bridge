using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargeDisplay : MonoBehaviour {

    public GameObject[] chargeIcons;
    public Image chargeMetter;

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
            for (int i = 0; i < chargeIcons.Length; i++) {
                chargeIcons[i].SetActive(false);
            }
            for (int i = 0; i < charges && i < chargeIcons.Length; i++) {
                chargeIcons[i].SetActive(true);
            }
        }
        chargeMetter.fillAmount = Cannon.cannon.ChargeProgress();
	}
}
