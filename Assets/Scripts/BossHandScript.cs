using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossHandScript : NetworkBehaviour
{
    public float health = 250;
    public bool attackFinish = false;
    public bool dropItem = false;

    public float attackTime = 3;

    public GameObject deathEffect;
    public GameObject hitEffect;

    public List<GameObject> powerUps;
    int spawnIdx;

    public BossScript boss;

    public Animator anim;

    public enum handState { vulnerable, damaged, desperation }

    public handState state;

    // Use this for initialization
    void Start()
    {
        state = handState.vulnerable;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hands: " + state);

        if (health <= 0)
        {
            state = handState.damaged;
        }

        if (state == handState.damaged)
        {
            CmdDeath();
        }

        if(state == handState.vulnerable)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                anim.SetBool("attackTimerOff", true);
                attackTime = 0;
            }
        }

        if(state == handState.desperation)
        {
            anim.SetBool("desperate", true);
        }

        if(attackFinish == true)
        {
            anim.SetBool("attackTimerOff", false);
            attackTime = 6;
        }

        if (dropItem == true)
        {
            spawnIdx = Random.Range(0, powerUps.Count);
            NetworkServer.Spawn(Instantiate(powerUps[spawnIdx], gameObject.transform.position, gameObject.transform.rotation));
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            health -= 1;
        }
    }

    [Command]
    void CmdDeath()
    {
        //NetworkServer.Destroy(gameObject);
        anim.SetBool("leftIsDamaged", true);
    }

}
