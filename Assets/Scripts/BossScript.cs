using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossScript : NetworkBehaviour
{
    public float health = 1000;

    public GameObject deathEffect;
    public GameObject hitEffect;
    public BossHandScript  hand1;
    public BossHandScript2 hand2;

    public enum bossState { invincible, damaged, death};

    public bossState state;

    // Use this for initialization
    void Start()
    {
        state = bossState.invincible;

        hand1 = GetComponentInChildren<BossHandScript>();
        hand2 = GetComponentInChildren<BossHandScript2>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Boss: " + state);

        if (health == 800)
        {
            state = BossScript.bossState.invincible;

            health -= 1;
            hand1.health = 5;
            hand2.health = 5;

            hand1.state = BossHandScript.handState.vulnerable;
            hand1.anim.SetBool("leftIsDamaged", false);
            hand2.state = BossHandScript2.handState.vulnerable;
            hand2.anim.SetBool("rightIsDamaged", false);
        }

        if (health == 600)
        {
            state = BossScript.bossState.invincible;

            health -= 1;
            hand1.health = 5;
            hand2.health = 5;

            hand1.state = BossHandScript.handState.vulnerable;
            hand1.anim.SetBool("leftIsDamaged", false);
            hand2.state = BossHandScript2.handState.vulnerable;
            hand2.anim.SetBool("rightIsDamaged", false);
        }

        if (health == 400)
        {
            state = BossScript.bossState.invincible;

            health -= 1;
            hand1.health = 5;
            hand2.health = 5;

            hand1.state = BossHandScript.handState.vulnerable;
            hand1.anim.SetBool("leftIsDamaged", false);
            hand2.state = BossHandScript2.handState.vulnerable;
            hand2.anim.SetBool("rightIsDamaged", false);
        }

        if (health == 200)
        {
            state = BossScript.bossState.invincible;

            health -= 1;
            hand1.health = 5;
            hand2.health = 5;

            hand1.state = BossHandScript.handState.vulnerable;
            hand1.anim.SetBool("leftIsDamaged", false);
            hand2.state = BossHandScript2.handState.vulnerable;
            hand2.anim.SetBool("rightIsDamaged", false);
        }

        if (health == 50)
        {
            state = BossScript.bossState.invincible;

            health -= 1;
            hand1.health = 20;
            hand2.health = 20;

            hand1.state = BossHandScript.handState.desperation;
            hand1.anim.SetBool("leftIsDamaged", false);
            hand2.state = BossHandScript2.handState.desperation;
            hand2.anim.SetBool("rightIsDamaged", false);
        }

        if (hand1.health <= 0 && hand2.health <= 0)
        {
            state = bossState.damaged;
            hand1.state = BossHandScript.handState.damaged;
            hand2.state = BossHandScript2.handState.damaged;
        }

        if (health <= 0)
        {
            state = bossState.death;
        }

        if (state == bossState.death)
        {
            CmdDeath();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            if(state != bossState.invincible)
                health -= 1;
        }
    }

    [Command]
    void CmdDeath()
    {
        NetworkServer.Destroy(gameObject);
        NetworkServer.Spawn(Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation));
    }
}
