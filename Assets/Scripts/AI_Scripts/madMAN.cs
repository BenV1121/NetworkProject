using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madMAN : MonoBehaviour {

    bool isAwake;

    public float speed;
    public float stop;
   Transform target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            target = PlayerManager.players[(int)Random.Range(0, PlayerManager.players.Count)].gameObject.transform;

        if (Vector2.Distance(transform.position, target.position) > stop)
        { 
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
	}
}
