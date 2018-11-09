using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalCore : MonoBehaviour {

	public Text moneyUI;
    public Text dpsUI;

	public float updatePeriodSec;		

    float money;
    float moneyPerSecIndicator;

    float accumMoney;
    float nextTimeToCheckDPS;
    float dps;

	void Start () {
        money = 0f;
        dps = 0f;
        moneyPerSecIndicator = 0f; 
        nextTimeToCheckDPS = Time.time + 1f;
        accumMoney = 0f;
	}
	
	void Update () {
        DisplayInfos();	
        CheckDPS(); 
	}

    public void MakeMoney (float earning)
    {
        money += earning;
        accumMoney += earning;
    }

    void CheckDPS()
    {
        if (Time.time > nextTimeToCheckDPS)
        {
            nextTimeToCheckDPS = Time.time + 1f;
            dps = accumMoney;
            accumMoney = 0f;
        }
    }

    void DisplayInfos()
    {
        moneyUI.text = "$ " + Mathf.FloorToInt(money).ToString();
        dpsUI.text = dps.ToString();
    }
}