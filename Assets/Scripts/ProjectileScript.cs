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

    public Vector2 direction;

    public GameObject hitEffect;

    public static ProjectileScript localProjectileScript;

    public void SetOwner(PlayerController p)
    { player = p; }

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log("Dir:" + direction);
        rb2.transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    { Destroy(gameObject); }
}
