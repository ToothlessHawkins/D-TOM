﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMine : MonoBehaviour {


    public Rigidbody Projectile = null;
    public float force = 15; // for AddExplosionForce - explosionForce
    Vector3 pos; //// for AddExplosionForce - explosionPosition
    public float radius = 6; // for AddExplosionForce - explosionRadius
    public float upMod = 100; // for AddExplosionForce - upwardsModifier - leaving this at zero, so that the explosion force will be easier to control and utilize
    public ForceMode fMode = ForceMode.Impulse; // for AddExplosionForce - ForceMode - 4 options: Force, Acceleration, Impulse and VelocityChange, no idea which is best

    public float damageDropoff = 14;

    public float armTime = 1f;
    private float placeTime;

    GameObject player;
    PlayerTank playertank;

    public ParticleSystem explosion;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playertank = player.GetComponent<PlayerTank>();


        Projectile.constraints = RigidbodyConstraints.FreezeAll;

        placeTime = Time.time;
    }

    //collision - rocket with anything else
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy") {
            if (Time.time - placeTime > armTime)
            {
                pos = transform.position; //should be the projectile itself
                Collider[] colliders = Physics.OverlapSphere(pos, radius);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, pos, radius, upMod, fMode);

                    }

                    //if the collider's gameobject has the script HP
                    if ((hit.gameObject.GetComponent("HP") as HP) != null && hit.tag.Equals("Enemy"))
                    {
                        //calculate individual damage
                        float damage = 25 - (damageDropoff * ((Vector3.Distance(hit.gameObject.transform.position, pos)) / radius));

                        //call the objects takeDamage method
                        hit.gameObject.GetComponent<HP>().TakeDamage(damage);
                    }
                }
                blowTheFuckUp();
            }
        }
    }

    void blowTheFuckUp()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
