using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MachineGun : Gun
{
    public HeadRotation rotator;
    public Transform firePoint;

    void Awake()
    {

        rotator = GetComponentInChildren<HeadRotation>();

        if (rotator != null)
        {
            //gunHead = rotator.transform.Find("gunHead_0");

            //if(gunHead != null)
            //{

            //}
            firePoint = rotator.transform.Find("FirePoint");
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
      
	}

    [Command]
    public override void CmdFire(Vector2 dir)
    {
        ProjectileScript projectileInstance = Instantiate(projectile, firePoint.position, Quaternion.identity) as ProjectileScript;
        projectileInstance.SetOwner(GetOwner());
        projectileInstance.direction = dir;
        NetworkServer.Spawn(projectileInstance.gameObject);
    }
}
