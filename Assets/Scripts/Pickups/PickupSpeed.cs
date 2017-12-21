using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupSpeed : Pickup
{

    public float speedAmount;

    //Have the server modify the player variables
    [Command]
    public override void CmdPickupEffect()
    {
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            pc = other.gameObject.GetComponent<PlayerController>();
            CmdPickupEffect();
            Die();
        }
    }
}
