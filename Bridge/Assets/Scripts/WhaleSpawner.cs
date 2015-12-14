using UnityEngine;
using System.Collections;

public class WhaleSpawner : MonoBehaviour {

    public GameObject whalePrefab;
    public string ignoreLayer;
    public int maxWhales = 10;

    float depthXx;
    float feedLevelXx;

    float radiusXx;

    int myLayerMask;

	// Use this for initialization
	void Start () {
        myLayerMask = Physics.AllLayers;
        if (LayerMask.NameToLayer(ignoreLayer) > -1) {
            myLayerMask = myLayerMask ^ (1 << LayerMask.NameToLayer(ignoreLayer));
        }
        radiusXx = whalePrefab.GetComponent<SphereCollider>().radius * whalePrefab. transform.lossyScale.x;
        if (!whalePrefab)
            Debug.LogError("Set whale prefab.");
        depthXx = whalePrefab.GetComponent<Whale>().intialDepth;
        feedLevelXx = whalePrefab.GetComponent<Whale>().feedLevel;
    }

    void TryToCreate() {
        Vector3 positionXx = new Vector3(Random.Range(LevelBounds.leftLimit, LevelBounds.rightLimit), 0, Random.Range(LevelBounds.bottomLimit, LevelBounds.topLimit));
        if(!Physics.CheckCapsule(positionXx + (Vector3.up * feedLevelXx), positionXx + (Vector3.down * depthXx), radiusXx, myLayerMask, QueryTriggerInteraction.Collide)) {
            GameObject whale = GameObject.Instantiate(whalePrefab);
            whale.transform.position = positionXx;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Whale.WhaleCount < maxWhales)
            TryToCreate();
	}
}
