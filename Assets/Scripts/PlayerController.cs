using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
	}


    [Command]
    void CmdFire()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        ProjectileScript projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity) as ProjectileScript;
        projectileInstance.SetOwner(this);
        projectile.mousePositionP = mousePosition;
        NetworkServer.Spawn(projectileInstance.gameObject);

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition, 100, notTohHit);
        Debug.DrawLine(transform.position, mousePosition);
    }

	// Update is called once per frame
	void Update ()
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

        if(Input.GetButtonDown("Fire1"))
        {
            CmdFire();
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
