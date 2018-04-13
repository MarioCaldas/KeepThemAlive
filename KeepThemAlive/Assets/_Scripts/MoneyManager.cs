using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    int Money;
    public Text MoneyText;

	void Start ()
    {
        Money = 5000;
	}

	void Update ()
    {
        Debug.Log("Money: " + Money);
        MoneyText.text = "" + Money + "/5000";
	}

    public void BuySomething(int cost)
    {
        Debug.Log("Money: " + Money);
        Money = Money - cost;
        Debug.Log("Money: " + Money);
    }

    public bool CanBuy(int cost)
    {
        if (cost > Money)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
