using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Pickup : NetworkBehaviour {

    protected PlayerController pc;

    [Command]
    public abstract void CmdPickupEffect();

    [Server]
    protected void Die()
    {
        NetworkServer.Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            pc = other.gameObject.GetComponent<PlayerController>();
            CmdPickupEffect();
            Die();
        }
    }
}
