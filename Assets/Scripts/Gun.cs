using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Gun : NetworkBehaviour {

    public Transform gunPoint;
    public ProjectileScript projectile;
    private PlayerController owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public abstract void CmdFire(Vector2 mousePosition);

    public void SetOwner(PlayerController p)
    { owner = p; }
    public PlayerController GetOwner()
    { return owner; }
}
