using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    float force;

    private Rigidbody2D rgb2;

    public bool groundCheck = true;
    //public bool facingRight = true;
    //public GameObject projectilePrefab;
    public ProjectileScript projectile;

    public static PlayerController localPlayerController;

    private Action NetUpdate = null;

    public Gun gun;

    DefaultGun df;
    MachineGun mf;
    Shotgun    sf;

    public bool isDF;
    public bool isMF;
    public bool isSF;

    // Use this for initialization
    void Start ()
    {
        if (isLocalPlayer) { localPlayerController = this; }
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        //projectilePrefab = (GameObject)Resources.Load("Bullet");

        gun = GetComponent(typeof(Gun)) as Gun;
        df = GetComponent<DefaultGun>();
        mf = GetComponent<MachineGun>();
        sf = GetComponent<Shotgun>();

        NetworkSetup();

        isDF = true;
        isMF = false;
        isSF = false;
    }

    // Get the direction to fire the bullet in
    private Vector2 bulletDirectionVector
    {
        get
        {
            Vector2 flatPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return (targetPosition - flatPosition).normalized;
        }
    }

    void NetworkSetup()
    {
        if (isLocalPlayer)
        {
            PlayerManager.localPlayer = this;

            SetLocalDelegates();
        }
    }

    void SetLocalDelegates()
    {
        // Set the net update to local update.
        NetUpdate += LocalUpdate;
    }

    void LocalUpdate()
    {

    }

    [Command]
    private void CmdFire(Vector2 dir)
    {
        ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
        //projectileInstance.SetOwner(this);
        projectileInstance.direction = dir;
        NetworkServer.Spawn(projectileInstance.gameObject);
    }

    // Update is called once per frame
    private void Update ()
    {
        if (hasAuthority)
        { 
            float move = Input.GetAxis("Horizontal");
            force = move * speed;
            transform.Translate(new Vector2(force, 0));
        }

        if (Input.GetKeyDown("space") && groundCheck)
        {
            if(hasAuthority)
            rgb2.AddForce(Vector2.up * 300);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {

        }

        if (isDF == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if(hasAuthority)
                    df.CmdFire(bulletDirectionVector);
            }
        }
        else if (isMF == true)
        {
            if (Input.GetButton("Fire1"))
            {
                if (hasAuthority)
                    mf.CmdFire(bulletDirectionVector);
            }
        }
        else if (isSF == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hasAuthority)
                    sf.CmdFire(bulletDirectionVector);
            }
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //if()
        groundCheck = true;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        groundCheck = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        groundCheck = false;
    }
}
