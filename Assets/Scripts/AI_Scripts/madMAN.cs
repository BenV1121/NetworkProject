using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madMAN : MonoBehaviour {

    public float speed;
    public float stop;
    public string PlayerTag = "Player";
    public float triggerRang = 15;

   Transform target;
    // Use this for initializatio

 

    void Start () {
        target = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Transform>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {


        if (target != null && Vector2.Distance(transform.position, target.position) > stop)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }

    }

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach (GameObject player in players)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null && shortestDistance <= triggerRang)
        {
            target = nearestPlayer.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerRang);
    }
}
