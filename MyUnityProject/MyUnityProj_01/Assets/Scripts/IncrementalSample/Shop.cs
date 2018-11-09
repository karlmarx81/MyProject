using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Shop : MonoBehaviour {
	public string shopName;
	public float maxCapacity;
	public float peopleProcessPerSec;
	public float earningPerPeople;	

	public Text currentPeopleUI;
	public Text earningUI;

	[HideInInspector]
	public float currentPeople;

	float remainingPeople;
	float earning;
	float accumulatedEarning;

//	public Shop (string _shopName, int _maxCapacity, int _peopleProcessPerSec, float _earningPerSec) {
//		shopName = _shopName;
//		maxCapacity = _maxCapacity;
//		peopleProcessPerSec = _peopleProcessPerSec;
//		earningPerSec = _earningPerSec;
//	}

	void Start ()
	{
		remainingPeople = 0f;
	}

	void Update ()
	{
		DisplayInfos ();
		Working ();
	}

	public void Receiving (float peopleIncome)
	{
		currentPeople += peopleIncome * Time.deltaTime;
	}

	public void Working ()
	{
		if (currentPeople > maxCapacity) {
			remainingPeople += (currentPeople - maxCapacity);
			currentPeople = maxCapacity;
		}

		if (currentPeople > 0f) {
			currentPeople -= peopleProcessPerSec * Time.deltaTime;

			if (currentPeople < 0f) {
				currentPeople = 0f;
			}
		}
	}

	public float Earning ()
	{
		if (currentPeople >= peopleProcessPerSec) {
			earning = peopleProcessPerSec * earningPerPeople * Time.deltaTime;
			//Debug.Log ("First Routine : " + earning);
		}
		else if (currentPeople < peopleProcessPerSec) { 			
			earning = (peopleProcessPerSec - currentPeople) * earningPerPeople * Time.deltaTime;
			//Debug.Log ("Second Routine : " + earning);
		}
		else {
			earning = 0f;
			//Debug.Log ("Last Routine : " + earning);
		}

		accumulatedEarning += earning;
		return earning;
	}

	void DisplayInfos ()
	{
		currentPeopleUI.text = "Current People : " + Mathf.RoundToInt(currentPeople).ToString ();
		earningUI.text = "Earning per frame : " + Mathf.RoundToInt(earning).ToString ();
	}
}