using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceDescription : MonoBehaviour
{
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
            Description.text = "Object: Wood Desk \nIt's a weak spot to hide during an earthquake" +
                "\n\nAdvice: Buy Metal Desk \nDescription: Desk with high resistance, the civilians will have 50hp left \n\nCost: 100";
        }
        else if (Source == "wall")
        {
            Description.text = "Object: Normal wall \nNormal walls could break easily during an earthquake" +
                "\n\nAdvice: Buy Concrete Wall \n Descripiton: Wall with high endurance, the civilians will have 70hp left \n\nCost: 300";
        }
        else if (Source == "Window")
        {
            Description.text = "Object: Wood Window \nIt's not safe to be close to them because the glass can break during an earthquake" +
                "\n\nAdvice: Buy Metal Window \n Description: Reinforced window that is prepared to vibrations, the civilians will have 30hp left \n\nCost: 50";
        }
        else if (Source == "Pillar")
        {
            Description.text = "Object: Wood Pillar \nAlthough it can support the ceiling, this pillar definitely cannot prevent it from falling" +
                "\n\nAdvice: Buy Concrete Pillar \n Description: Pillar that is prepared to support the ceiling, the civilians will have 80hp left \n\nCost: 125";
        }
    }

    public static void Clear()
    {
        Painel.SetActive(false);
        Description.text = "";
    }
    
}
