using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickUpMachineGun : Pickup
{

    [Command]
    public override void CmdPickupEffect()
    {
        pc.isDF = false;
        pc.isMF = true;

        pc.mfLimit = 400;
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
