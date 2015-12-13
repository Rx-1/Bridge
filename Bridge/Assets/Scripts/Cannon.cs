using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public static Cannon cannon;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
    public bool addParentVelocity = true;
    public float shotsPerMin = 60;
    public bool mustBeChargedToFire = false;
    public float chargeSpeedAdd = 10;
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

        //Charge'n Fire
        if(chargeTrigger) {
            chargeTimer += Time.deltaTime;
            //for (; chargeTimer >= chargingTime && charges < maxCharges; chargeTimer -= chargingTime, charges++)
            //    ; 
            while (chargeTimer >= chargingTime && charges < maxCharges) {
                charges++;
                chargeTimer -= chargingTime;
            }
        } else {
            chargeTimer = 0;
            if(fireTrigger && shotTimer <= 0 && (!mustBeChargedToFire || charges > 0)) {
                GameObject projectile = GameObject.Instantiate(projectilePrefab);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                Vector3 projectileVelocity = projectile.transform.up * (projectileSpeed + (chargeSpeedAdd * charges));
                if(mustBeChargedToFire)
                    projectileVelocity -= projectile.transform.up * chargeSpeedAdd;
                if (addParentVelocity)
                    projectileVelocity += parentRB.velocity;
                projectile.GetComponent<Rigidbody>().velocity = projectileVelocity;
                shotTimer = shotsPerMin / 60;
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
