using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechStats : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;
    public Text boostText;
    public Image boostBar;

    private int maxHealth = 120;
    public int health = 120;
    public float maxBoost = 100;
    public float boost = 100;
    
    private StateMachine sm;

    private void Start()
    {
        sm = GetComponent<StateMachine>();
        health = maxHealth;
        boost = maxBoost;
    }

    private void Update()
    {
        boostText.text = "Boost: " + boost.ToString("F0") + "%";
        if (boost >= maxBoost)
        {
            boost = maxBoost;
            sm.boostExhausted = false;
        }
        if (boost < 0) boost = 0;
        if (boost == 0) sm.boostExhausted = true;

        BoostBarFiller();
    }

    void BoostBarFiller()
    {
        boostBar.fillAmount = boost / maxBoost;
    }
}
