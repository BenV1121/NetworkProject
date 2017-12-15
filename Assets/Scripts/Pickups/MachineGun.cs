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
    public override void CmdFire(Vector2 dir)
    {
        ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
        projectileInstance.SetOwner(GetOwner());
        projectileInstance.direction = dir;
        NetworkServer.Spawn(projectileInstance.gameObject);
    }
}
