using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementalCore : MonoBehaviour {

	float money;
	float moneyPerSecIndicator;

	public Text moneyUI;
	public GameObject totalEarningPerSecUI;
	public GameObject customerSatisfactionUI;
	public GameObject elevatorUI;
	public GameObject lobbyClickerButton;
	public GameObject elevatorClickerButton;

	public float updatePeriodSec;
	public Shop[] shops;

	int numberOfShops;

	void Start () {
		Init ();
	}

	void Init () 
	{
		money = 0f;
		moneyPerSecIndicator = 0f;

		numberOfShops = shops.Length;
	}	

	void Update () {
		
		moneyUI.text = "$ " + Mathf.RoundToInt(money).ToString();
	}
}