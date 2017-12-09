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

    private void OnBecameInvisible()
    { Destroy(gameObject); }
}
