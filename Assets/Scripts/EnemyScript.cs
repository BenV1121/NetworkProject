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
    public GameObject hitEffect;

    // Use this for initialization
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isDead = true;
        }

        if (isDead == true)
        {
            CmdDeath();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            health -= 1;
            NetworkServer.Spawn(Instantiate(hitEffect, gameObject.transform.position, gameObject.transform.rotation));
        }
    }

    [Command]
    void CmdDeath()
    {
        NetworkServer.Destroy(gameObject);
        NetworkServer.Spawn(Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation));
    }
}
