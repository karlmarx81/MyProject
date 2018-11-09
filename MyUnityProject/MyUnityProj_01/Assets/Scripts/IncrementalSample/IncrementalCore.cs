using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalCore : MonoBehaviour {

	public Text moneyUI;
    public Text dpsUI;

    public float initMoney;
    public float cheatMoneyAmount;	

    [HideInInspector]
    public float money;

    float moneyPerSecIndicator;

    float accumMoney;
    float nextTimeToCheckDPS;
    float dps;

	void Start () {
        money = initMoney;
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

    public void IncreaseMoneyCheat()
    {
        money += cheatMoneyAmount;
    }

    void DisplayInfos()
    {
        //moneyUI.text = "$ " + Mathf.FloorToInt(money).ToString();
        moneyUI.text = "$ " + money.ToString(".000");
        dpsUI.text = "Earning Per Sec : " + dps.ToString();
    }
}