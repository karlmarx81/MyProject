using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

	public Text peopleIncomeUI;
	public Text currentPeopleUI;

	public Elevator elevator;

	public float maxCap;
	public float incomeSpd;
    public float initPpl;

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

	void DisplayInfos ()
	{
        currentPeopleUI.text = "Current People : " + Mathf.FloorToInt(currentPpl).ToString();
        peopleIncomeUI.text = "People Income : " + Mathf.FloorToInt(incomeSpd).ToString();
	}
}
