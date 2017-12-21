using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemDrop : NetworkBehaviour
{
    public float health = 1;
    public bool isDead;
    int spawnIdx;

    public GameObject deathEffect;
    public GameObject hitEffect;

    public List<GameObject> powerUps;

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
            CmdDeath();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
             NetworkServer.Destroy(collider.gameObject);

            health -= 1;

            NetworkServer.Destroy(Instantiate(hitEffect, collider.transform.position, gameObject.transform.rotation));

            if (health <= 0)
            {
                isDead = true;
            }
        }
    }

    [Command]
    void CmdDeath()
    {
        Destroy(gameObject);

        spawnIdx = Random.Range(0, powerUps.Count);
        NetworkServer.Spawn(Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation));
        NetworkServer.Spawn(Instantiate(powerUps[spawnIdx], gameObject.transform.position, gameObject.transform.rotation));

    }
}
