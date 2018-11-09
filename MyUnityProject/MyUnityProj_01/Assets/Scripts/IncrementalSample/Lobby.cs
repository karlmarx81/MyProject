using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

	public Text peopleIncomeUI;
	public Text currentPeopleUI;

	public Elevator elevator;

	public float maxCapacity;
	public float peopleIncomePerSec;

	[HideInInspector]
	public float currentPeople;

	void Start () {
		
	}
	
	void Update () {
		IncreasePeople ();
		DisplayInfos ();
	}

	void IncreasePeople () {
		currentPeople += peopleIncomePerSec * Time.deltaTime;
	}

	public float SendPeopleToElevator (float elevRequest)
	{
		float peopleToTransfer = 0f;
		elevRequest *= Time.deltaTime;

		if (currentPeople >= elevRequest) {
			currentPeople -= elevRequest;
		} else if (currentPeople < elevRequest && currentPeople > 0f) {
			currentPeople = 0f;
			peopleToTransfer = elevRequest - currentPeople;
		} else {
			peopleToTransfer = 0f;
		}

		if (currentPeople >= 0f) {
			currentPeople -= peopleToTransfer;
		}

		return peopleToTransfer;
	}

	void DisplayInfos ()
	{
		currentPeopleUI.text = "Current People : " + Mathf.RoundToInt(currentPeople).ToString ();
		peopleIncomeUI.text = "People Income per frame : " + Mathf.RoundToInt(peopleIncomePerSec * Time.deltaTime).ToString ();
	}
}
