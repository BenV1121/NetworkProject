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
    public GameObject noDamageEffect;

    public static ProjectileScript localProjectileScript;

    EnemyScript enemy;
    public BossScript boss;

    public void SetOwner(PlayerController p)
    { player = p; }

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<BossScript>() as BossScript;
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
        if (collider.gameObject.tag == "Enemy")
        {
            StartCoroutine(DestroyAfterTime(2));

            NetworkServer.Spawn(Instantiate(hitEffect, gameObject.transform.position, gameObject.transform.rotation));
        }

        if (collider.gameObject.tag == "Boss")
        {
            StartCoroutine(DestroyAfterTime(2));
            
            if(boss.state == BossScript.bossState.damaged)
            {
                NetworkServer.Spawn(Instantiate(hitEffect, gameObject.transform.position, gameObject.transform.rotation));
            }
            else if(boss.state == BossScript.bossState.invincible)
            {
                StartCoroutine(DestroyAfterTime(2));
                NetworkServer.Spawn(Instantiate(noDamageEffect, gameObject.transform.position, gameObject.transform.rotation));
            }
        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
