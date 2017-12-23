using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public float speed;
    float force;

    public GameObject deathEffect;

    [SyncVar]
    public float speedBoostTimer;

    //Default values, used for when a power-up wears off.
    float baseSpeed;
    float baseJump;

    //How powerful the jump is.
    public float jumpForce = 300;

    [SyncVar]
    public float maxHealth = 10;
    [SyncVar]
    public float _health;

    private Rigidbody2D rgb2;

    public bool groundCheck = true;
    //public bool facingRight = true;
    //public GameObject projectilePrefab;
    public ProjectileScript projectile;

    public static PlayerController localPlayerController;

    private Action NetUpdate = null;

    public Gun gun;

    public HeadRotation rotator;
    public Transform gunHead;
    public Transform firePoint;

    public float health
    { get { return _health; } }

    DefaultGun df;
    MachineGun mf;
    Shotgun    sf;

    public bool isDF;
    public bool isMF;
    public bool isSF;

    public float mfLimit = 400;

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
            multyPLayerCamera cam = (FindObjectsOfType(typeof(multyPLayerCamera)) as multyPLayerCamera[])[0];
            cam.targets.Add(transform);
        if (isLocalPlayer) {
            localPlayerController = this;
        }
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        //projectilePrefab = (GameObject)Resources.Load("Bullet");

        //initialize defaults
        _health = maxHealth;
        baseSpeed = speed;
        baseJump = jumpForce;

        gun = GetComponent(typeof(Gun)) as Gun;
        df = GetComponent<DefaultGun>();
        mf = GetComponent<MachineGun>();
        sf = GetComponent<Shotgun>();

        NetworkSetup();

        isDF = true;
        isMF = false;
        isSF = false;

        speedBoostTimer = 0;
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    // Get the direction to fire the bullet in
    private Vector2 bulletDirectionVector
    {
        get
        {
            Vector2 flatPosition = new Vector2(firePoint.position.x, firePoint.position.y);
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
        Debug.Log("recieving " + firePoint.position);
        ProjectileScript projectileInstance = Instantiate(projectile, firePoint.position, Quaternion.identity) as ProjectileScript;
        //projectileInstance.SetOwner(this);
        projectileInstance.direction = dir;
        NetworkServer.Spawn(projectileInstance.gameObject);
    }

    public void StartSpeedBoost(float time, float newSpeed)
    {
        if(speedBoostTimer <= 0)
        {
            speedBoostTimer = time;
            StartCoroutine(SpeedBoost(newSpeed));
        }
        else
            speedBoostTimer = time;
    }

    IEnumerator SpeedBoost(float newSpeed)
    {
        speed = newSpeed;
        while (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            yield return null;
        }
        speed = baseSpeed;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (!isLocalPlayer)
            return;

        if(health <= 0)
        {
            CmdDie();
        }

        if (hasAuthority)
        { 
            float move = Input.GetAxis("Horizontal");
            force = move * speed;
            transform.Translate(new Vector2(force, 0));
        }

        if (Input.GetKeyDown("space") && groundCheck)
        {
            if(hasAuthority)
            rgb2.AddForce(Vector2.up * jumpForce);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {

        }

        if (isDF == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hasAuthority)
                    df.CmdFire(bulletDirectionVector);
            }
        }
        else if (isMF == true)
        {
            if (Input.GetButton("Fire1"))
            {
                if (hasAuthority)
                    mf.CmdFire(bulletDirectionVector);

                mfLimit -= 1;
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

        if(mfLimit <= 0)
        {
            isMF = false;
            isDF = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
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

    //
    //SETTERS
    //

    //Only let the server modify health
    [Server]
    public void setHealth(float h) { _health = h; Debug.Log(gameObject.name + " health: " + health); }

    [Command]
    private void CmdDie()
    {
        
        if(!isServer)
            Network.Disconnect(200);

        NetworkServer.Spawn(Instantiate(deathEffect, transform.position, transform.rotation));
        NetworkServer.Destroy(gameObject);
    }

}
