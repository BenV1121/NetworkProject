using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DefaultGun : Gun
{
    public HeadRotation rotator;
    public Transform firePoint;

    //The cooldown for firing the gun
    public float fireCooldown;
    private float fireTimer = 0;

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

    private Vector2 bulletDirectionVector
    {
        get
        {
            Vector2 flatPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return (targetPosition - flatPosition).normalized;
        }
    }

    // Update is called once per frame
    void Update () {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }

    [Command]
    public override void CmdFire(Vector2 dir)
    {
        if (fireTimer <= 0)
        {
            ProjectileScript projectileInstance = Instantiate(projectile, firePoint.position, Quaternion.identity) as ProjectileScript;
            projectileInstance.direction = dir;
            NetworkServer.Spawn(projectileInstance.gameObject);

            fireTimer = fireCooldown;
        }
    }
}
