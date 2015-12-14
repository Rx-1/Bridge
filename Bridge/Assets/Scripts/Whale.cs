using UnityEngine;
using System.Collections;

public class Whale : MonoBehaviour {

    public static int WhaleCount = 0;

    enum WhaleState {Sunked, Surfacing, Surfaced, Diving};
    WhaleState myState = WhaleState.Sunked;

    public GameObject particlePrefab;

    public float intialDepth = 10;
    public float feedLevel = 0;
    public float maxSpawnRadius = 120;
    public float minSpawnRadius = 30;
    public float speed = 10;
    public float destructionDelay = 10;

    bool readytofeed = false;

    Transform player;

    Animator AC;

    void OnTriggerEnter(Collider c) {
        if ((!c.isTrigger || readytofeed) && (myState == WhaleState.Surfacing || myState == WhaleState.Surfaced)) {
            if (c.isTrigger && readytofeed) {
                GameObject particleXx = GameObject.Instantiate(particlePrefab);
                particleXx.transform.position = transform.position;
                Destroy(particleXx, destructionDelay);
                GameManager.SavedWhales();
            }
            Destroy(gameObject, destructionDelay);
            WhaleCount--;
            myState = WhaleState.Diving;
            AC.SetBool("Dive", true);
        }
    }

	// Use this for initialization
	void Start () {
        AC = gameObject.GetComponentInChildren<Animator>();
        player = ShipFunktions.player.transform;
        WhaleCount++;
        transform.position += Vector3.down * intialDepth;
    }
	
	// Update is called once per frame
	void Update () {
	    if(myState == WhaleState.Sunked) {
            if((transform.position - player.position).magnitude < maxSpawnRadius && (transform.position - player.position).magnitude > minSpawnRadius) {
                myState = WhaleState.Surfacing;
            }
        } else if(myState == WhaleState.Surfacing) {
            transform.LookAt(player.position);
            transform.position += Vector3.up * speed * Time.deltaTime;
            if(transform.position.y >= feedLevel) {
                transform.position += Vector3.up * (feedLevel - transform.position.y);
                myState = WhaleState.Surfaced;
                AC.SetBool("Wake", true);
            }
        } else if (myState == WhaleState.Surfaced) {
            transform.LookAt(player.position);
            if (AC.GetCurrentAnimatorStateInfo(AC.GetLayerIndex("Base Layer")).IsName("MouthAgape"))
                readytofeed = true;
        } else {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
	}
}
