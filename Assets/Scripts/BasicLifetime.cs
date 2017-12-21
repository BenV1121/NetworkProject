using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BasicLifetime : NetworkBehaviour {

    public float lifetime;
    float timer;

	// Use this for initialization
	void Start () {
        timer = lifetime;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        NetworkServer.Destroy(this.gameObject);
	}
}
