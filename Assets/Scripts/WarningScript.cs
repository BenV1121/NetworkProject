using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    public bool animationEnded = false;
	
	// Update is called once per frame
	void Update ()
    {
		if(animationEnded)
        {
            Destroy(gameObject);
        }
	}
}
