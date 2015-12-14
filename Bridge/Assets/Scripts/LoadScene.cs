using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public bool timedXx = false;
    public float restartTimer = 10;
    public string myScene;
    public string myAxis;

    float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(timedXx) {
            timer += Time.unscaledDeltaTime;
            if (timer > restartTimer)
                Application.LoadLevel(myScene);
        } else {
            if(Input.GetAxisRaw(myAxis) != 0)
                Application.LoadLevel(myScene);
        }
	}
}
