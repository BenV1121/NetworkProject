using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ProjectileScript : NetworkBehaviour
{
    PlayerController player;
    Rigidbody2D rb2;
    public float speed;
    public int damage;

    public float delayTime;

    public Vector2 direction;

    public GameObject hitEffect;

    public static ProjectileScript localProjectileScript;

    EnemyScript enemy;

    public void SetOwner(PlayerController p)
    { player = p; }

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2.transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnBecameInvisible()
    {
        StartCoroutine(DestroyAfterTime(1));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Floor")
        {
            StartCoroutine(DestroyAfterTime(.1f));

            NetworkServer.Spawn(Instantiate(hitEffect, transform.position, gameObject.transform.rotation));
        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(gameObject);
    }
}
