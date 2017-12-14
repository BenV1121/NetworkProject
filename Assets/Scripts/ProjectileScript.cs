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

    public Vector2 mousePositionP;

    public GameObject hitEffect;

    public static ProjectileScript localProjectileScript;

    public void SetOwner(PlayerController p)
    { player = p; }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        rb2.transform.Translate((mousePositionP - playerPosition).normalized * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    { Destroy(gameObject); }
}
