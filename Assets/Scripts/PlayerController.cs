using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    float force;

    bool facingRight = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float move = Input.GetAxis("Horizontal");

        force = move * speed;

        transform.Translate(new Vector2(force, 0));
	}
}
