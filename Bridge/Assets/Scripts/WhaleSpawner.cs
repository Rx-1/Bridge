using UnityEngine;
using System.Collections;

public class WhaleSpawner : MonoBehaviour {

    public GameObject whalePrefab;
    public string ignoreLayer;

    float radiusXx;

    int myLayerMask;

    int i = 0;

	// Use this for initialization
	void Start () {
        myLayerMask = Physics.AllLayers;
        if (LayerMask.NameToLayer(ignoreLayer) > -1) {
            myLayerMask = myLayerMask ^ (1 << LayerMask.NameToLayer(ignoreLayer));
        }
        radiusXx = whalePrefab.GetComponent<SphereCollider>().radius * whalePrefab. transform.lossyScale.x;
        if (!whalePrefab)
            Debug.LogError("Set whale prefab.");
    }

    void TryToCreate() {
        Vector3 positionXx = new Vector3(Random.Range(LevelBounds.leftLimit, LevelBounds.rightLimit), 0, Random.Range(LevelBounds.bottomLimit, LevelBounds.topLimit));
        if(!Physics.CheckSphere(positionXx, radiusXx, myLayerMask, QueryTriggerInteraction.Collide)) {
            GameObject whale = GameObject.Instantiate(whalePrefab);
            whale.transform.position = positionXx;
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(i < 100)
            TryToCreate();
	}
}
