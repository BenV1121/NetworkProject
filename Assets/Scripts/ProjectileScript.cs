using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public float shootingRate = 1f;

    private float shootCooldown;

	// Use this for initialization
	void Start ()
    {
        shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
