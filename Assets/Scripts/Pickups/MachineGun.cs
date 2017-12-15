using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MachineGun : Gun {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
      
	}

    [Command]
    public override void CmdFire(Vector2 mousePosition)
    {
        ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
        projectileInstance.SetOwner(GetOwner());
        //projectile.mousePositionP = mousePosition;
        NetworkServer.Spawn(projectileInstance.gameObject);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 100, notTohHit);
        Debug.DrawLine(transform.position, mousePosition);
    }
}
