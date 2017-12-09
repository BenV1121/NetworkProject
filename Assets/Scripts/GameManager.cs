using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    private static GameManager instanceRef;

    public static GameManager Instance
    {
        get
        {
            if(instanceRef == null) { instanceRef = new GameManager(); }
            return instanceRef;
        }
    }

    private GameManager()
    {
        //init stuff
    }

}
