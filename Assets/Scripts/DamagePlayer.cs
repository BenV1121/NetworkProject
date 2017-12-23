using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamagePlayer : NetworkBehaviour {

    public float damageAmount;

    [Command]
    private void CmdDamage(GameObject pc)
    {
        pc.GetComponent<PlayerController>().setHealth(pc.GetComponent<PlayerController>().health - damageAmount);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

    //        //Make sure the variable is valid...
    //        if(pc != null)
    //            CmdDamage(pc.gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            //Make sure the variable is valid...
            if (pc != null)
                CmdDamage(pc.gameObject);
        }
    }
}
