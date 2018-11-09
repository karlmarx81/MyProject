using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
		
	public Text currentPeopleUI;
	public Text earningUI;

    public IncrementalCore incCore;

    public float maxCap;
    public float peopleProcessSpd;
    public float earningPerPeople;
    public float initPpl;

    float currentPpl;
	float earning;
	float accumulatedEarning;

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
        else
        {
            isEmpty = false;
        }
    }

	void DisplayInfos ()
	{
        currentPeopleUI.text = "Current People : " + Mathf.FloorToInt(currentPpl).ToString ();
        earningUI.text = "Earning Spd : " + Mathf.RoundToInt(earning).ToString ();
	}
}