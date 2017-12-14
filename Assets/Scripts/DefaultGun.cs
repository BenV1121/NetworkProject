using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DefaultGun : Gun {

    //The cooldown for firing the gun
    public float fireCooldown;
    private float fireTimer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }

    [Command]
    public override void CmdFire(Vector2 mousePosition)
    {
        if (fireTimer <= 0)
        {
            ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
            projectileInstance.SetOwner(GetOwner());
            //projectile.mousePositionP = mousePosition;
            NetworkServer.Spawn(projectileInstance.gameObject);

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 100, notTohHit);
            Debug.DrawLine(transform.position, mousePosition);

            fireTimer = fireCooldown;
        }
    }
}
