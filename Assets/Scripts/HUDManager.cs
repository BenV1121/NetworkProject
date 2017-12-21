using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : NetworkBehaviour {

    private static HUDManager _instance;

    //Reference to the health bar so we can update it.
    public Image healthBar;
    public Text speedCooldown;
    public GameObject speedTimer;

    //An icon that shows what gun you have.
    //public GameObject gunIcon;

    //Icons that show which buffs are active and their cooldowns
    //public GameObject speedBuff;
    //public GameObject jumpBuff;

    //public getter
    public static HUDManager instance
    { get { return _instance; } }

    public PlayerController player
    { get { return PlayerManager.localPlayer; } }

    public void Awake()
    {
        if (!instance)
            _instance = this;
        else
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
        SetUpDelegates();
        InitValues();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHealth();
        UpdateSpeed();
	}

    void SetUpDelegates()
    {

    }
    void InitValues()
    {

    }

    void UpdateHealth()
    {
        healthBar.fillAmount = player.health / player.maxHealth;
    }

    void UpdateSpeed()
    {
        speedTimer.SetActive(player.speedBoostTimer > 0);
        speedCooldown.text = (Mathf.Ceil(player.speedBoostTimer)).ToString();
    }
   
}
