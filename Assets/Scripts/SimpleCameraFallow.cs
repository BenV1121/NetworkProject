using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SimpleCameraFallow : NetworkBehaviour {
    [Header("offset")]
    public Vector3 offset;

    [Header("Target & smoothSpeed")]
    public GameObject player;
    public Transform target;
    public float smoothSpeed = 0.125f;
    private SimpleCameraFallow simpleCameraFallow;
    private void Start()
    {
        
    }

    void Player ()
    {

    }
    void FixedUpdate()
    {   // makes it work on the server  
        if (target != null) {
            simpleCameraFallow = this; }
        // sets the the target
        else {
            player = PlayerManager.localPlayer.gameObject;
            target = PlayerManager.localPlayer.transform;
        }

        Vector3 DesiredPosition = target.position + offset;
        Vector3 smoothposition = Vector3.Lerp(transform.position, DesiredPosition, smoothSpeed);
        transform.position = smoothposition;

    }

}
