using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyScript : NetworkBehaviour
{
    public float health = 5;
    public bool isDead;

    public GameObject deathEffect;

	// Use this for initialization
	void Start ()
    {
        isDead = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isDead == true)
        {
            Death();
        }
	}


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Destroy(collider.gameObject);

            health -= 1;

            if(health <= 0)
            {
                isDead = true;
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);

        Destroy(Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject, 2);
    }
}
