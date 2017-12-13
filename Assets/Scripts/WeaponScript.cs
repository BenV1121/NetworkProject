using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponScript : NetworkBehaviour
{

    public float fireRate = 1f;
    public LayerMask notToHit;
    public GameObject shotPrefab;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {

        }
	}
}
