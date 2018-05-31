﻿using System.Collections;
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

    GameObject deadSign;

    void Start () {

        image = GetComponent<Image>();

        timeText = transform.GetChild(0).GetComponent<Text>();

        if(transform.parent.parent != null)
        {
            npcScript = transform.parent.parent.GetComponent<NpcHurted>();
            health = transform.parent.parent.GetComponent<NpcHurted>().Health();

        }


        timeText.text = "" + health;

        healthTimeAux = health;

        initialHealth = health;
        //image.fillAmount = health;

        canvasInitRot = transform.rotation;

        textInitRot = transform.GetChild(0).rotation;

        deadSign = transform.GetChild(1).gameObject;
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
                image.fillAmount -= 1.0f / health * Time.deltaTime * 4;

                healthTimeAux = image.fillAmount;

                timeText.color = Color.black;
                image.color = Color.black;
                deadSign.GetComponent<Image>().color = Color.black;

                timeText.text = "" + Mathf.Round(healthTimeAux * initialHealth);

               
            }
            else
            {
                image.fillAmount -= 1.0f / health * Time.deltaTime / 3;

                healthTimeAux = image.fillAmount;

                timeText.text = "" + Mathf.Round(healthTimeAux * initialHealth);
            }
            

        }

        if(healthTimeAux == 0 && npcScript != null)
        {
            npcScript.faliceu = true;
            Debug.Log("é zero");

            deadSign.SetActive(true);

            timeText.text = "";
        }
    }
}
