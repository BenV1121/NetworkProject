using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnoyingPlayer : NetworkBehaviour {
    public GameObject target;
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject impactEffect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isServer) { Destroy(gameObject); }

        else
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            NetworkServer.Spawn(effectIns);


            NetworkServer.Destroy(target.gameObject);
            NetworkServer.Destroy(gameObject);
        }

    }


    
}
