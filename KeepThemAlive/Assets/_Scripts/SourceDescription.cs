using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceDescription : MonoBehaviour
{
    public static GameObject DescriptionPanel, QuestionPanel;
    public static Text Description, Question, Money;

    static int cost;
    
    private void Start()
    {
        DescriptionPanel = transform.GetChild(0).gameObject;
        QuestionPanel = transform.GetChild(1).gameObject;

        Description = transform.GetChild(0).GetComponentInChildren<Text>();
        Question = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Money = transform.GetChild(1).Find("MoneyText").GetComponent<Text>();

        DescriptionPanel.SetActive(false);
        QuestionPanel.SetActive(false);
    }

    public static void ChangeDescription(string Source)
    {
        DescriptionPanel.SetActive(true);

        if (Source == "Desk")
        {
            Description.text = "Object: Wood Desk \nIt's a weak spot to hide during an earthquake" +
                "\n\nAdvice: Buy Metal Desk \nDescription: Desk with high resistance, the civilians will have 50hp left";
        }
        else if (Source == "wall")
        {
            Description.text = "Object: Normal wall \nNormal walls could break easily during an earthquake" +
                "\n\nAdvice: Buy Concrete Wall \n Descripiton: Wall with high endurance, the civilians will have 70hp left";
        }
        else if (Source == "Window")
        {
            Description.text = "Object: Wood Window \nIt's not safe to be close to them because the glass can break during an earthquake" +
                "\n\nAdvice: Buy Metal Window \n Description: Reinforced window that is prepared to vibrations, the civilians will have 30hp left";
        }
        else if (Source == "Pillar")
        {
            Description.text = "Object: Wood Pillar \nAlthough it can support the ceiling, this pillar definitely cannot prevent it from falling" +
                "\n\nAdvice: Buy Concrete Pillar \n Description: Pillar that is prepared to support the ceiling, the civilians will have 80hp left";
        }
    }

    public static void SetQuestion(string source)
    {
        QuestionPanel.SetActive(true);

        switch (source)
        {
            case "Desk":
                Question.text = "Are you sure?";
                Money.text = "" + ChangeObjects.Cost;
                break;
            case "wall":
                Question.text = "Are you sure?";
                Money.text = "" + ChangeObjects.Cost;
                break;
            case "Window":
                Question.text = "Are you sure?";
                Money.text = "" + ChangeObjects.Cost;
                break;
            case "Pillar":
                Question.text = "Are you sure?";
                Money.text = "" + ChangeObjects.Cost;
                break;
            case "Door":
                Question.text = "Are you sure?";
                Money.text = "" + ChangeObjects.Cost;
                break;
        }
    }

    public static void Clear()
    {
        DescriptionPanel.SetActive(false);
        Description.text = "";
    }

    public void DeactivateQuestionPanel()
    {
        QuestionPanel.SetActive(false);
        Question.text = "";
    }
    
}
