using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour {

	public Text transferAmountPerSecUI;
	public Text receivingAmountPerSecUI;
	public Text currentPeopleUI;
    public Text elevUI;

    public Shop[] shopList;

	public float maxCap;
	public float transferSpd;

    public IncrementalCore incCore;
    public Lobby lobby;

    float currentPpl;
    float receivedPpl;

    bool isMaxxed;
    bool isEmpty;

    public float[] maxCapAdd;
    public float[] transferSpdAdd;
    public float[] upgradeCost;

    int currentLv = 0;

    void Start () {
        currentPpl = 0f;
        receivedPpl = 0f;

        isMaxxed = false;
        isEmpty = true;
	}

	void Update () {
        CheckCapacity();
        TakePeopleFromLobby();
        SendPeopleToShop();
        DisplayInfos();
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

    void TakePeopleFromLobby()
    {
        if (isMaxxed == false)
        {
            receivedPpl = lobby.SendPeopleToElevator(transferSpd);
            currentPpl += receivedPpl;
        }
    }

    void SendPeopleToShop()
    {
        if (isEmpty == false)
        {
            currentPpl -= transferSpd * Time.deltaTime;
        }

        for (int i = 0; i < shopList.Length; i++)
        {
            shopList[i].ReceivePeople(transferSpd * Time.deltaTime / shopList.Length);
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
                transferSpd += transferSpdAdd[currentLv];

                Debug.Log("Elevator Upgrade Success");
            }
            else
            {
                Debug.Log("Elevator : Hey! You are not ready!");
            }
        }
    }

    void DisplayInfos()
    {
        //currentPeopleUI.text = "Current People : " + Mathf.FloorToInt(currentPpl).ToString();
        //receivingAmountPerSecUI.text = "Receiving People : " + Mathf.FloorToInt(receivedPpl).ToString();
        transferAmountPerSecUI.text = "Transfer Spd : " + Mathf.FloorToInt(transferSpd).ToString();
        elevUI.text = "Elev Lv. " + currentLv.ToString() + ", Next Upg. Cost : " + upgradeCost[currentLv + 1].ToString();
    }	
}
