using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupSpeed : Pickup
{

    public float speedAmount = 0.3f;
    public float duration = 5;

    //Have the server modify the player variables
    [Command]
    public override void CmdPickupEffect()
    {
        pc.speedBoostTimer = duration;
        pc.speed = speedAmount;

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
