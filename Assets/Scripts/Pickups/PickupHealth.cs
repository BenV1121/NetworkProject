using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupHealth : Pickup {

    public float healthAmount;

    //Have the server modify the player variables
    [Command]
    public override void CmdPickupEffect()
    {
        if (pc.health + healthAmount < pc.maxHealth)
            pc.setHealth(pc.health + healthAmount);
        else
            pc.setHealth(pc.maxHealth);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            pc = other.gameObject.GetComponent<PlayerController>();
            CmdPickupEffect();
            Die();
        }
    }
}
