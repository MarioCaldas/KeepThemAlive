using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    int Money;
    public Text MoneyText;

    void Start ()
    {
        Money = 1000;
	}

	void Update ()
    {
        MoneyText.text = "" + Money + "/1000";
	}

    public void BuySomething(int cost)
    {
        Money = Money - cost;
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
