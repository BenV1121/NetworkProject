using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class ItemDrop : MonoBehaviour
{
    public float health = 1;
    public bool isDead;

    public GameObject deathEffect;
    public GameObject hitEffect;

    public GameObject powerUps;

    // Use this for initialization
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            Death();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Network.Destroy(collider.gameObject);

            health -= 1;

            //NetworkServer.
            Destroy(Network.Instantiate(hitEffect, collider.transform.position, gameObject.transform.rotation, 0) as GameObject, 2);

            if (health <= 0)
            {
                isDead = true;
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);

        //NetworkServer.
        Destroy(Network.Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation, 0) as GameObject, 2);
        Network.Instantiate(powerUps, gameObject.transform.position, gameObject.transform.rotation, 0);
    }
}
