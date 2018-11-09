using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

	public Text peopleIncomeUI;
	public Text currentPeopleUI;
    public Text LobbyUI;

    public IncrementalCore incCore;
    public Elevator elevator;

	public float maxCap;
	public float incomeSpd;
    public float initPpl;

    public float[] maxCapAdd;
    public float[] incomSpdAdd;
    public float[] upgradeCost;
	
    int currentLv = 0;

    float currentPpl;
    bool isMaxxed;
    bool isEmpty;


    void Start () {
        currentPpl = initPpl;
        isMaxxed = false;
        isEmpty = true;
	}
	
	void Update () {
        CheckCapacity();
        GatherPeople();
        DisplayInfos();
    }

    void GatherPeople ()
    {
        if (isMaxxed == false)
        {
            currentPpl += incomeSpd * Time.deltaTime;
        }
    }

    void CheckCapacity ()
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

    public float SendPeopleToElevator (float sendSpd)
    {
        float pplAmount = 0f;

        if (isEmpty == false)
        {
            currentPpl -= sendSpd * Time.deltaTime;
            pplAmount = sendSpd * Time.deltaTime;
        }
        return pplAmount;
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
                incomeSpd += incomSpdAdd[currentLv];

                Debug.Log("Lobby Upgrade Success");
            }
            else
            {
                Debug.Log("Lobby : Hey! You are not ready!");
            }
        }
    }

	void DisplayInfos ()
	{
        currentPeopleUI.text = "Current People : " + Mathf.FloorToInt(currentPpl).ToString() + " / " + maxCap.ToString();
        peopleIncomeUI.text = "People Income : " + Mathf.FloorToInt(incomeSpd).ToString();
        LobbyUI.text = "Lobby Lv. " + currentLv.ToString() + ", Next Upg. Cost : " + upgradeCost[currentLv + 1].ToString();
	}
}
