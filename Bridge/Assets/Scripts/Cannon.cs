using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public static Cannon cannon;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
    public float projectileDistance = 30;
    public float projectileHeight = 15;
    public bool addParentVelocity = true;
    public float shotsPerMin = 60;
    public bool mustBeChargedToFire = false;
    public float chargeDistanceAdd = 30;
    public float chargeHeightAdd = 15;
    public int maxCharges = 3;
    public float chargingTime = 2;

    float shotTimer = 0;
    float chargeTimer = 0;
    int charges = 0;

    bool fireTrigger = false;
    bool chargeTrigger = false;

    Rigidbody parentRB;

    void Awake() {
        if (cannon)
            Debug.LogError("Only one cannon allowed");
        cannon = this;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetComponent<MeshRenderer>());
        Destroy(transform.GetComponent<Mesh>());
    }

    // Use this for initialization
    void Start () {
        parentRB = ShipFunktions.player.transform.GetComponent<Rigidbody>();
    }

    public void Fire() {
        fireTrigger = true;
    }

    public void Charge() {
        chargeTrigger = true;
    }

    public void CannonUpdate() {
        //Shot Timer
        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;

        //Charge'n Fire   D = vx * t ||  t = 2 * 2%(2h / g) || D = vx * 2 * 2%(2h / g) || D / 2vx = 2%(2h / g) || (D / 2vx)%2  = 2h / g || g = 2h / (D / 2vx)%2 h = at2/2 || 2h / t2 = a
        if(chargeTrigger) {
            chargeTimer += Time.deltaTime;
            //for (; chargeTimer >= chargingTime && charges < maxCharges; chargeTimer -= chargingTime, charges++)
            //    ; 
            while (chargeTimer >= chargingTime && charges < maxCharges) {
                charges++;
                chargeTimer -= chargingTime;
                Debug.Log(charges);
            }
        } else {
            chargeTimer = 0;
            if(fireTrigger && shotTimer <= 0 && (!mustBeChargedToFire || charges > 0)) {
                GameObject projectile = GameObject.Instantiate(projectilePrefab);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                float proDis = projectileDistance + charges * chargeDistanceAdd;
                float proHei = projectileHeight + charges * chargeHeightAdd;
                if (mustBeChargedToFire) {
                    proDis -= chargeDistanceAdd;
                    proHei -= projectileHeight;
                }
                float gra = (2 * proHei) / Mathf.Pow(proDis / (2 * projectileSpeed), 2);
                Vector3 projectileVelocity = ((projectile.transform.up - (Vector3.up * projectile.transform.up.y)).normalized * projectileSpeed) + Vector3.up * gra * (proDis / (2 * projectileSpeed));
                if (addParentVelocity)
                    projectileVelocity += parentRB.velocity;
                projectile.GetComponent<CannonBall>().projectileVelocity = projectileVelocity;
                projectile.GetComponent<CannonBall>().projectileGravity = gra * Vector3.down;
                shotTimer = shotsPerMin / 60;
                charges = 0;
                Debug.Log(charges);
            }
        }

        //Reset Triggers
        fireTrigger = false;
        chargeTrigger = false;
    }

    // Update is called once per frame
    void Update () {
        
	}
}
