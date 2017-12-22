using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madMAN : MonoBehaviour {

    public float speed;
    public float stop;
   Transform target;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, target.position) > stop)
        { 
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
	}
}
