using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcHealthBar : MonoBehaviour {

    private Image image;

    float health;

    float initialHealth;



    public Text timeText;

    NpcHurted npcScript;

    float healthTimeAux;

    Quaternion canvasInitRot, textInitRot;



    void Start () {

        image = GetComponent<Image>();

        timeText = transform.GetChild(0).GetComponent<Text>();

        npcScript = transform.parent.parent.GetComponent<NpcHurted>();

        health = transform.parent.parent.GetComponent<NpcHurted>().Health();

        timeText.text = "" + health;

        healthTimeAux = health;

        initialHealth = health;
        //image.fillAmount = health;

        canvasInitRot = transform.rotation;

        textInitRot = transform.GetChild(0).rotation;
    }

    // Update is called once per frame
    void Update ()
    {

        transform.rotation = canvasInitRot;

        transform.GetChild(0).rotation = textInitRot;

        if (health > 0)
        {
            if(npcScript.IsOnFire() == true)
            {
                print("fogo dam");
                image.fillAmount -= 1.0f / health * Time.deltaTime * 4;

                healthTimeAux = image.fillAmount;

                timeText.text = "" + Mathf.Round(healthTimeAux * initialHealth);

                npcScript.faliceu = true;
            }
            else
            {
                image.fillAmount -= 1.0f / health * Time.deltaTime / 2;

                healthTimeAux = image.fillAmount;

                timeText.text = "" + Mathf.Round(healthTimeAux * initialHealth);
            }
            

        }
    }
}
