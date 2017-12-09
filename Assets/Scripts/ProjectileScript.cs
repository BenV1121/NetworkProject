using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ProjectileScript : NetworkBehaviour
{
    PlayerController owner;
    Rigidbody2D rb2;
    public float speed;
    public int damage;

    public Vector2 mousePosition;

    public void SetOwner(PlayerController p)
    { owner = p; }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 ownerSpot = new Vector2(owner.transform.position.x, owner.transform.position.y);
        rb2.transform.Translate((mousePosition - ownerSpot).normalized * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //var hit = collision.gameObject;
        //var hitPlayer = hit.GetComponent<PlayerController>();
        //if (hitPlayer != null && hitPlayer != owner)
        //{
        //    Debug.Log("Happened");
        //    //var combat = hit.GetComponent<Combat>();
        //    //combat.TakeDamage(10);
        //    Destroy(gameObject);
        //}

    }

    void OnBecameInvisible()
    { Destroy(gameObject); }
}
