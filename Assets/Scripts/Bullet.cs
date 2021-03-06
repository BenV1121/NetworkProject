﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public Vector3 dir;
    private Transform target;

    public float speed = 70f;
    public GameObject impactEffect;
public void Awake()
    {
        //Vector3 dir = target.position - transform.position;
      
    }
    public void Seek(Transform _target)
    {
        Debug.Assert(_target != null);

        target = _target;
        dir = target.position - transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            NetworkServer.Destroy(gameObject);
            return;
        }

        //Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
   
    //void OnCollisionEnter2D(Collision2D other)
    //{


    //    if (!isServer) { Destroy(gameObject); }

    //    else
    //    {
    //        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
    //        NetworkServer.Spawn(effectIns);


    //        //NetworkServer.Destroy(target.gameObject);
    //        NetworkServer.Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" ||
            collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Floor")
        {
            StartCoroutine(DestroyAfterTime(.1f));
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            NetworkServer.Spawn(effectIns);
        }

        //if (!isServer) { Destroy(gameObject); }

        //else
        //{
        //    GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        //    NetworkServer.Spawn(effectIns);


        //    //NetworkServer.Destroy(target.gameObject);
        //    //NetworkServer.Destroy(gameObject);
        //}
    }

    private void OnBecameInvisible()
    {
        StartCoroutine(DestroyAfterTime(.5f));
    }

    IEnumerator DestroyAfterTime(float time)
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(gameObject);
    }

}
