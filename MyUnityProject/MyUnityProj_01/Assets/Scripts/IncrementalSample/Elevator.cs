using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour {

	public Text transferAmountPerSecUI;
	public Text receivingAmountPerSecUI;
	public Text currentPeopleUI;

	public float maxCapacity;
	public float receivingAmountPerSec;
	public float transferAmountPerSec;

	public IncrementalCore incCore;
	public Lobby lobby;

	float currentPeople;
	float currentTransfer;

	void Start () {			
		currentTransfer = transferAmountPerSec;
	}

	void Update () {
		if (currentPeople < maxCapacity) {
			currentPeople += lobby.SendPeopleToElevator (receivingAmountPerSec);
		} else if (currentPeople > maxCapacity) {
			currentPeople = maxCapacity;
		}

		if (currentPeople > transferAmountPerSec * Time.deltaTime) {
			currentTransfer = transferAmountPerSec * Time.deltaTime;
		} else if (currentPeople < transferAmountPerSec * Time.deltaTime) {
			currentTransfer = (transferAmountPerSec * Time.deltaTime) - currentPeople;
		} else {
			currentTransfer = 0f;
		}

		currentPeople -= currentTransfer; 

		DisplayInfos ();
	}



	public void SetDistribution ()
	{
		
	}

	public bool CheckElevatorIsFull ()
	{
		return false;
	}

	void Working ()
	{
		
	}

	void DisplayInfos ()
	{
		currentPeopleUI.text = "Current People : " + Mathf.RoundToInt(currentPeople).ToString ();
		transferAmountPerSecUI.text = "People Transfer per frame : " + Mathf.RoundToInt(currentTransfer * Time.deltaTime).ToString ();
	}
}
