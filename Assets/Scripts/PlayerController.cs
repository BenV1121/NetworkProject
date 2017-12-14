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

    private Action Movement = null;

    // Use this for initialization
    void Start ()
    {
        if (isLocalPlayer) { localPlayerController = this; }
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        //projectilePrefab = (GameObject)Resources.Load("Bullet");

        NetworkSetup();

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
    { ProcessInput(); }

    bool shootInput
    { get { return Input.GetButtonDown("Fire1"); } }

    void ProcessInput()
    { requestShoot(); }

    void requestShoot()
    {
        if(shootInput)
        {
            CmdFire(bulletDirectionVector);
        }
    }

    [Command]
    private void CmdFire(Vector2 dir)
    {
        Debug.Log("Recv: " + dir);
        ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
        projectileInstance.SetOwner(this);
        projectileInstance.direction = dir;
        NetworkServer.Spawn(projectileInstance.gameObject);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 100, notTohHit);
        Debug.DrawLine(transform.position, dir);
    }

    // Update is called once per frame
    private void Update ()
    {
        float move = Input.GetAxis("Horizontal");
        force = move * speed;
        transform.Translate(new Vector2(force, 0));

        if (Input.GetKeyDown("space") && groundCheck)
        {
            rgb2.AddForce(Vector2.up * 300);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {

        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Sen: " + bulletDirectionVector);
            CmdFire(bulletDirectionVector);
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
