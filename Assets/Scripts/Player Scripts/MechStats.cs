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
    public RawImage warningUI;
    public Color blink0;
    public Color blink1;
    private float blinkSpeed = 3;

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
        if (boost == 0)
        {
            sm.boostExhausted = true;
        }

        BoostBarFiller();
    }

    private void FixedUpdate()
    {
        if (sm.boostExhausted) warningUI.color = Color.Lerp(blink0, blink1, Mathf.PingPong(Time.time * blinkSpeed, .5f));
        else warningUI.color = blink0;
    }

    void BoostBarFiller()
    {
        boostBar.fillAmount = boost / maxBoost;
    }
}
