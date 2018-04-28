using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceDescription : MonoBehaviour {

    public static GameObject Painel;
    public static Text Description;

    private void Start()
    {
        Painel = transform.GetChild(0).gameObject;
        Description = transform.GetChild(0).GetComponentInChildren<Text>();
        Painel.SetActive(false);
    }

    public static void ChangeDescription(string Source)
    {
        Painel.SetActive(true);
        if (Source == "Desk")
        {
            Description.text = "Source: Desk \n\nOwned: Wood Table \nIt's a weak spot to hide when exist an earthquake" +
                "\n\nBuy: Metal Desk \nCost: 100 \nDesk with high resistance, that block rocks that fall during an earthquake";
        }
        else if (Source == "wall")
        {
            Description.text = "Source: Wall \n\nOwned: Normal wall \nNormal walls could break when exist an earthquake" +
                "\n\nBuy: Concrete Wall \nCost: 300 \nIt's a Wall with high resistance, that block vibrations and is hard to fall";
        }
        else if (Source == "Window")
        {
            Description.text = "Source: Window \n\nOwned: Wood Window \nIt's dangerous be close because of glass fragments when exist an earthquake" +
                "\n\nBuy: Metal Window \nCost: 50 \nWindow that is prepared to vibrations, but take care to get away during an earthquake";
        }
        else if (Source == "Pillar")
        {
            Debug.Log("Entrei!!!");
            Description.text = "Source: Pillar \n\nOwned: Wood Pillar \nWood always could break when exist an earthquake" +
                "\n\nBuy: Concrete Pillar \nCost: 125 \nPillar that is prepared to don't collapse the roof during an earthquake";
        }
    }

    public static void Clear()
    {
        Painel.SetActive(false);
        Description.text = "";
    }
    
}
