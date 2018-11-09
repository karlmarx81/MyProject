using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
		
	public Text currentPeopleUI;
    public Text processSpdUI;
	public Text earningUI;
    public Text shopUI;

    public IncrementalCore incCore;

    public float maxCap;
    public float peopleProcessSpd;
    public float earningPerPeople;
    public float initPpl;

    float currentPpl;
	float earning;

    public float[] maxCapAdd;
    public float[] processSpdAdd;
    public float[] earningPerPplAdd;
    public float[] upgradeCost;

    int currentLv = 0;

    bool isMaxxed;
    bool isEmpty;
    float wastedPpl;

	void Start ()
	{
        earning = 0f;
        currentPpl = initPpl;
        wastedPpl = 0f;
        isMaxxed = false;
        isEmpty = true;
	}

	void Update ()
	{
        CheckCapacity();
        ProcessPeople();
        DisplayInfos ();
	}

    void ProcessPeople ()
    {
        if (isEmpty == false)
        {
            currentPpl -= peopleProcessSpd * Time.deltaTime;
            earning = (peopleProcessSpd * Time.deltaTime) * earningPerPeople;
            incCore.MakeMoney(earning);

            //Debug.Log("Current People is : " + currentPpl);
            //Debug.Log("Earning is : " + earning);
        }
    }

    public void ReceivePeople (float amount)
    {
        if (isMaxxed == false)
        {
            currentPpl += amount;    
        }
        else if (isMaxxed == true)
        {
            wastedPpl += amount;
        }
    }

    void CheckCapacity()
    {
        if (currentPpl >= maxCap)
        {
            currentPpl = maxCap;
            isMaxxed = true;
        }
        else
        {
            isMaxxed = false;
        }

        if (currentPpl <= 0f)
        {
            currentPpl = 0f;
            isEmpty = true;
        }
        else if (currentPpl > 1f)
        {
            isEmpty = false;
        }
    }

    public void IncreaseLevel()
    {
        if (currentLv < maxCapAdd.Length - 1)
        {
            if (upgradeCost[currentLv + 1] < incCore.money)
            {
                currentLv++;
                incCore.money -= upgradeCost[currentLv];

                maxCap += maxCapAdd[currentLv];
                peopleProcessSpd += processSpdAdd[currentLv];
                earningPerPeople += earningPerPplAdd[currentLv];

                Debug.Log(this.transform.name + " Upgrade Success");
            }
            else
            {
                Debug.Log(this.transform.name + " : Hey! You are not ready!");
            }
        }
    }

    void DisplayInfos ()
	{
        currentPeopleUI.text = "Current People : " + Mathf.FloorToInt(currentPpl).ToString () + " / " + maxCap.ToString(); 
        processSpdUI.text = "Process Spd : " + peopleProcessSpd.ToString();
        earningUI.text = "Earning Spd : " + earning.ToString (".000");
        shopUI.text = this.transform.name + " Lv. " + currentLv.ToString() + ", Next Upg. Cost : " + upgradeCost[currentLv + 1].ToString();
    }
}