using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcHealthBar : MonoBehaviour {

    Image image;

    float health;

    float health2;


    NpcHurted npcScript;

    void Start () {

        image = GetComponent<Image>();


        npcScript = transform.parent.parent.GetComponent<NpcHurted>();

        health = transform.parent.parent.GetComponent<NpcHurted>().Health();

        Debug.Log(transform.parent.parent.GetComponent<NpcHurted>().Health());

       

        //image.fillAmount = health;
    }

    // Update is called once per frame
    void Update ()
    {



        if (health > 0)
        {

            image.fillAmount -= 1.0f / health * Time.deltaTime / 2;
        }
    }
}
