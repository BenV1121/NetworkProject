using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float force;

    private Rigidbody2D rgb2;

    public bool groundCheck = true;
    bool facingRight = true;

    // Use this for initialization
    void Start ()
    {
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
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
