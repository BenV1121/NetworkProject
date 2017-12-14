using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Gun {

    //How much spread the gun has.
    public float spreadDeviation;
    //The cooldown for firing the gun
    public float fireCooldown;
    private float fireTimer = 0;

    //HALF the number of extra bullets (if you want 3 total, put in 1)
    //We use half the extra amount because for every extra number here
    //it spawns one bullet with positive deviation and another with negative deviation
    public int extraBulletHalfCount;

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
            //spawn first bullet
            ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
            projectileInstance.SetOwner(GetOwner());
            //projectile.mousePositionP = mousePosition;
            NetworkServer.Spawn(projectileInstance.gameObject);

            //spawn other bullets
            //NOTE: if the bullets come out kinda weird...
            //make it so that all the bullets are spawned and don't move all at once...
            //then make them start moving all at once
            for(int i = 0; i < extraBulletHalfCount; i++)
            {
                ProjectileScript projectileInstanceA = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
                projectileInstance.SetOwner(GetOwner());
               ///* projectile.mousePositionP = mousePosition;// + V*/ector2(spreadDeviation);
                NetworkServer.Spawn(projectileInstance.gameObject);
            }

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 100, notTohHit);
            Debug.DrawLine(transform.position, mousePosition);

            fireTimer = fireCooldown;
        }
    }
}
